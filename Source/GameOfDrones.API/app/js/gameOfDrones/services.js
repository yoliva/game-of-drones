angular.module('app')
    .factory('matchDataService', [
        '$http', 'config', function($http, config) {

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
        '$http', 'config', function($http, config) {

            var eventApiUrl = config.apiUrl;
            var endPoint = "rules/";
            var url = eventApiUrl + endPoint;

            var service = {};

            service.getAll = function() {
                return $http.get(url + 'getAll');
            };

            service.updateCurrent = function (id) {
                return $http.put(url +'updateDefault/' + id);
            };

            service.create = function (data) {
                return $http.post(url + 'create/',data);
            };

            return service;
        }
    ]);

angular.module('app')
    .factory('playerDataService', [
        '$http', 'config', function($http, config, $q) {

            var eventApiUrl = config.apiUrl;
            var endPoint = "players/";
            var url = eventApiUrl + endPoint;

            var service = {};

            service.getStats = function(name) {
                return $http.get(url + 'stats/' + name);
            };

            service.getAll = function() {
                return $http.get(url + 'allPlayers');
            };

            service.getComparissonStats = function(player1Name,player2Name) {
                return $http.get(url+'statsComparisson/'+player1Name+'/'+player2Name);
            };

            return service;
        }
    ]);