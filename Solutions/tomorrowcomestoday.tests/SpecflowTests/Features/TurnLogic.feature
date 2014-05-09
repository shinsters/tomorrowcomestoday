Feature: TurnLogic

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
	Then I see the card tsar is unable to see the answer cards
	