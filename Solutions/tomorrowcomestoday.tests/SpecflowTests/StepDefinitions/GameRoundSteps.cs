namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using TechTalk.SpecFlow;

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
    }
}