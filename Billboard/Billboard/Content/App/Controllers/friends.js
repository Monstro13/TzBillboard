angular.module('Billboard').controller('FriendsCtrl', ['$scope', '$rootScope', '$location', 'Manager',
    function ($scope, $rootScope, $location, Manager) {

        $scope.getFriends = function () {
            Manager.Friends($rootScope.currentUser).then(function (result) {
                $scope.friends = result;
            });
        };

        $scope.getFriends();

        Manager.AllUsers().then(function (result) {
            $scope.allusers = result;
        });

        $scope.addFriend = function (id) {
            Manager.AddUserToFriends(id).then(function (result) {
                if (result) {
                    $rootScope.Alert('success', 'Пользователь добавлен в друзья');
                    $scope.getFriends();
                } else {
                    $rootScope.Alert('info', 'Ошибка. Возможно пользователь уже у вас в друзьях');
                }
            });
        };

    }]);