angular.module('app')
    .factory('matchDataService', [
        '$http', 'config', function($http, config, $q) {

            var eventApiUrl = config.apiUrl;
            var endPoint = "matches/";
            var url = eventApiUrl + endPoint;

            var service = {};

            service.getAll = function() {
                return $http.get(url + 'getAll');
            };

            service.getById = function(id) {
                return $http.get(url + 'match/' + id);
            };

            service.create = function(match) {
                return $http.post(url + 'create', match);
            };

            service.evalRound = function(data) {
                return $http.post(url + 'evalRound', data);
            };

            service.putStats = function (data) {
                return $http.put(url + 'matchStats/', data);
            }

            service.getWinner = function (id) {
                
                return $http.get(url + 'winner/' + id);
            }

            return service;
        }
    ]);

angular.module('app')
    .factory('ruleDataService', [
        '$http', 'config', function($http, config, $q) {

            var eventApiUrl = config.apiUrl;
            var endPoint = "rules/";
            var url = eventApiUrl + endPoint;

            var service = {};

            service.getAll = function() {
                return $http.get(url + 'getAll');
            };

            return service;
        }
    ]);

angular.module('app')
    .factory('playerDataService', [
        '$http', 'config', function ($http, config, $q) {

            var eventApiUrl = config.apiUrl;
            var endPoint = "rules/";
            var url = eventApiUrl + endPoint;

            var service = {};
        }
    ]);