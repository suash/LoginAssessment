angular.module('assessment', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);

angular.module('assessment').controller('RegisterController', function ($scope, $window) {
    $scope.step2Disabled = true;
    $scope.step3Disabled = true;
    $scope.step4Disabled = true;

    $scope.canProceedToStep2 = function () {
        return !$scope.step2Disabled;
    };

    $scope.canProceedToStep3 = function () {
        return !$scope.step3Disabled;
    };

    $scope.validateStep1 = function () {
        var form = $scope.registerForm;
        var isvalid = form.age.$modelValue > 0 && form.gender.$modelValue >= 0 && form.email.$modelValue;
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

    $scope.goToStep1 = function () {
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
});