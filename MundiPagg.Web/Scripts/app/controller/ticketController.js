﻿define([
    'jquery',
    'angular',
    'underscore',
    "datepicker-i18n",
    "jquery-mask-input"
], function ($, angular, _, datepicker) {

    var ticketController = function ($scope, $ticketService) {

        angular.element(document).ready(function () {
            $("#Quantity").mask("9?9", { placeholder: "" });
            $("#DtEvent").datepicker();
            $("#Expiration").mask("99/99", { placeholder: "" });
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

            $scope.Ticket.EventId = $("#EventId").val();

            $("button.button-save").attr("disabled", "disabled").html("Aguarde....");
            $("#painelError").hide("slow");

            $ticketService.create($scope.Ticket).success(function (data) {
                if (data.success) {
                    $("#painelSuccess").show("slow");
                    $("#formStep2").hide();
                }
                else {
                    $("#painelError").show("slow");
                }

                $("button.button-save").removeAttr("disabled").html("Finalizar");

            }).error(function () {
                $("#painelError").show("slow");
                $("button.button-save").removeAttr("disabled").html("Finalizar");
            });

        };

        $scope.doBuyByOneClick = function ($event, form) {

            if (!form.validate())
                return

            var data = {
                DtEvent: $scope.Ticket.DtEvent,
                PaymentToken: $scope.Ticket.PaymentToken,
                Quantity: $scope.Ticket.Quantity,
                EventId: $("#EventId").val()
            };

            $("button.btn-comprar").attr("disabled", "disabled");
            $("a.btn-comprar-quick").attr("disabled", "disabled").html("Aguarde...");


            $ticketService.createQuick(data).success(function (data) {

                if (data.success) {
                    $("#painelSuccessQuick").show("slow");
                }
                else {
                    $("#painelErrorQuick").show("slow");
                }

                $("button.btn-comprar").removeAttr("disabled", "disabled");
                $("a.btn-comprar-quick").removeAttr("disabled", "disabled").html("Comprar com 1 Click");
                
            }).error(function () {
                $("#painelErrorQuick").show("slow");
                $("button.btn-comprar").removeAttr("disabled", "disabled");
                $("a.btn-comprar-quick").removeAttr("disabled", "disabled");
            });

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
                PaymentToken: {
                    required: true,
                }
            },
            messages: {
                Quantity: {
                    required: "Digite a quantidade",
                },
                DtEvent: {
                    required: "Selecione a data de nascimento",
                },
                PaymentToken: {
                    required: "Selecione o cartão",
                }

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

    ticketController.$inject = ["$scope", "TicketService"];
    return ticketController;

});