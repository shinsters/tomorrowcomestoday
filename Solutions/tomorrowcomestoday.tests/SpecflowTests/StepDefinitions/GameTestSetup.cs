namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using TechTalk.SpecFlow;

    /// <summary>
    /// Sets up basic components for tests
    /// </summary>
    [Binding]
    public class GameTestSetup
    {
        [Given(@"I have an initalised back end")]
        public void GivenIHaveAnInitalisedBackEnd()
        {
            InitaliseTests.Initalise();
        }
    }
}
