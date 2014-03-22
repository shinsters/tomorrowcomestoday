Feature: GameRoundTests
	While the game is operational
	I want to ensure the rounds function as expected
	To avoid the game being crap

Background: I have a fully configured back end
	Given I have an initalised back end
	And I have the following players:
	| Name     |
	| James    |
	| Jean Luc |
	| Benjamin |
	| Kathryn  | 

Scenario: When a game starts users are handed 
	Given I have a started game with the id 'F4909379-AF76-418E-873D-E575A8BA3233' containing following players:
	| Name     |
	| Jean Luc |
	| Benjamin |
	Then I see the game 'F4909379-AF76-418E-873D-E575A8BA3233' is in state 'Active'
	And I see the game 'F4909379-AF76-418E-873D-E575A8BA3233' players are in the following state:
	| Name     | Points | Cards in hand |
	| Jean Luc | 0      | 10            |
	| Benjamin | 0      | 10            |

