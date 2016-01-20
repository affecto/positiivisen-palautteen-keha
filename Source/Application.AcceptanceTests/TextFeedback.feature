@ignore
Feature: TextFeedback

Background: 
	Given the following employees exist:
	| Name       |
	| Ted Tester |
	| Carl Coder |

Scenario: Giving feedback
	When 'Ted Tester' is given free feedback 'Great sense of humour.'
	And 'Ted Tester' is given free feedback 'Funny looking test data.'
	Then 'Ted Tester' has the following free feedback:
	| Feedback                 |
	| Great sense of humour.   |
	| Funny looking test data. |
	And 'Carl Coder' has no feedback

Scenario: Duplicate feedback
	Given 'Ted Tester' is given free feedback 'Great guy!'
	When 'Ted Tester' is given free feedback 'Great guy!'
	Then 'Ted Tester' has the following free feedback:
	| Feedback   |
	| Great guy! |
