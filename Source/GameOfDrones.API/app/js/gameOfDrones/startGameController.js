angular.module('gameOfDrones').
    controller('StartGameController', ['$scope', function ($scope) {

        $scope.gameSettings = {};
        $scope.imageUrl = '/Content/Images/events.png';

        $scope.startGame = function () {
            alert($scope.gameSettings.player1Name + ' - ' + $scope.gameSettings.player2Name);
        }
    }
]);