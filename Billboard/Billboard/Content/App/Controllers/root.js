angular.module('Billboard').controller('RootCtrl', ['$scope', '$rootScope', '$timeout', 'Auth',
    function ($scope, $rootScope, $timeout, Auth) {

        var timer;

        $rootScope.Alert = function (type, msg) {
            var alert = {
                type: type,
                msg: msg
            };

            $rootScope.alert = alert;
            $rootScope.isAlert = true;

            timer = $timeout($rootScope.alertTimer, 4000);
        }

        $rootScope.alertTimer = function () {
            $rootScope.isAlert = false;
            $timeout.cancel(timer);
        }

        Auth.checkLogin();

        $scope.logout = function () {
            Auth.logout();
        }

        $scope.query = "";

        $scope.search = function (go) {
            if (($scope.query.length > 3 || $scope.query.length == 0 || go == 'go') && $rootScope.isLogged) {
                $scope.$$childTail.query = $scope.query;
                $scope.$$childTail.currentPage = 1;
                $scope.$$childTail.setPosts(false, true);
            }
        };

    }]);