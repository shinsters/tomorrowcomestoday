using TechTalk.SpecFlow;

namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;
    using TomorrowComesToday.Tests.Helpers;

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

    }
}
