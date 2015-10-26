angular.module('gameOfDrones').
    controller('GameController', [
        '$scope', '$routeParams', '$location', 'matchDataService', 'playerDataService', function($scope, $routeParams, $location, matchDataService, playerDataService) {

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
                ruleName: '',
                rounds: []
            }
            matchDataService.getById(id)
                .success(function(data) {

                    //initialize player initial info
                    $scope.gameLogicHandler.ruleId = data.ruleId;
                $scope.gameLogicHandler.ruleName = data.ruleName;

                    $scope.gameLogicHandler.player1Stats = {
                        name: data.playersStatses[0].player.name,
                        wonRounds: 0,
                        lostRounds: 0,
                        tiedRounds: 0,
                    };
                    $scope.gameLogicHandler.player2Stats = {
                        name: data.playersStatses[1].player.name,
                        wonRounds: 0,
                        lostRounds: 0,
                        tiedRounds: 0
                    };
                    $.each(data.availableMoves, function(index, item) {
                        $scope.gameLogicHandler.availableMoves.push({
                            value: item,
                            text: item
                        });
                    });

                    $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player1Stats.name;

                    playerDataService.getStats($scope.gameLogicHandler.player1Stats.name)
                        .success(function(data) {
                            $scope.gameLogicHandler.player1Stats.previousPerfomance = data.wins + '-' +data.loses;
                        });
                    playerDataService.getStats($scope.gameLogicHandler.player2Stats.name)
                        .success(function(data) {
                            $scope.gameLogicHandler.player2Stats.previousPerfomance = data.wins + '-' + data.loses;
                        });
                });

            $scope.isPlayer1Turn = function() {
                return $scope.gameLogicHandler.player1Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.isPlayer2Turn = function() {
                return $scope.gameLogicHandler.player2Stats.name == $scope.gameLogicHandler.playerOnTurn;
            };

            $scope.player1MakeMove = function() {
                if ($scope.gameLogicHandler.player1Move == '' || $scope.gameLogicHandler.player1Move == undefined) {
                    swal("Info!", "You must choose a valid move from the list.", "info");
                    return;
                }
                $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player2Stats.name;
            }

            $scope.player2MakeMove = function() {
                if ($scope.gameLogicHandler.player2Move == '' || $scope.gameLogicHandler.player2Move == undefined) {
                    swal("Info!", "You must choose a valid move from the list.", "info");
                    return;
                }

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
                        case 'Player1Won':
                            $scope.gameLogicHandler.player1Stats.wonRounds++;
                            $scope.gameLogicHandler.player2Stats.lostRounds++;
                            break;
                        case 'Player2Won':
                            $scope.gameLogicHandler.player1Stats.lostRounds++;
                            $scope.gameLogicHandler.player2Stats.wonRounds++;
                            break;
                        case 'Tie':
                            $scope.gameLogicHandler.player1Stats.tiedRounds++;
                            $scope.gameLogicHandler.player2Stats.tiedRounds++;
                            break;
                        }

                        //check for game end condition

                        if (checkEndGameCondition()) {
                            //push data if we have a winner
                            putMatchStats();
                        } else {

                            //store round and start new round
                            $scope.gameLogicHandler.rounds.push({
                                player1Move: $scope.gameLogicHandler.player1Move,
                                player2Move: $scope.gameLogicHandler.player2Move,
                                roundResult: data.roundText
                            });

                            //restart round settings
                            $scope.gameLogicHandler.player1Move = '';
                            $scope.gameLogicHandler.player2Move = '';
                            $scope.gameLogicHandler.playerOnTurn = $scope.gameLogicHandler.player1Stats.name;

                            $scope.gameLogicHandler.currentRound++;
                        }
                    });
            }

            $scope.displayRuleDefinition = function () {

            }

            function putMatchStats() {
                var matchData = {
                    matchId: id,
                    playersStats: [
                        {
                            playerName: $scope.gameLogicHandler.player1Stats.name,
                            wonRounds: $scope.gameLogicHandler.player1Stats.wonRounds,
                            lostRounds: $scope.gameLogicHandler.player1Stats.lostRounds,
                            tiedRounds: $scope.gameLogicHandler.player1Stats.tiedRounds
                        },
                        {
                            playerName: $scope.gameLogicHandler.player2Stats.name,
                            wonRounds: $scope.gameLogicHandler.player2Stats.wonRounds,
                            lostRounds: $scope.gameLogicHandler.player2Stats.lostRounds,
                            tiedRounds: $scope.gameLogicHandler.player2Stats.tiedRounds
                        }
                    ]
                };

                matchDataService.putStats(matchData)
                    .success(function(data) {
                        $location.path('/finish/' + id);
                    });
            }

            function checkEndGameCondition() {
                return $scope.gameLogicHandler.player1Stats.wonRounds == 3 || $scope.gameLogicHandler.player2Stats.wonRounds == 3;
            }
        }
    ]);