angular.module('gameOfDrones').
    controller('StatsController', [
        '$scope', 'playerDataService', function($scope, playerDataService) {

            $scope.currentPLayer = {};

            $scope.availablePlayers = [];
            $scope.statsInfo = [];

            $scope.player1Stats = {};
            $scope.player2Stats = {};

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
                    $scope.currentPlayer.wons = data.wins;
                    $scope.currentPlayer.roundsWon = data.wonRounds;
                    $scope.currentPlayer.roundsTied = data.tiedRounds;
                    $scope.currentPlayer.lost = data.loses;
                    $scope.currentPlayer.roundLost = data.loseRounds;

                });
            }

            $scope.faceToFaceComparisson = function () {

                if ($scope.player1Stats.name == undefined || $scope.player1Stats.name == '' || $scope.player2Stats.name == undefined || $scope.player2Stats.name == '') {
                    swal("Error!", "You must two players to compare.", "error");
                    return;
                }
                if ($scope.player2Stats.name == $scope.player1Stats.name) {
                    swal("Error!", "You must select two different players to compare.", "error");
                    return;
                }

                playerDataService.getComparisonStats($scope.player1Stats.name, $scope.player2Stats.name)
                    .success(function(data) {
                        $scope.statsInfo = [];
                        $scope.statsInfo.push(data.player1);
                        $scope.statsInfo.push(data.player2);
                });
            }
        }
    ]);