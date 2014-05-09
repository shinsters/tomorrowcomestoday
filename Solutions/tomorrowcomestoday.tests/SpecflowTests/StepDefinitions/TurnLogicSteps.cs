using TechTalk.SpecFlow;

namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Linq;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    [Binding]
    public class TurnLogicSteps
    {
        [Given(@"the card tsar is currently '(.*)'")]
        public void GivenTheCardTsarIsCurrently(string playerName)
        {
            var gameStateRepository = TestKernel.Container.Resolve<IGameRepository>();
            var playerRepository = TestKernel.Container.Resolve<IPlayerRepository>();
            var player = playerRepository.GetByName(playerName);

            var gameGuid = Guid.ParseExact(CommonConcepts.TEST_GAME_GUID, "D");
            var gameState = gameStateRepository.GetByGuid(gameGuid);

            // an active player is created when a game is, just un set it and set the needed one
            var disablingActivePlayer = gameState.GamePlayers.First(o => o.IsActivePlayer);
            disablingActivePlayer.IsActivePlayer = false;

            var newActivePlayer = gameState.GamePlayers.First(o => o.Player.Guid == player.Guid);
            newActivePlayer.IsActivePlayer = true;
        }

        [Given(@"the following players have played an answer card:")]
        public void GivenTheFollowingPlayersHavePlayedAnAnswerCard(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
