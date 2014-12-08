angular.module('Billboard').controller('AddCtrl', ['$scope', '$modal', '$log',
    function ($scope, $modal, $log) {
        var modalInstance = $modal.open({
            templateUrl: 'popup-add.html',
            controller: 'ModalAddCtrl',
            backdrop: 'static'
        });

        modalInstance.result.then(function () {
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    }]);

angular.module('Billboard').controller('ModalAddCtrl', ['$scope', '$modalInstance', '$location', 'Auth', '$rootScope',
    function ($scope, $modalInstance, $location, Auth, $rootScope) {

        $rootScope.m = $modalInstance;

        $scope.setFocus = function (elem) {
            $scope.elem = elem;
        };

        $scope.closePopup = function () {
            $modalInstance.dismiss('cancel');
            $location.path('/');
        };

    }]);