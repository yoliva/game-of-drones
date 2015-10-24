angular.module('gameOfDrones').
    controller('GameController', [
        '$scope', '$routeParams', '$location', 'matchDataService', function ($scope, $routeParams, $location, matchDataService) {

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

            $scope.isPlayer1Turn = function isPlayer1Turn () {
                return $scope.gameLogicHandler.player1Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.isPlayer2Turn = function() {
                return $scope.gameLogicHandler.player2Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.player1MakeMove = function () {
                if ($scope.gameLogicHandler.player1Move == '')
                    return;
                $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player2Stats.name;
            }

            $scope.player2MakeMove = function() {

                if ($scope.gameLogicHandler.player2Move == '')
                    return;

                //call to the service and process the response
                var roundMoves = {
                    ruleId: $scope.gameLogicHandler.ruleId,
                    player1Move: $scope.gameLogicHandler.player1Move,
                    player2Move: $scope.gameLogicHandler.player2Move
                };
                matchDataService.evalRound(roundMoves)
                    .success(function(data) {

                        //update player stats for this round
                        switch (data.result) {
                        case 'Player1Wins':
                            $scope.gameLogicHandler.player1Stats.winnerRounds++;
                            $scope.gameLogicHandler.player2Stats.loserRounds++;
                            break;
                        case 'Player2Wins':
                            $scope.gameLogicHandler.player1Stats.loserRounds++;
                            $scope.gameLogicHandler.player2Stats.winnerRounds++;
                            break;
                        case 'Draw':
                            $scope.gameLogicHandler.player1Stats.drawRounds++;
                            $scope.gameLogicHandler.player2Stats.drawRounds++;
                            break;
                        }

                        //check for game end condition

                        if (checkEndGameCondition()) {
                            //push data if we have a winner
                            putMatchStats();                           
                        }
                        else {

                            //store round and start new round
                            $scope.gameLogicHandler.rounds.push({
                                player1Move: $scope.gameLogicHandler.player1Move,
                                player2Move: $scope.gameLogicHandler.player2Move,
                                roundResult: data.result
                            });

                            //restart round settings
                            $scope.gameLogicHandler.player1Move = '';
                            $scope.gameLogicHandler.player2Move = '';
                            $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player1Stats.name;

                            $scope.gameLogicHandler.currentRound++;
                        }
                    });
            }

            function putMatchStats() {
                var matchData = {
                    matchId: id,
                    playersStats: [
                        {
                            playerName: $scope.gameLogicHandler.player1Stats.name,
                            winnerRounds: $scope.gameLogicHandler.player1Stats.winnerRounds,
                            loserRounds: $scope.gameLogicHandler.player1Stats.loserRounds,
                            drawRounds: $scope.gameLogicHandler.player1Stats.drawRounds
                        },
                        {
                            playerName: $scope.gameLogicHandler.player2Stats.name,
                            winnerRounds: $scope.gameLogicHandler.player2Stats.winnerRounds,
                            loserRounds: $scope.gameLogicHandler.player2Stats.loserRounds,
                            drawRounds: $scope.gameLogicHandler.player2Stats.drawRounds
                        }
                    ]
                };

                matchDataService.putStats(matchData)
                    .success(function(data) {
                        $location.path('/finish/' + id);
                    });
            }

            function checkEndGameCondition() {
                return $scope.gameLogicHandler.player1Stats.winnerRounds == 3 || $scope.gameLogicHandler.player2Stats.winnerRounds == 3;
            }
        }
    ]);