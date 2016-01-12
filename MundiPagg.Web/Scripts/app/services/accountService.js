﻿define([
    'jquery',
    'angular',
    'underscore',
], function ($, angular, _) {

    var accountService = function ($http) {

        this.create = function (data) {
            var url = "/account/save";
            return $http.post(url, data);
        };

    };

    accountService.$inject = ["$http"];
    return accountService;

});
