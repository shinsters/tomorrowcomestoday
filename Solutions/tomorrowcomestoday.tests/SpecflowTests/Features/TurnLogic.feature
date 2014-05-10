Feature: Turn Logic

Background: I have a fully configured back end
	Given I have an initalised back end
	And I have the following players:
	| Name     |
	| James    |
	| Jean Luc |
	| Benjamin |
	| Kathryn  | 


Scenario: When all players have played their white card, the card tsar is able to see them
	Given I have a game with following players:
	| Name     |
	| James    |
	| Jean Luc |
	| Benjamin |
	| Kathryn  |
	And the game is started 
	And the card tsar is currently 'Jean Luc'
	And the following players have played an answer card:
	| Name     |
	| James    |
	| Benjamin |
	| Kathryn  |
	Then I see the card tsar is able to see the answer cards
	
Scenario: When not all players have played their white card, the card tsar is unable to see them
	Given I have a game with following players:
	| Name     |
	| James    |
	| Jean Luc |
	| Benjamin |
	| Kathryn  |
	And the game is started 
	And the card tsar is currently 'Jean Luc'
	And the following players have played an answer card:
	| Name     |
	| James    |
	| Benjamin |
	Then I see the card tsar is able to not see the answer cards

Scenario: When all users have played a hand, the card tsar can select a winner and points are allocated
	Given I have a game with following players:
	| Name     |
	| James    |
	| Jean Luc |
	| Benjamin |
	| Kathryn  |
	And the game is started 
	And the card tsar is currently 'Jean Luc'
	And the following players have played an answer card:
	| Name     |
	| James    |
	| Benjamin |
	| Kathryn  |
	And the card tsar selects an answer card
	Then I see the player who played the winning card has a point