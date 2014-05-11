// define controllers
angular.module("app").controller('GameController', function ($scope, $location, $anchorScroll) {
    // Reference the auto-generated proxy for the hub.  
    var gameHub = $.connection.gameHub;

    // Next chat message to send to client
    $scope.newChatMessage = "";
    $scope.chatMessages = [];

    $scope.cardsInHand = [];
    $scope.cardsInTopRow = [];
    $scope.cardsInBottomRow = [];

    $scope.blackCardText = "";
    $scope.players = [];
    $scope.playerGameGuid = "";

    // bind to local dom events
    $scope.sendChatMessage = function () {
        gameHub.server.sendChatMessage($scope.newChatMessage);
        $scope.newChatMessage = "";
    }

    /// Gets a new chat message and binds to list
    gameHub.client.getChatMessage = function (message) {
        $scope.chatMessages.push(message);
        $scope.$apply();
        var chatBody = angular.element(document.getElementById('chat-body'));
        var chatBoxBottom = angular.element(document.getElementById('chat-box-bottom'));
        chatBody.scrollTo(chatBoxBottom, 0, 1000);
    }

    /// Send a card back to the server
    $scope.sendWhiteCard = function(cardGuid) {
        gameHub.server.sendWhiteCard(cardGuid);
    }

    /// Called when user has been joined to a game, contains state of game
    gameHub.client.sendInitialState = function (gameInitialStateViewModel) {

        // to split cards in hand in two for view
        var rowCounter = 1;
        angular.forEach(gameInitialStateViewModel.DealtCards, function (item) {
            $scope.cardsInHand.push(item);
            if (rowCounter <= 5) {
                $scope.cardsInTopRow.push(item);
            } else {
                $scope.cardsInBottomRow.push(item);
            }
            rowCounter++;
        });

        // grab out the players
        angular.forEach(gameInitialStateViewModel.PlayerNames, function (item) {
            $scope.players.push(item);
        });

        $scope.blackCardText = gameInitialStateViewModel.BlackCardText;
        $scope.playerGameGuid = gameInitialStateViewModel.PlayerInGameGuid;

        $scope.$apply();
    }

    $.connection.hub.start().done(function () {
        gameHub.server.joinServer("hank" + Math.floor(Math.random() * 100));
    });
});