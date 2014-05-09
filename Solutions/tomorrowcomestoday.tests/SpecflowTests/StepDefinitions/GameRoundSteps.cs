﻿namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    using Table = TechTalk.SpecFlow.Table;

    /// <summary>
    /// Steps for the game round.
    /// </summary>
    [Binding]
    public class GameRoundSteps
    {

        /// <summary>
        /// The GUID for our test games as their identifier
        /// </summary>
        public const string TEST_GAME_GUID = "F4909379-AF76-418E-873D-E575A8BA3233";


        [Given(@"I have a game with following players:")]
        public void GivenIHaveAStartedGameWithTheIdContainingFollowingPlayers(Table table)
        {
            var playerRepository = InitaliseTests.Container.Resolve<IPlayerRepository>();
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameRepository>();

            var playersToAdd = new List<Player>();

            // first get the players we're going to be using
            foreach (var row in table.Rows)
            {
                var playerName = row.GetString("Name");
                var player = playerRepository.GetByName(playerName);

                Assert.IsTrue(player != null, string.Format("No player by name {0} was found", playerName));

                playersToAdd.Add(player);
            }

            var guid = Guid.ParseExact(TEST_GAME_GUID, "D");

            var game = new GameBuilder()
                .AddPlayers(playersToAdd)
                .WithGuid(guid)
                .Create();

            gameStateRepository.SaveOrUpdate(game);
        }

        [Then(@"I see the game is in state '(.*)'")]
        public void ThenISeeTheGameIsInState( string stateAsString)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameRepository>();

            var gameGuid = Guid.ParseExact(TEST_GAME_GUID, "D");

            // maybe this shouldn't be a bool on the game state, but an enum with more options.
            var gameActivityState = stateAsString == "Active";

            var game = gameStateRepository.GetByGuid(gameGuid);

            Assert.IsNotNull(game, string.Format("Game with guid {0} was not found", TEST_GAME_GUID));

            Assert.IsTrue(
                game.IsActive == gameActivityState,
                "The state of the game {0} was not {1}",
                TEST_GAME_GUID, 
                stateAsString);
        }

        [Given(@"I have a limited white deck size of '(.*)' cards")]
        public void GivenIHaveALimitedWhiteDeckSizeOfCards(int limitedDeckSize)
        {
            var cardRepository = InitaliseTests.Container.Resolve<ICardRepository>();
            cardRepository.SetCustomDeckSize(limitedDeckSize);
        }


        [Given(@"the game is started")]
        public void GivenTheGameHasAWhiteDeckOfCards()
        {
            var gameService = InitaliseTests.Container.Resolve<IGameService>();
            var gameGuid = Guid.ParseExact(TEST_GAME_GUID, "D");

            gameService.DealRound(gameGuid);
        }

        [Then(@"I see the game players are in the following state:")]
        public void ThenISeeTheGamePlayersAreInTheFollowingState(Table table)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameRepository>();
            var playerRepository = InitaliseTests.Container.Resolve<IPlayerRepository>();

            var gameGuid = Guid.ParseExact(TEST_GAME_GUID, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            // first get the players we're going to be using
            foreach (var row in table.Rows)
            {
                // first check the player is in the game as expected
                var playerName = row.GetString("Name");
                var player = playerRepository.GetByName(playerName);

                Assert.IsTrue(player != null, string.Format("No player by name {0} was found", playerName));
                Assert.IsTrue(gameState.GamePlayers.Any(o => o.Player.Guid == player.Guid));

                // check the players state is as expected 
                var playerState = gameState.GamePlayers.First(o => o.Player.Guid == player.Guid);

                var pointsExpected = row.GetInt32("Points");

                Assert.IsTrue(
                    playerState.Points == pointsExpected,
                    "Expected player {0} to have {1} points but actually had {2}",
                    playerName,
                    pointsExpected, 
                    playerState.Points);

                var cardsInHandExpected = row.GetInt32("Cards in hand");

                Assert.IsTrue(
                    playerState.CardsInHand.Count == cardsInHandExpected,
                    "Expected player {0} to have {1} cards in hand but actually had {2}",
                    playerName,
                    cardsInHandExpected,
                    playerState.CardsInHand.Count);
            }
        }

        [Then(@"I see the game has an active black card")]
        public void ThenISeeTheGameHasAnActiveBlackCard()
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameRepository>();

            var gameGuid = Guid.ParseExact(TEST_GAME_GUID, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            var amountofActiveBlackCards = gameState.BlackCardsInDeck.Count(o => o.IsCurrentCard);
            var expectedAmountOfBlackCards = amountofActiveBlackCards == 1;

            Assert.IsTrue(
                expectedAmountOfBlackCards,
                "Expected just one black card delt, but actually saw {0}",
                amountofActiveBlackCards);
        }

        [Then(@"I see the game has an active player")]
        public void ThenISeeTheGameHasAnActivePlayer()
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameRepository>();

            var gameGuid = Guid.ParseExact(TEST_GAME_GUID, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            var countOfActivePlayers = gameState.GamePlayers.Count(o => o.IsActivePlayer);
            var hasCorrectAmountOfActivePlayers = countOfActivePlayers == 1;

            Assert.IsTrue(
                hasCorrectAmountOfActivePlayers,
                "Expected only one active player, but instead there were {0}",
                countOfActivePlayers);
        }
    }
}