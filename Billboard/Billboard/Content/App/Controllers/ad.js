angular.module('Billboard').controller('AdCtrl', ['$scope', 'Manager', '$rootScope', '$modal', '$log',
    function ($scope, Manager, $rootScope, $modal, $log) {

        $scope.query = "";

        $scope.currentPage = 1;
        if (!$scope.posts) $scope.postsIsFull = false;

        $scope.setPosts = function (rFlag, qFlag) {
            Manager.Posts($scope.currentPage, $scope.query).then(function (results) {
                if (results.length > 0) {
                    $scope.posts = results;
                    $scope.postsIsFull = true;
                } else if (rFlag) $scope.currentPage--;

                if (results.length == 0 && qFlag) {
                    $scope.posts = results;
                    $scope.postsIsFull = false;
                }
            });
        };

        $scope.setPosts(false, false);

        $scope.getLeft = function () {
            if ($scope.currentPage > 1) {
                $scope.currentPage--;
                $scope.setPosts(false, false);
            }
        };

        $scope.getRight = function () {
            $scope.currentPage++;
            $scope.setPosts(true, false);
        };

        $scope.delete = function (id) {
            Manager.deletePost(id);
            $scope.posts.forEach(function (item, index) {
                if (item.Id == id)
                    $scope.posts.splice(index, 1);
            });
            if ($scope.posts.length == 0) $scope.postsIsFull = false;
        };

        $scope.edit = function (id) {
            $scope.posts.forEach(function (item, index) {
                if (item.Id == id) {
                    $rootScope.editPost = item;

                    var modalInstance = $modal.open({
                        templateUrl: 'popup-edit.html',
                        controller: 'ModalEditCtrl',
                        backdrop: 'static'
                    });

                    modalInstance.result.then(function () {
                    }, function () {
                        $log.info('Modal dismissed at: ' + new Date());
                    });
                }
            });
        }

    }]);

angular.module('Billboard').controller('ModalEditCtrl', ['$scope', '$modalInstance', '$location', '$rootScope', 'Manager',
    function ($scope, $modalInstance, $location, $rootScope, Manager) {

        $rootScope.m = $modalInstance;

        $scope.saveChanges = function () {
            Manager.savePost({
                id: $rootScope.editPost.Id,
                title: $rootScope.editPost.Title,
                text: $rootScope.editPost.Text
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