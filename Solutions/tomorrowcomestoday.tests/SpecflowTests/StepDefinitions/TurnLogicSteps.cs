using TechTalk.SpecFlow;

namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Linq;

    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

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
            newActivePlayer.PlayerState = PlayerState.IsActivePlayerSelecting;
        }

        [Given(@"the following players have played an answer card:")]
        public void GivenTheFollowingPlayersHavePlayedAnAnswerCard(Table table)
        {
            var playerRepository = TestKernel.Container.Resolve<IPlayerRepository>();
            var gameRepository = TestKernel.Container.Resolve<IGameRepository>();
            var game = gameRepository.GetByGuid(CommonConcepts.TEST_GAME_GUID);

            foreach (var row in table.Rows)
            {
                var name = row.GetString("Name");
                var player = playerRepository.GetByName(name);

                var gamePlayer = game.GamePlayers.First(o => o.Player.Guid == player.Guid);
                
            }
        }


        [Then(@"I see the card tsar is unable to see the answer cards")]
        public void ThenISeeTheCardTsarIsUnableToSeeTheAnswerCards()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
