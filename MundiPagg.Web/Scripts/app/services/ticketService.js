define([
    'jquery',
    'angular',
    'underscore',
], function ($, angular, _) {

    var ticketService = function ($http) {

        this.create = function (data) {
            var url = "/ticket/save";
            return $http({
                method: 'POST',
                url: url,
                headers: {
                    'X-CSRFToken': $("input[name='__RequestVerificationToken']").length > 0 ? $("input[name='__RequestVerificationToken']").val() : ""
                },
                data: data
            });
        };

        this.createQuick = function (data) {
            var url = "/ticket/saveQuick";
            return $http({
                method: 'POST',
                url: url,
                headers: {
                    'X-CSRFToken': $("input[name='__RequestVerificationToken']").length > 0 ? $("input[name='__RequestVerificationToken']").val() : ""
                },
                data: data
            });
        }

    };

    ticketService.$inject = ["$http"];
    return ticketService;

});
