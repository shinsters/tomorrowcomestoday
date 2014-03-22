Feature: GameRoundTests
	While the game is operational
	I want to ensure the rounds function as expected
	To avoid the game being crap

Background: I have a fully configured back end
	Given I have an initalised back end
	And I have the following players:
	| Name         |
	| Freda        |
	| Bob          |
	| James        |
	| Hank Skyman |


Scenario: When a game starts users are handed 
	Given I have a started game
