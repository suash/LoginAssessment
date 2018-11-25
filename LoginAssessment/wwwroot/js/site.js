var app = angular.module('assessment', ['ngAnimate', 'ngSanitize', 'ui.bootstrap', 'ngPasscheck']);

app.config(function(passCheckProvider) {
        passCheckProvider.init({
            policies: {
                weak: {
                    pattern: '[a-z ]{12,}$',
                    min: 12
                },
                low: {
                    pattern: '[a-z ]{15,}$',
                    min: 15
                },
                medium: {
                    pattern: '[a-z ]{18,}$',
                    min: 18
                },
                strong: {
                    pattern: '[a-z ]{25,}$',
                    min: 25
                },
                stronger: {
                    pattern: '[a-z ]{35,}$',
                    min: 35
                },
                strongest: {
                    pattern: '[a-z ]{50,}$',
                    min: 50
                }
            },
            scoring: {
                base: 1,
                weak: {
                    min: 1,
                    max: 10,
                    bonus: 1
                },
                low: {
                    min: 11,
                    max: 15,
                    bonus: 1
                },
                medium: {
                    min: 16,
                    max: 40,
                    bonus: 1.05
                },
                strong: {
                    min: 41,
                    max: 60,
                    bonus: 1.25
                },
                stronger: {
                    min: 61,
                    max: 80,
                    bonus: 1.50
                },
                strongest: {
                    min: 81,
                    max: 100,
                    bonus: 1.50
                }
            },
            common: {
                test: true,
                path: '~/wwwroot/lib/ng-passcheck/dist/passwords.json'
            }
        });
    });

app.controller('RegisterController', function ($scope, $window) {
    $scope.step2Disabled = true;
    $scope.step3Disabled = true;
    $scope.step4Disabled = true;
    $scope.age = 18;
    $scope.complexity = {
        value: 0,
        type: 'danger',
        text: 'Weak'
    };

    $scope.$on('passwordStrength:result', function (event, value) {
        $scope.strength = value;
    });

    $scope.canProceedToStep2 = function () {
        return !$scope.step2Disabled;
    };

    $scope.canProceedToStep3 = function () {
        return !$scope.step3Disabled && !$scope.step2Disabled;
    };

    $scope.canProceedToStep4 = function () {
        return !$scope.step4Disabled && !$scope.step3Disabled && !$scope.step2Disabled;
    };

    $scope.validateStep1 = function () {
        var form = $scope.registerForm;
        var isvalid = form.age.$modelValue > 0 && form.gender.$modelValue && form.email.$modelValue;
        $scope.step2Disabled = !isvalid;
    };

    $scope.validateStep2 = function () {
        var isvalid = $scope.isValidLength &&
            $scope.hasSpecialCharacter &&
            $scope.hasDigit &&
            $scope.hasLowercaseLetter &&
            $scope.hasUppercaseLetter &&
            !$scope.passwordsNotMatching;

        $scope.step3Disabled = !isvalid;
    };

    $scope.validateStep3 = function() {
        var form = $scope.registerForm;
        var isvalid = form.passphrase.$modelValue && form.passphrase.$modelValue.length >= 16 && !$scope.passphraseNotMatching;
        $scope.step4Disabled = !isvalid;
    };

    $scope.canSubmit = function () {
        $scope.validateStep1();
        $scope.validateStep2();
        $scope.validateStep3();

        return $scope.canProceedToStep2() && $scope.canProceedToStep3() && $scope.canProceedToStep4();
    }

    $scope.function = function () {
        $scope.active = 1;
    };

    $scope.goToStep2 = function () {
        $scope.active = 1;
    };

    $scope.goToStep3 = function () {
        $scope.active = 2;
    };

    $scope.goToStep4 = function () {
        $scope.active = 3;
    };

    $scope.password1Change = function () {
        var form = $scope.registerForm;
        if (form.password.$modelValue) {
            var passwordValue = form.password.$modelValue;
            $scope.isValidLength = passwordValue.length > 8;
            $scope.hasSpecialCharacter = new RegExp('[`~!@#$%^&*()\\-\\_=+,<.>]').test(passwordValue);
            $scope.hasDigit = new RegExp('[0-9]').test(passwordValue);
            $scope.hasLowercaseLetter = new RegExp('[a-z]').test(passwordValue);
            $scope.hasUppercaseLetter = new RegExp('[A-Z]').test(passwordValue);
        } else {
            $scope.isValidLength = false;
            $scope.hasSpecialCharacter = false;
            $scope.hasDigit = false;
            $scope.hasLowercaseLetter = false;
            $scope.hasUppercaseLetter = false;
        }

        $scope.validateStep2();

        $scope.setComplexity();

        $scope.password2Change();
    };

    $scope.password2Change = function () {
        var form = $scope.registerForm;
        $scope.passwordsNotMatching = form.password.$modelValue !== form.password2.$modelValue;
        $scope.validateStep1();
        $scope.validateStep2();
    };

    $scope.checkPassphraseMatch = function() {
        var form = $scope.registerForm;
        $scope.passphraseNotMatching = form.passphrase.$modelValue && form.passphrase.$modelValue !== form.passphrase2.$modelValue;

        $scope.validateStep1();
        $scope.validateStep2();
        $scope.validateStep3();
    };

    $scope.setComplexity = function() {
        var value = 0;
        if ($scope.isValidLength) {
            value = value + 20;
        }

        if ($scope.hasSpecialCharacter) {
            value = value + 20;
        }

        if ($scope.hasDigit) {
            value = value + 20;
        }

        if ($scope.hasLowercaseLetter) {
            value = value + 20;
        }

        if ($scope.hasUppercaseLetter) {
            value = value + 20;
        }

        $scope.complexity.value = value;

        switch (value) {
        case 0:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Very Weak';
            break;
        case 20:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Ver Weak';
            break;
        case 40:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Weak';
            break;
        case 60:
            $scope.complexity.type = 'warning';
            $scope.complexity.text = 'Weak';
            break;
        case 80:
            $scope.complexity.type = 'info';
            $scope.complexity.text = 'Strong';
            break;
        case 100:
            $scope.complexity.type = 'success';
            $scope.complexity.text = 'Very Strong';
            break;
        default:
        }

        console.log('Complexity: ' + JSON.stringify($scope.complexity));
    };
});

