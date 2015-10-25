angular.module('gameOfDrones').
    controller('StatsController', [
        '$scope', 'playerDataService', function($scope, playerDataService) {

            $scope.currentPLayer = {};
            $scope.availablePlayers = [];
            playerDataService.getAll().success(function(data) {

                $.each(data, function(index, item) {
                    $scope.availablePlayers.push({
                        value: item.name,
                        text: item.name
                    });
                });

            });

            $scope.updatePlayerStats = function() {
                playerDataService.getStats($scope.currentPlayer.id).success(function(data) {
                    console.log(data);
                    $scope.currentPlayer.wons = data.wins;
                    $scope.currentPlayer.roundsWon = data.wonRounds;
                    $scope.currentPlayer.roundsTied = data.tiedRounds;
                    $scope.currentPlayer.lost = data.loses;
                    $scope.currentPlayer.roundLost = data.loseRounds;

                });
            }
        }
    ]);