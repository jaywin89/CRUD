(function () {
    'use strict'

    angular.module(Reference).controller("userNameController", UserNameController)
    UserNameController.$inject=["$scope", "userNameService"];

    function UserNameController($scope, userNameService) {
        var vm = this;
        vm.registerModel = {};
        vm.loginModel = {};
        vm.register = _register;
        vm.login = _login;
        vm.goToRegister = _goToRegister;

        function _goToRegister() {
            setTimeout(function () { window.location.href = 'http://localhost:65257/Home/MemberRegister'; }, 500);
        }

        function _login() {
            userNameService.login(vm.loginModel)
                .then(function (data) {
                    console.log(data)
                    //setTimeout(function () { window.location.href = 'http://localhost:65257/Home/HomePage'; }, 500);
                    if (data.isSuccessful == false) {
                        alert("Email is incorrect");
                    } else {
                        setTimeout(function () { window.location.href = 'http://localhost:65257/Home/HomePage'; }, 500);
                    }
                }).catch(function (err) {
                    console.log(err)
                })
        }

        function _register() {
            console.log(vm.registerModel);
            if (vm.registerModel.password != vm.registerModel.confirmPassword) {
                vm.matchingPassword = true;
            }
            else {
                vm.matchingPassword = false;
            userNameService.register(vm.registerModel)
                .then(function (data) {
                    console.log(data)
                    vm.registerSuccess = true;
                    setTimeout(function () { window.location.href = 'http://localhost:65257/Home/MemberLogin'; }, 2000);
                }).catch(function (err) {
                    console.log(err)
                })
            }
        }

    }
})();