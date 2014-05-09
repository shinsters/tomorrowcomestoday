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

# Card dealing

Scenario: When a game starts users are handed their cards
	Given I have a game with following players:
	| Name     |
	| Jean Luc |
	| Benjamin |
	And the game is started 
	Then I see the game is in state 'Active'
	And I see the game players are in the following state:
	| Name     | Points | Cards in hand |
	| Jean Luc | 0      | 10            |
	| Benjamin | 0      | 10            |


Scenario: Cards in deck when not enough left are properly shared among players
	Given I have a game with following players:
	| Name     |
	| Jean Luc |
	| Benjamin |
	And I have a limited white deck size of '11' cards
	And the game is started
	Then I see the game is in state 'Active'
	And I see the game players are in the following state:
	| Name     | Points | Cards in hand |
	| Jean Luc | 0      | 6             |
	| Benjamin | 0      | 5             |

# Player assignment 

Scenario: A card tsar is assigned and a black card is played as the game starts
	Given I have a game with following players:
	| Name     |
	| Jean Luc |
	| Benjamin |
	And the game is started 
	Then I see the game is in state 'Active'
	And I see the game has an active black card
	And I see the game has an active player

# Turn 



