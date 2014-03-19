namespace TomorrowComesToday.Tests.SpecflowTests.StepDefinitions
{
    using System.Collections.Generic;

    using TechTalk.SpecFlow;

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
        [Given(@"I have a deck of cards")]
        public void GivenIHaveADeckOfCards()
        {
            var cardRepository = InitaliseTests.Container.Resolve<ICardRepository>();

            var derp = cardRepository.GetAll();

            const int DeckSize = CommonConcepts.DeckSize;

            var cards = new List<Card>();

            for (var i = 0; i < DeckSize; i++)
            {
                var card = new CardBuilder()
                    .Text("Hello World")
                    .Type(CardType.Black)
                    .Create();

                cards.Add(card);
            }
        }

        [Given(@"I have a started game")]
        public void GivenIHaveAStartedGame()
        {
            ScenarioContext.Current.Pending();
        }
    }
}