angular.module('gameOfDrones').
    controller('EndGameController', ['$scope', '$location', '$routeParams', 'matchDataService', function ($scope, $location, $routeParams, matchDataService) {

        $scope.imageUrl = '/Content/Images/events.png';

        $scope.winner = '';

        var id = $routeParams.id;
        matchDataService.getWinner(id)
            .success(function(data) {
                $scope.winner = data.winner.name;
        });

    }
    ]);