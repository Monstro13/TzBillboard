angular.module('Billboard').controller('WelcomeCtrl', ['$scope', '$rootScope', '$location',
    function ($scope, $rootScope, $location) {
        if ($rootScope.isLogged)
            $location.path('/advt');
    }]);