angular.module('Billboard')
    .factory('Manager', ['$http', '$location', '$rootScope',
        function ($http, $location, $rootScope) {

            var api = {};

            api.Posts = function (page, query) {
                return $http.post('/Apis/Posts', { page: page, query: query }).then(function (results) {
                    return results.data;
                })
            };

            api.deletePost = function (id) {
                return $http.post('/Apis/DeletePost', { id: id }).then(function (results) {
                    if (results.data) {
                        $rootScope.Alert('info', 'Объявление удалено.');
                    } else {
                        $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения. Попробуйте позже');
                    }
                });
            };

            api.savePost = function (post) {
                return $http.post('/Apis/SavePost',
                    {
                        id: post.id,
                        title: post.title,
                        text: post.text
                    }).then(function (results) {
                        if (results.data) {
                            $rootScope.m.dismiss('cancel');
                            $rootScope.Alert('info', 'Объявление изменено.');
                        } else {
                            $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения. Попробуйте позже');
                        }
                    });
            };

            api.Friends = function (userId) {
                return $http.post('/Apis/Friends', { userId: userId }).then(function (results) {
                    return results.data;
                })
            };

            api.FriendsPost = function (login) {
                return $http.post('/Apis/FriendsPosts', { login: login }).then(function (results) {
                    return results.data;
                })
            };

            api.AllUsers = function () {
                return $http.post('/Apis/AllUsers').then(function (results) {
                    return results.data;
                })
            };

            api.AddUserToFriends = function (id) {
                return $http.post('/Apis/AddToFriend', { id: id }).then(function (results) {
                    return results.data;
                })
            };

            return api;
        }
    ]);