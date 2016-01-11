define([
    'jquery',
    'angular',
    'underscore',
    'ngValidate'
], function ($, angular, _) {

    var configuration = function ($validatorProvider) {
        $validatorProvider.addMethod("cpf", function (value, element) {
            value = jQuery.trim(value);
            value = value.replace('.', '');
            value = value.replace('.', '');
            cpf = value.replace('-', '');
            while (cpf.length < 11) cpf = "0" + cpf;
            var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
            var a = [];
            var b = new Number;
            var c = 11;
            for (i = 0; i < 11; i++) {
                a[i] = cpf.charAt(i);
                if (i < 9) b += (a[i] * --c);
            }

            if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11 - x }
            b = 0;
            c = 11;
            for (y = 0; y < 10; y++) b += (a[y] * c--);
            if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11 - x; }

            var retorno = true;
            if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) retorno = false;

            return this.optional(element) || retorno;

        }, "CPF Inválido");

        $validatorProvider.addMethod("password", function (value, element) {
            return this.optional(element) || /^([0-9a-zA-Z@#%$!&*]{6,10})$/.test(value);
        }, "Senha deve ter de 6 a 10 letras e só pode conter letras, números ou os caracteres @, #, %, $, !, &, *");

    }

    var app = angular.module('mundipagg_app', ['ngValidate']).config(configuration);
    return app;
});