define([
    'jquery',
    'angular',
    'underscore',
    "datepicker-i18n",
    "jquery-mask-input"  
], function ($, angular, _, datepicker) {
    
    var accountController = function ($scope) {

        angular.element(document).ready(function () {
            $("#CPF").mask("99999999999", { placeholder: "" });
            $("#Birthday").datepicker();
        });

        $scope.doStepOne = function (form) {
            if (!form.validate()) {
                return;
            }

            // Caso o formulário esteja validado passar pro proximo formulário

            $("#formStep1").fadeOut("slow", function () {
                $("#formStep2").fadeIn("slow");
            });
        }

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

            },
            messages: {

            }
        };


    };

   

    accountController.$inject = ["$scope"];
    return accountController;

});