app.controller('PasswordResetController', function ($scope) {
    $scope.complexity = {
        value: 0,
        type: 'danger',
        text: 'Weak'
    };

    $scope.formInValid = true;

    $scope.password1Change = function () {
        var form = $scope.passwordResetForm;
        if (form.password.$modelValue) {
            var passwordValue = form.password.$modelValue;
            $scope.isValidLength = passwordValue.length > 8;
            $scope.hasSpecialCharacter = new RegExp('[`~!@#$%^&*()\\-\\_=+,<.>]').test(passwordValue);
            $scope.hasDigit = new RegExp('[0-9]').test(passwordValue);
            $scope.hasLowercaseLetter = new RegExp('[a-z]').test(passwordValue);
            $scope.hasUppercaseLetter = new RegExp('[A-Z]').test(passwordValue);
        } else {
            $scope.isValidLength = false;
            $scope.hasSpecialCharacter = false;
            $scope.hasDigit = false;
            $scope.hasLowercaseLetter = false;
            $scope.hasUppercaseLetter = false;
        }

        $scope.setComplexity();
        $scope.password2Change();
    };

    $scope.validateForm = function() {
        var form = $scope.passwordResetForm;

        var isvalid = $scope.isValidLength &&
            $scope.hasSpecialCharacter &&
            $scope.hasDigit &&
            $scope.hasLowercaseLetter &&
            $scope.hasUppercaseLetter &&
            !$scope.passwordsNotMatching;

        $scope.formInValid = !isvalid;
    };

    $scope.password2Change = function () {
        var form = $scope.passwordResetForm;
        if (!form.password.$modelValue || !form.password2.$modelValue) {
            $scope.passwordsNotMatching = true;
        } else {
            $scope.passwordsNotMatching = form.password.$modelValue !== form.password2.$modelValue;
        }

        $scope.validateForm();
    };

    $scope.setComplexity = function () {
        var value = 0;
        if ($scope.isValidLength) {
            value = value + 20;
        }

        if ($scope.hasSpecialCharacter) {
            value = value + 20;
        }

        if ($scope.hasDigit) {
            value = value + 20;
        }

        if ($scope.hasLowercaseLetter) {
            value = value + 20;
        }

        if ($scope.hasUppercaseLetter) {
            value = value + 20;
        }

        $scope.complexity.value = value;

        switch (value) {
        case 0:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Very Weak';
            break;
        case 20:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Very Weak';
            break;
        case 40:
            $scope.complexity.type = 'danger';
            $scope.complexity.text = 'Weak';
            break;
        case 60:
            $scope.complexity.type = 'warning';
            $scope.complexity.text = 'Weak';
            break;
        case 80:
            $scope.complexity.type = 'info';
            $scope.complexity.text = 'Strong';
            break;
        case 100:
            $scope.complexity.type = 'success';
            $scope.complexity.text = 'Very Strong';
            break;
        default:
        }

        console.log('Complexity: ' + JSON.stringify($scope.complexity));
    };
});

app.controller('PassphraseResetController', function($scope) {

    $scope.$on('passwordStrength:result', function (event, value) {
        $scope.strength = value;
    });

    $scope.checkPassphraseMatch = function () {
        var form = $scope.passphraseResetForm;
        $scope.passphraseNotMatching = form.passphrase.$modelValue && (form.passphrase.$modelValue !== form.passphrase2.$modelValue);
        $scope.validateForm();
    };

    $scope.validateForm = function () {
        var form = $scope.passphraseResetForm;
        var isvalid = form.passphrase.$modelValue && (form.passphrase.$modelValue.length >= 16 && !$scope.passphraseNotMatching);
        $scope.formInValid = !isvalid;
    };
});

app.controller('DummyController', function() {

});