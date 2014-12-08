angular.module('Billboard')
    .filter('fromNow', function () {
        return function (date) {
            return moment(date, 'DD.MM.YYYY HH:mm:ss').locale('ru').fromNow();
        };
    });