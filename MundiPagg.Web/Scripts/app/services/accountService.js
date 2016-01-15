define([
    'jquery',
    'angular',
    'underscore',
], function ($, angular, _) {

    var accountService = function ($http) {

        this.create = function (data) {
            var url = "/account/save";
            return $http({
                method: 'POST',
                url: url,
                headers: {
                    'X-CSRFToken': $("input[name='__RequestVerificationToken']").length > 0 ? $("input[name='__RequestVerificationToken']").val() : ""
                },
                data: data
            });
        };

        this.login = function (data) {
            var url = "/account/login";
            return $http({
                method: 'POST',
                url: url,
                headers: {
                    'X-CSRFToken': $("input[name='__RequestVerificationToken']").length > 0 ? $("input[name='__RequestVerificationToken']").val() : ""
                },
                data: data
            });
        };


    };

    accountService.$inject = ["$http"];
    return accountService;

});
