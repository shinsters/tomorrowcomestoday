﻿// define controllers
angular.module("app").controller('GameController', function ($scope) {
    // Reference the auto-generated proxy for the hub.  
    var gameHub = $.connection.gameHub;

    // Next chat message to send to client
    $scope.newChatMessage = "";
    $scope.chatMessages = [];

    $scope.cardsInHand = [];
    $scope.cardsInTopRow = [];
    $scope.cardsInBottomRow = [];
    $scope.winnerGuid = "";

    $scope.blackCardText = "";
    $scope.players = [];
    $scope.playerGameGuid = "";

    // details of the current card tsar
    $scope.activePlayerGuid = "";
    $scope.activePlayerName = "";
    
    $scope.winningPlayerGuid = "";
    $scope.winningCardGuid = "";

    // the players token
    $scope.token = "";

    // the guid of the card that has just been played
    $scope.sentCardGuids = [];
    
    // number of shown cards on the main page
    $scope.shownCards = [];

    // has this user played this turn
    $scope.hasPlayed = false;

    // bind to local dom events
    $scope.sendChatMessage = function () {
        gameHub.server.sendChatMessage($scope.token, $scope.newChatMessage);
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
    $scope.sendCard = function(cardGuid) {
        gameHub.server.sendCard($scope.token, cardGuid);
        $scope.sentCardGuids.push(cardGuid);
        $scope.hasPlayed = true;
        $scope.$apply();
    }

    /// Called when the server says we can show a card
    gameHub.client.showGameCard = function () {
        $scope.shownCards.push({'Text': "Played Card", "Guid" : ""});
        $scope.$apply();
    }

    /// Called when a player has won
    gameHub.client.sendWinner = function(winnerGuid, cardGuid) {
        $scope.winningPlayerGuid = winnerGuid;
        $scope.winningCardGuid = cardGuid;

        addPointToPlayer(winnerGuid);
        $scope.$apply();
    }

    /// Called when a new round has begun
    gameHub.client.sendNextRound = function (gameNextRoundStateViewModel) {
        // set the state
        $scope.winningPlayerGuid = "";
        $scope.activePlayerGuid = gameNextRoundStateViewModel.ActivePlayerGuid;
        $scope.blackCardText = gameNextRoundStateViewModel.BlackCardText;
        $scope.shownCards = [];
        $scope.hasPlayed = false;

        updateCurrentPlayerName();

        var newCards = gameNextRoundStateViewModel.WhiteCards;

        // set the cards, could be more efficient but JS arrays aren't fun
        angular.forEach(newCards, function (newCard) {
            angular.forEach($scope.sentCardGuids, function (cardGuid) {
                angular.forEach($scope.cardsInHand, function (cardInHand) {
                    if (cardInHand.Guid === cardGuid) {
                        var index = $scope.cardsInHand.indexOf(cardInHand);
                        $scope.cardsInHand[index].Guid = newCard.Guid;
                        $scope.cardsInHand[index].Text = newCard.Text;
                    }
                });
            });
        });

        $scope.sentCardGuids = [];

        $scope.$apply();
    }

    /// Called when all cards have been played
    gameHub.client.showAllCards = function (gameAllChosenViewModel) {
        $scope.shownCards = gameAllChosenViewModel.AnswerCards;
        $scope.$apply();
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
        $scope.activePlayerGuid = gameInitialStateViewModel.ActivePlayerGuid;
        $scope.token = gameInitialStateViewModel.Token;

        updateCurrentPlayerName();

        $scope.$apply();
   }

    $.connection.hub.start().done(function () {
        gameHub.server.joinServer("shinsters" + Math.floor(Math.random() * 100));
    });

    /// update the name of the curent player
    function updateCurrentPlayerName() {

        angular.forEach($scope.players, function (player) {
            if (player.Guid === $scope.activePlayerGuid) {
                $scope.activePlayerName = player.Name;
            }
        });
    }

    /// add a point to a players total
    function addPointToPlayer(playerGuid) {
        angular.forEach($scope.players, function (player) {
            if (player.Guid === playerGuid) {
                player.Points++;
            }
        });
    }
});

