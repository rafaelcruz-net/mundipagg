define([
    'jquery',
    'angular',
    'underscore',
    "datepicker-i18n",
    "jquery-mask-input"
], function ($, angular, _, datepicker) {

    var ticketController = function ($scope) {

        angular.element(document).ready(function () {
            $("#Quantity").mask("9?9", { placeholder: "" });
            $("#DtEvent").datepicker();
            $("#Expiration").mask("99/9999", { placeholder: "" });
            $("#SecurityCode").mask("999?9", { placeholder: "" });
            $("#CreditCardNumber").mask("99999999999999", { placeholder: "", autoclear: false });


        });

        $scope.doStepOne = function (form) {
            if (!form.validate()) {
                return;
            }

            // Caso o formulário esteja validado passar pro proximo formulário
            $("#Step2").fadeIn("slow");
        };

        $scope.doStepTwo = function (form) {
            if (!form.validate())
                return;




        }

        /*
        *  Regras de Validação do Primeiro Passo do Criação de Conta 
        */
        $scope.validationOptions = {
            rules: {
                Quantity: {
                    required: true,
                },
                
                DtEvent: {
                    required: true,
                },
            },
            messages: {
                Quantity: {
                    required: "Digite a quantidade",
                },
                DtEvent: {
                    required: "Selecione a data de nascimento",
                },
            }
        };

        $scope.validationOptionsForPayment = {
            rules: {
                CreditCardBrand: {
                    required: true,
                },
                HolderName: {
                    required: true,
                },
                CreditCardNumber: {
                    required: true,
                },
                SecurityCode: {
                    required: true,
                },
                Expiration: {
                    required: true,
                },
            },
            messages: {
                CreditCardBrand: {
                    required: "Selecione a bandeira",
                },
                HolderName: {
                    required: "Digite a Nome",
                },
                CreditCardNumber: {
                    required: "Digite o número do cartão de crédito",
                },
                SecurityCode: {
                    required: "Digite o código de segurança do cartão",
                },
                Expiration: {
                    required: "Digite o mês e ano de expiração",
                },
            }
        };



    };

    ticketController.$inject = ["$scope"];
    return ticketController;

});