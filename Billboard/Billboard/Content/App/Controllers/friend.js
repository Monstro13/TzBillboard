angular.module('Billboard').controller('FriendCtrl', ['$scope', '$rootScope', '$location', 'Manager', '$routeParams',
    function ($scope, $rootScope, $location, Manager, $routeParams) {
        Manager.FriendsPost($routeParams.friend).then(function (result) {
            $scope.posts = result;

            if ($scope.posts.length > 0) {
                $scope.postsIsFull = true;
                $scope.currentPage = 1;
            }
        });
    }]);