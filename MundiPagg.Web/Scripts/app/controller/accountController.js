﻿define([
    'jquery',
    'angular',
    'underscore',
    "datepicker-i18n",
    "jquery-mask-input"  
], function ($, angular, _, datepicker) {
    
    var accountController = function ($scope, $stateService, $accountService) {

        angular.element(document).ready(function () {
            $("#CPF").mask("99999999999", { placeholder: "" });
            $("#Birthday").datepicker();
            $("#CEP").mask("99999-999");
        });

        $scope.doStepOne = function (form) {
            if (!form.validate()) {
                return;
            }

            // Caso o formulário esteja validado passar pro proximo formulário

            $("#formStep1").fadeOut("slow", function () {
                $("#formStep2").fadeIn("slow");
            });
        };

        $scope.doStepTwo = function (form) {

            if (!form.validate())
                return;

            $accountService.create($scope.RegisterModel).success(function (data) {

            });
        }

        $scope.loadCities = function (uf) {

            $("#City").empty().append("<option value=''>Selecione</option>");

            if (uf == "")
                return;

            $stateService.getCityByState(uf).success(function (data) {
                if (data.success) {
                    _.forEach(data.content, function (e) {
                        $("#City").append("<option value='" + e.id + "'>" + e.name + "</option>");
                    });
                }
            });
        };

        /*
         *  Regras de Validação do Primeiro Passo do Criação de Conta 
         */
        $scope.validationOptions = {
            rules: {
                Name: {
                    required: true,
                },
                CPF: {
                    required: true,
                    cpf: true,

                },
                Email: {
                    required: true,
                    email: true,
                },
                Birthday: {
                    required: true,
                },
                Genre: {
                    required: true,
                },
                Password: {
                    required: true,
                    password: true
                },
                ConfirmPassword: {
                    required: true,
                    equalTo: "#Password",
                    password: true
                },
            },
            messages: {
                Name: {
                    required: "Digite o Nome",
                },
                CPF: {
                    required: "Digite o CPF",
                    cpf: "CPF inválido"
                },
                Email: {
                    required: "Digite seu Email",
                    email: "Email não está em formato válido",
                },
                Birthday: {
                    required: "Selecione a data de nascimento",
                },
                Genre: {
                    required: "Selecione o sexo",
                },
                Password: {
                    required: "Digite a senha",
                    password: "Senha deve ter de 6 a 10 letras e só pode conter letras, números ou os caracteres @, #, %, $, !, &, *"
                },
                ConfirmPassword: {
                    required: "Digite a confirmação de senha",
                    equalTo: "A confirmação de senha deve ser igual a senha",
                    password: "Senha deve ter de 6 a 10 letras e só pode conter letras, números ou os caracteres @, #, %, $, !, &, *"
                },
            }
        };

        $scope.validationOptionsForAddress = {
            rules: {
                CEP: {
                    required: true
                },
                Address: {
                   required: true
                },
                Complement: {
                    required: true
                },
                Neighbor: {
                    required: true
                },
                State: {
                    required: true
                },
                City: {
                    required: true
                },
            },
            messages: {
                CEP: {
                    required: "Digite o CEP"
                },
                Address: {
                    required: "Digite o Logradouro"
                },
                Complement: {
                    required: "Digite o Complemento"
                },
                Neighbor: {
                    required: "Digite o bairro"
                },
                State: {
                    required: "Selecione o estado"
                },
                City: {
                    required: "Selecione a cidade"
                },

            }
        };


    };

    accountController.$inject = ["$scope", "StateService", "AccountService"];
    return accountController;

});
