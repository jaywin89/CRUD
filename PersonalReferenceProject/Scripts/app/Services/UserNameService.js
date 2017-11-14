(function () {

    angular.module(Reference).factory("userNameService", UserNameService)
    UserNameService.$inject = ["$http", "$q"];

    function UserNameService($http, $q) {
        var srv = {
            register : _register
            , login : _login
        }
        return srv;


        function _register(model) {
            return $http.post("/api/UserName", model)
                .then(function (response) {
                    return response;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }

        function _login(model){
            return $http.post("/api/UserName/login", model)
                .then(function (response) {
                    return response.data;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }

    }

})();