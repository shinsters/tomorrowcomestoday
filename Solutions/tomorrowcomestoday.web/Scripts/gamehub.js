// angular app
var app = angular.module("app", ['ngRoute']).config(function($routeProvider) {

    $routeProvider.when('/game', {
        templateUrl : '/templates/game/',
        controller : 'GameController'
    });

    $routeProvider.otherwise({
        redirectTo: '/game'
    });
});

app.controller('GameController', function ($scope) {
    $scope.message = 'hi';
});

// signalr hub
$(function () {

        // Reference the auto-generated proxy for the hub.  
        var gameHub = $.connection.gameHub;

        gameHub.client.broadcastMessage = function (message) {
            alert(message);
        }

        $.connection.hub.start().done(function () {
            //gameHub.server.send("name", "message");
            gameHub.server.joinServer("hank");
        });
});