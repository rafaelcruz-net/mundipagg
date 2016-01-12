define([
    'jquery',
    'angular',
    'underscore',
], function ($, angular, _) {

    var stateService = function ($http) {

        this.getCityByState = function (uf) {
            var url = "/state/" + uf + "/cities";
            return $http.post(url);
        };

    };

    stateService.$inject = ["$http"];
    return stateService;

});
