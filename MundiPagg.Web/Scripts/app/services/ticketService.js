define([
    'jquery',
    'angular',
    'underscore',
], function ($, angular, _) {

    var ticketService = function ($http) {

        this.create = function (data) {
            var url = "/ticket/save";
            return $http.post(url, data);
        };

    };

    ticketService.$inject = ["$http"];
    return ticketService;

});
