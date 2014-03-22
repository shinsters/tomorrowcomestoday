namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate.Mapping;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    using Table = TechTalk.SpecFlow.Table;

    /// <summary>
    /// Steps for the game round.
    /// </summary>
    [Binding]
    public class GameRoundSteps
    {
        [Given(@"I have a started game with the id '(.*)' containing following players:")]
        public void GivenIHaveAStartedGameWithTheIdContainingFollowingPlayers(string gameId, Table table)
        {
            var playerRepository = InitaliseTests.Container.Resolve<IPlayerRepository>();

            var playersToAdd = new List<Player>();

            // first get the players we're going to be using
            foreach (var row in table.Rows)
            {
                var playerName = row.GetString("Name");
                var player = playerRepository.GetByName(playerName);

                Assert.IsTrue(player != null, string.Format("No player by name {0} was found", playerName));

                playersToAdd.Add(player);
            }

            // I have no idea what the second parametre here wants. 
            var guid = Guid.ParseExact(gameId, "N");

            var game = new GameStateBuilder()
                .AddPlayers(playersToAdd)
                .WithGuid(guid)
                .Create();
        }
    }
}