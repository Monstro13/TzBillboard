angular.module('Billboard').controller('LoginCtrl', ['$scope', '$modal', '$log',
    function ($scope, $modal, $log) {
        var modalInstance = $modal.open({
            templateUrl: 'popup-login.html',
            controller: 'ModalLoginCtrl',
            backdrop: 'static'
        });

        modalInstance.result.then(function () {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }]);

angular.module('Billboard').controller('ModalLoginCtrl', ['$scope', '$modalInstance', '$location', 'Auth', '$rootScope',
    function ($scope, $modalInstance, $location, Auth, $rootScope) {

        $rootScope.m = $modalInstance;

        $scope.login = function () {
            Auth.login({
                login: $scope.Login,
                password: $scope.Password
            });
        };

        $scope.setFocus = function (elem) {
            $scope.elem = elem;
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss('cancel');
            $location.path('/');
        };
    }]);