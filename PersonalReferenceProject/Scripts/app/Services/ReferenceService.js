(function () {

    angular.module(Reference).factory("referenceService", ReferenceService)

    ReferenceService.$inject = ["$http", "$q"];

    function ReferenceService($http, $q) {
        var srv = {
            addReference: _addReference
            , editReference: _editReference
            , deleteReference: _deleteReference
            , getAllRefByType: _getAllRefByType
            , getCurrentRef: _getCurrentRef
            , findWeather: _findWeather
        }
        return srv;

        function _findWeather(zipcode) {
            return $http.post("/api/reference/" + zipcode)
                .then(function (response) {
                    return response.data;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }
        function _addReference(model) {
            return $http.post("/api/reference", model)
                .then(function (response) {
                    return response;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }
        function _editReference(model) {
            return $http.put("/api/reference", model)
                .then(function (response) {
                    return response;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }

        function _deleteReference(model) {
            console.log(model);
            return $http.delete("/api/reference/"+ model.id, model)
                .then(function (response) {
                    return response;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }

        function _getAllRefByType(model) {
            console.log("hi", model);
            return $http.post("/api/reference/get", model)
                .then(function (response) {
                    return response.data
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }
        function _getCurrentRef(id) {
            return $http.get("/api/reference/" + id, id)
                .then(function (response) {
                    return response.data;
                }).catch(function (err) {
                    return $q.reject(err);
                })
        }

    }

})();