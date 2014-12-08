angular.module('Billboard')
    .factory('Auth', ['$http', '$location', '$rootScope',
        function ($http, $location, $rootScope) {

            var api = {};

            api.register = function (user) {
                return $http.post('/Apis/Register',
                    {
                        login: user.login,
                        password: user.password
                    })
                    .success(function (data) {
                        if (data) {
                            $rootScope.currentUser = data.Id;
                            $rootScope.isLogged = true;
                            $rootScope.m.dismiss('cancel');
                            $location.path('/advt');
                            $rootScope.Alert('success', 'Поздравляем. Регистрация завершена. Вы вошли на сайт');
                        } else {
                            $rootScope.Alert('info', 'Ошибка. Регистрация невозможна. Вероятно такой пользователь уже существует.');
                        }
                    })
                    .error(function () {
                        $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения. Попробуйте позже');
                    });
            };

            api.login = function (user) {
                return $http.post('/Apis/Login',
                    {
                        login: user.login,
                        password: user.password
                    })
                    .success(function (data) {
                        if (data) {
                            $rootScope.currentUser = data.Id;
                            $rootScope.isLogged = true;
                            $rootScope.m.dismiss('cancel');
                            $location.path('/advt');
                            $rootScope.Alert('success', 'Поздравляем. Вы успешно вошли на сайт.');
                        } else {
                            $rootScope.Alert('info', 'Ошибка. Вход невозможен. Вероятно такого пользователя не существует.');
                        }
                    })
                    .error(function () {
                        $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения. Попробуйте позже');
                    });
            };

            api.checkLogin = function () {
                return $http.post('/Apis/CheckLogin')
                    .success(function (data) {
                        if (data) {
                            $rootScope.currentUser = data.Id;
                            $rootScope.isLogged = true;
                        }
                    })
                    .error(function () {
                        $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения.');
                    });
            };

            api.logout = function () {
                return $http.post('/Apis/Logout')
                    .success(function (data) {
                        $rootScope.currentUser = null;
                        $rootScope.isLogged = false;
                        $location.path('/');
                        $rootScope.Alert('info', 'Вы успешно вышли.');
                    })
                    .error(function () {
                        $rootScope.Alert('danger', 'Произошла внутренняя ошибка. Приносим свои извенения.');
                    });
            }

            return api;
        }
    ]);