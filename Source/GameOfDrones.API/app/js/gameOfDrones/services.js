angular.module('app')
    .factory('matchService', [
        '$http', 'config', function($http, config, $q) {

        var eventApiUrl = config.apiUrl;
        var endPoint = "matches/";
        var url = eventApiUrl + endPoint;

        var service = {};


        $scope.match = {};
        service.getMatch = function(id) {

            var deferred = $q.defer();

            $http.get(url + id)
                .then(function(result) {
                angular.copy(result.data, match);
                    deferred.resolve();
                }, function() {
                    //ERROR
                    deferred.reject();
                });

            return deferred.promise();
        };

        service.getById = function(id) {
            return $http.get(url + id);
        };

        service.update = function(event) {
            var request = {};
            request.event = event;
            return $http.put(url + event.id, request);
        };

        service.create = function(event) {
            var request = {};
            request.event = event;
            return $http.post(url, request);
        };

        service.destroy = function(id) {
            return $http.delete(url + id);
        };

        service.getFeatured = function() {
            return $http.get(url + 'featured');
        };
        return service;
    }
]);