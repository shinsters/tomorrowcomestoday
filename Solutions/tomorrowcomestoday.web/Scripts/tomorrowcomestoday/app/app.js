var duScroll = angular.module('duScroll', []);  

// angular app
var app = angular.module('app', ['ngRoute', 'duScroll']).config(function ($routeProvider) {

    $routeProvider.when('/game', {
        templateUrl : '/Content/templates/game/game.html',
        controller : 'GameController'
    });

    $routeProvider.otherwise({
        redirectTo: '/game'
    });
});

// directives

app.directive("sendsAnswerWhenClicked", function() {
    return {
        restrict: "A",
        link: function(scope, element, attributes) {
            //var originalMessage = scope.message;
            element.bind("mousedown", function () {
                scope.sendWhiteCard(attributes.guid);
            });
        }
    }
});