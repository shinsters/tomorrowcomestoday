namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
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
        [Given(@"I have a started game with the id '(.*)' containing following players:")]
        public void GivenIHaveAStartedGameWithTheIdContainingFollowingPlayers(string gameGuid, Table table)
        {
            var playerRepository = InitaliseTests.Container.Resolve<IPlayerRepository>();
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameStateRepository>();

            var playersToAdd = new List<Player>();

            // first get the players we're going to be using
            foreach (var row in table.Rows)
            {
                var playerName = row.GetString("Name");
                var player = playerRepository.GetByName(playerName);

                Assert.IsTrue(player != null, string.Format("No player by name {0} was found", playerName));

                playersToAdd.Add(player);
            }

            var guid = Guid.ParseExact(gameGuid, "D");

            var game = new GameStateBuilder()
                .AddPlayers(playersToAdd)
                .WithGuid(guid)
                .Create();

            gameStateRepository.SaveOrUpdate(game);
        }

        [Then(@"I see the game '(.*)' is in state '(.*)'")]
        public void ThenISeeTheGameIsInState(string gameGuidAsString, string stateAsString)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameStateRepository>();

            var gameGuid = Guid.ParseExact(gameGuidAsString, "D");

            // maybe this shouldn't be a bool on the game state, but an enum with more options.
            var gameActivityState = stateAsString == "Active";

            var game = gameStateRepository.GetByGuid(gameGuid);

            Assert.IsNotNull(game, string.Format("Game with guid {0} was not found", gameGuidAsString));

            Assert.IsTrue(
                game.IsActive == gameActivityState,
                "The state of the game {0} was not {1}",
                gameGuidAsString, 
                stateAsString);
        }

        [Given(@"the game '(.*)' has a white deck of '(.*)' cards")]
        public void GivenTheGameHasAWhiteDeckOfCards(string gameGuidAsString, int cardsInWhiteDeck)
        {
            var gameService = InitaliseTests.Container.Resolve<IGameService>();
            var gameGuid = Guid.ParseExact(gameGuidAsString, "D");

            gameService.DealWhiteStart(cardsInWhiteDeck, gameGuid);
        }

        [Then(@"I see the game '(.*)' players are in the following state:")]
        public void ThenISeeTheGamePlayersAreInTheFollowingState(string gameGuidAsString, Table table)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameStateRepository>();
            var playerRepository = InitaliseTests.Container.Resolve<IPlayerRepository>();

            var gameGuid = Guid.ParseExact(gameGuidAsString, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            // first get the players we're going to be using
            foreach (var row in table.Rows)
            {
                // first check the player is in the game as expected
                var playerName = row.GetString("Name");
                var player = playerRepository.GetByName(playerName);

                Assert.IsTrue(player != null, string.Format("No player by name {0} was found", playerName));
                Assert.IsTrue(gameState.GamePlayerStates.Any(o => o.Player.Guid == player.Guid));

                // check the players state is as expected 
                var playerState = gameState.GamePlayerStates.First(o => o.Player.Guid == player.Guid);

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

        [Then(@"I see the game '(.*)' has an active black card")]
        public void ThenISeeTheGameHasAnActiveBlackCard(string gameGuidAsString)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameStateRepository>();

            var gameGuid = Guid.ParseExact(gameGuidAsString, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);



           
        }

        [Then(@"I see the game '(.*)' has an active player")]
        public void ThenISeeTheGameHasAnActivePlayer(string gameGuidAsString)
        {
            var gameStateRepository = InitaliseTests.Container.Resolve<IGameStateRepository>();

            var gameGuid = Guid.ParseExact(gameGuidAsString, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            var countOfActivePlayers = gameState.GamePlayerStates.Count(o => o.IsActivePlayer);
            var hasCorrectAmountOfActivePlayers = countOfActivePlayers == 1;

            Assert.IsTrue(
                hasCorrectAmountOfActivePlayers,
                "Expected only one active player, but instead there were {0}",
                countOfActivePlayers);
        }


    }
}