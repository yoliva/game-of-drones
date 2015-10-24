angular.module('gameOfDrones').
    controller('GameController', [
        '$scope', '$routeParams', 'matchDataService', function($scope, $routeParams, matchDataService) {

            var id = $routeParams.id;

            $scope.gameLogicHandler = {
                currentRound: 1,
                playerOnTurn: '',
                player1Stats: {},
                player2Stats: {},
                player1Move: '',
                player2Move: '',
                availableMoves: [],
                playedRounds: [],
                ruleId: '',
                rounds: []
            }
            matchDataService.getById(id)
                .success(function(data) {

                    //initialize player initial info
                    $scope.gameLogicHandler.ruleId = data.ruleId;

                    $scope.gameLogicHandler.player1Stats = {
                        name: data.playersStatses[0].player.name,
                        winnerRounds: 0,
                        loserRounds: 0,
                        drawRounds: 0,
                    };
                    $scope.gameLogicHandler.player2Stats = {
                        name: data.playersStatses[1].player.name,
                        winnerRounds: 0,
                        loserRounds: 0,
                        drawRounds: 0
                    };
                    $.each(data.availableMoves, function(index, item) {
                        $scope.gameLogicHandler.availableMoves.push({
                            value: item,
                            text: item
                        });
                    });

                    $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player1Stats.name;
                });

            $scope.isPlayer1Turn = function() {
                return $scope.gameLogicHandler.player1Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.isPlayer2Turn = function() {
                return $scope.gameLogicHandler.player2Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.player1MakeMove = function() {
                $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player2Stats.name;
            }

            $scope.player2MakeMove = function() {


                //call to the service and process the response
                var roundMoves = {
                    ruleId: $scope.gameLogicHandler.ruleId,
                    player1Move: $scope.gameLogicHandler.player1Move,
                    player2Move: $scope.gameLogicHandler.player2Move
                };
                matchDataService.evalRound(roundMoves).success(function(data) {
                    //if the game must continue
                    switch(data.result) {
                        case 'Player1Wins':
                            $scope.gameLogicHandler.player1Stats.winnerRounds++;
                            $scope.gameLogicHandler.player2Stats.loserRounds++;
                            break;
                        case 'Player2Wins':
                            $scope.gameLogicHandler.player1Stats.loserRounds++;
                            $scope.gameLogicHandler.player2Stats.winnerRounds++;
                        case 'Draw':
                            $scope.gameLogicHandler.player1Stats.drawRounds++;
                            $scope.gameLogicHandler.player2Stats.drawRounds++;
                    }

                    //push data if we have a winner

                    $scope.gameLogicHandler.rounds.push({
                        player1Move: $scope.gameLogicHandler.player1Move,
                        player2Move: $scope.gameLogicHandler.player2Move,
                        roundResult: data.result
                    });
                    $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player1Stats.name;
                    $scope.gameLogicHandler.currentRound++;
                });



            }
        }
    ]);