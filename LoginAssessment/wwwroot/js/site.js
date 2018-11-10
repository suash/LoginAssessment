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

    $scope.$on('passwordStrength:result', function (event, value) {
        $scope.strength = value;
    });

    $scope.canProceedToStep2 = function () {
        return !$scope.step2Disabled;
    };

    $scope.canProceedToStep3 = function () {
        return !$scope.step3Disabled;
    };


    $scope.canProceedToStep4 = function () {
        return !$scope.step4Disabled;
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
        var isvalid = form.passphrase.$modelValue && form.passphrase.$modelValue.length >= 12 && !$scope.passphraseNotMatching;
        $scope.step4Disabled = !isvalid;
    };

    $scope.function = function () {
        $scope.active = 0;
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
    };

    $scope.password2Change = function () {
        var form = $scope.registerForm;
        $scope.passwordsNotMatching = form.password.$modelValue !== form.password2.$modelValue;

        $scope.validateStep2();
    };

    $scope.checkPassphraseMatch = function() {
        var form = $scope.registerForm;
        $scope.passphraseNotMatching = form.passphrase.$modelValue && form.passphrase.$modelValue !== form.passphrase2.$modelValue;
        $scope.validateStep3();
    };
});