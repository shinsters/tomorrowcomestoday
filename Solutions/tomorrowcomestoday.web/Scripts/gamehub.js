// angular app
var app = angular.module("app", ['ngRoute']).config(function ($routeProvider) {

    $routeProvider.when('/game', {
        templateUrl : '/Content/templates/game/game.html',
        controller : 'GameController'
    });


    $routeProvider.otherwise({
        redirectTo: '/game'
    });
});

app.controller('GameController', function ($scope) {
    // Reference the auto-generated proxy for the hub.  
    var gameHub = $.connection.gameHub;

    $scope.cardsInHand = [];
    $scope.playerNames = [];
    $scope.blackCardText = "";
    $scope.players = [];
    $scope.playerGameGuid = "";

    gameHub.client.broadcastMessage = function (message) {
    }

    /// Called when user has been joined to a game, contains state of game
    gameHub.client.sendInitialState = function (gameInitialStateViewModel) {

        // grab out the cards in hand
        angular.forEach(gameInitialStateViewModel.DealtCards, function (item) {
            $scope.cardsInHand.push(item);
        });

        // grab out the players
        angular.forEach(gameInitialStateViewModel.PlayerNames, function (item) {
            $scope.playerNames.push(item);
        });

        $scope.blackCardText = gameInitialStateViewModel.BlackCardText;
        $scope.playerGameGuid = gameInitialStateViewModel.PlayerInGameGuid;

        alert("Black card: " + $scope.blackCardText);
        alert("Player Guid: " + $scope.playerGameGuid);
    }

    $.connection.hub.start().done(function () {
        gameHub.server.joinServer("hank" + Math.floor(Math.random() * 100));
    });

}); 