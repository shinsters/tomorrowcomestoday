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

    gameHub.client.broadcastMessage = function (message) {
        alert(message);
    }

    $.connection.hub.start().done(function () {
        gameHub.server.joinServer("hank" + Math.floor(Math.random() * 100));
    });

}); 