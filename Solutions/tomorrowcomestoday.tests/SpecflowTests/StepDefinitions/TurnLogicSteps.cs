using TechTalk.SpecFlow;

namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate.Mapping;

    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;
    using TomorrowComesToday.Tests.Helpers;

    using Table = TechTalk.SpecFlow.Table;

    [Binding]
    public class TurnLogicSteps
    {
        [Given(@"the card tsar is currently '(.*)'")]
        public void GivenTheCardTsarIsCurrently(string playerName)
        {
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var playerRepository = TestKernel.Container.Resolve<IPlayerRepository>();
            var player = playerRepository.GetByName(playerName);

            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            // an active player is created when a game is, just un set it and set the needed one
            var disablingActivePlayer = game.GamePlayers.First(o => o.PlayerState == PlayerState.IsActivePlayerWaiting);
            disablingActivePlayer.PlayerState = PlayerState.IsNormalPlayerSelecting;

            var newActivePlayer = game.GamePlayers.First(o => o.Player.Guid == player.Guid);
            newActivePlayer.PlayerState = PlayerState.IsActivePlayerWaiting;
        }

        [Given(@"the following players have played an answer card:")]
        public void GivenTheFollowingPlayersHavePlayedAnAnswerCard(Table table)
        {
            var playerRepository = TestKernel.Container.Resolve<IPlayerRepository>();
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var gameService = TestKernel.Container.Resolve<IGameService>();

            var gameGuid = Guid.ParseExact(CommonConcepts.TEST_GAME_GUID, "D");
            var game = gameRepository.GetByGuid(gameGuid);

            foreach (var row in table.Rows)
            {
                var name = row.GetString("Name");
                var player = playerRepository.GetByName(name);

                var gamePlayer = game.GamePlayers.First(o => o.Player.Guid == player.Guid);
                var randomWhiteCard = gamePlayer.WhiteCardsInHand.ToList().GetRandomItem();

                gameService.PlayWhiteCard(gameGuid, gamePlayer.GamePlayerGuid, randomWhiteCard.GameCardGuid);
            }
        }

        [Then(@"I see the card tsar is able to see the answer cards")]
        public void ThenISeeTheCardTsarIsAbleToSeeTheAnswerCards()
        {
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            var activePlayer = game.GamePlayers.Where(o => o.PlayerState == PlayerState.IsActivePlayerSelecting).ToList();
            var activePlayerExpectedAmount = activePlayer.Count() == 1;
            Assert.IsTrue(activePlayerExpectedAmount, "Expected 1 active selecting player, but instead found {0}", activePlayer.Count());
        }


        [Then(@"I see the card tsar is able to not see the answer cards")]
        public void ThenISeeTheCardTsarIsAbleToNotSeeTheAnswerCards()
        {
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            var activePlayer = game.GamePlayers.Where(o => o.PlayerState == PlayerState.IsActivePlayerWaiting).ToList();
            var activePlayerExpectedAmount = activePlayer.Count() == 1;
            Assert.IsTrue(activePlayerExpectedAmount, "Expected 1 active selecting player, but instead found {0}", activePlayer.Count());
        }

        [Given(@"the card tsar selects an answer card")]
        public void GivenTheCardTsarSelectsAnAnswerCard()
        {
            var gameService = TestKernel.Container.Resolve<IGameService>();
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            var playedWhiteCards = new List<GameCard>();

            foreach (var gamePlayer in game.GamePlayers)
            {
                playedWhiteCards.AddRange(gamePlayer.WhiteCardsInHand.Where(o => o.GameCardState == GameCardState.IsInPlay));
            }

            var randomWhiteWinningCard = playedWhiteCards.ToList().GetRandomItem();

            var activePlayer = game.GamePlayers.First(o => o.PlayerState == PlayerState.IsActivePlayerSelecting);

            gameService.SelectWhiteCardAsWinner(game.GameGuid, activePlayer.GamePlayerGuid, randomWhiteWinningCard.GameCardGuid);
        }

        [Then(@"I see the player who played the winning card has a point")]
        public void ThenISeeThePlayerWhoPlayedTheWinningCardHasAPoint()
        {
            var gameService = TestKernel.Container.Resolve<IGameService>();
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            var playersWithAPoint = game.GamePlayers.Where(o => o.Points > 0);
            var expectedAmountOfPlayersWithPoint = playersWithAPoint.Count() == 1;

            Assert.IsTrue(
                expectedAmountOfPlayersWithPoint, 
                "Expected 1 player to have a point, actually had {0}", 
                playersWithAPoint);
        }
    }
}
