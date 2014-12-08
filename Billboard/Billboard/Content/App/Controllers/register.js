angular.module('Billboard').controller('RegisterCtrl', ['$scope', '$modal', '$log',
    function ($scope, $modal, $log) {
        var modalInstance = $modal.open({
            templateUrl: 'popup-register.html',
            controller: 'ModalRegisterCtrl',
            backdrop: 'static'
        });

        modalInstance.result.then(function () {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }]);

angular.module('Billboard').controller('ModalRegisterCtrl', ['$scope', '$modalInstance', '$location', 'Auth', '$rootScope',
    function ($scope, $modalInstance, $location, Auth, $rootScope) {

        $rootScope.m = $modalInstance;

        $scope.register = function () {
            Auth.register({
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
