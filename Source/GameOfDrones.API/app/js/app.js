angular.module('app', ['ngRoute', 'gameOfDrones'])
    .config(function($routeProvider) {
        $routeProvider.when('/start', {
            templateUrl: '/app/partials/gameOfDrones/start.html',
            controller: 'StartGameController'
        });
        $routeProvider.when('/finish', {
            templateUrl: '/app/partials/gameOfDrones/finish.html',
        });
        $routeProvider.when('/game/:id', {
            templateUrl: '/app/partials/gameOfDrones/game.html',
            controller: 'GameController'
        });
        $routeProvider.when('/stats', {
            templateUrl: '/app/partials/gameOfDrones/stats.html',
            controller: 'StatsController'
        });
        $routeProvider.when('/configuration', {
            templateUrl: '/app/partials/gameOfDrones/configuration.html',
            controller: 'ConfigurationController'
        });
        
        $routeProvider.otherwise({
            redirectTo: '/start'
        });
    })
    .constant("config", {
        apiUrl: 'http://localhost:6092/api/v1/'
    });

angular.module('gameOfDrones', []);