Feature: GameRoundTests
	While the game is operational
	I want to ensure the rounds function as expected
	To avoid the game being crap

Background: I have a fully configured back end
	Given I have a deck of cards


Scenario: When a game starts users are handed 
	Given I have a started game
	Then I expect every user will have a full set of cards in their hand