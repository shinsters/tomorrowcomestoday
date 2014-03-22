namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System.Collections.Generic;

    using FluentNHibernate.Conventions.Inspections;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// Steps for the game round.
    /// </summary>
    [Binding]
    public class GameRoundSteps
    {
        [Given(@"I have a started game")]
        public void GivenIHaveAStartedGame()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have the following players:")]
        public void GivenIHaveTheFollowingPlayers(Table table)
        {

            foreach (var row in table.Rows)
            {
                var name = row.GetString("Name");

                var player = new PlayerBuilder()
                    .Named(name)
                    .Create();
            }
        }
    }
}