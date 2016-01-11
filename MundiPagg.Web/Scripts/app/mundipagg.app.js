require.config({
    baseUrl: "/Scripts",
    paths: {
        "jquery": "vendor/jquery/jquery",
        "jquery-validation": "vendor/jquery-validation/jquery.validate",
        "jquery-mask-input": "vendor/jquery.maskedinput/jquery.maskedinput",
        "jquery-ui-core": "vendor/jquery-ui/core",
        "datepicker": "vendor/jquery-ui/datepicker",
        "datepicker-i18n": "vendor/jquery-ui/datepicker-pt-BR",
        "underscore": "vendor/underscore/underscore",
        "angular": "vendor/angular/angular",
        "ngResource": "vendor/angular-resource/angular-resource",
        "ngSanitize": "vendor/angular-sanitize/angular-sanitize",
        "ngValidate": "vendor/jpkleemans-angular-validate/angular-validate.min",
        "foundation": "vendor/foundation-sites/foundation",
    },
    shim: {
        underscore: {
            exports: "_",
        },
        jquery: {
            exports: "jquery"
        },
        "jquery-validation": {
            deps: ["jquery"],
        },
        "jquery-mask-input": {
            deps: ["jquery"],
        },
        "jquery-ui-core":{
            deps: ["jquery"],
        },
        "datepicker": {
            deps: ["jquery-ui-core"],
        },
        "datepicker-i18n": {
            deps: ["datepicker"],
        },
        angular: {
            deps: ["jquery"],
            exports: "angular"
        },
        foundation: {
            deps: ["jquery", "underscore", "angular"],
        },

        ngResource: {
            deps: [
                "angular"
            ]
        },
        ngSanitize: {
            deps: [
                "angular"
            ]
        },
        ngValidate: {
            deps: [
                "jquery-validation",
                "angular"
            ]
        }
    }
});

require(["jquery", "underscore", "datepicker-i18n", ,"foundation"], function ($, _, datepicker) {
    $(document).foundation();

    $(document).ready(function () {

    })
});