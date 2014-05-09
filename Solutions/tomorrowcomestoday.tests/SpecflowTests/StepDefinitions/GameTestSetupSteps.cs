namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// Sets up basic components for tests
    /// </summary>
    [Binding]
    public class GameTestSetupSteps
    {
        [Given(@"I have an initalised back end")]
        public void GivenIHaveAnInitalisedBackEnd()
        {
            TestKernel.Initialise();
        }

        [Given(@"I have the following players:")]
        public void GivenIHaveTheFollowingPlayers(Table table)
        {
            var playerRepository = TestKernel.Container.Resolve<IPlayerRepository>();

            foreach (var row in table.Rows)
            {
                var name = row.GetString("Name");

                var player = new PlayerBuilder()
                    .Named(name)
                    .Create();

                playerRepository.SaveOrUpdate(player);
            }
        }
    }
}
