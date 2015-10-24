angular.module('gameOfDrones').
    controller('StartGameController', ['$scope', '$location', 'matchDataService', function ($scope, $location, matchDataService) {

        $scope.gameSettings = {};
        $scope.imageUrl = '/Content/Images/events.png';

        $scope.startGame = function () {

            var newMatch = {
                p1Name: $scope.gameSettings.player1Name,
                p2Name: $scope.gameSettings.player2Name,
            };

            matchDataService.create(newMatch)
                .success(function(data) {
                    console.log(data);
                    $location.path('/game/'+data.id);
                });
        }
    }
]);