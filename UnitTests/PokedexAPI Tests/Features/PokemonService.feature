Feature: Pokemon Service
	As a user
	I want to be abe to use the Pokemon service
	to retrieve pokemon characters

Scenario: Get a valid pokemon	
	When I retrieve pokemon 'ditto'
	Then the pokemon should be found
	And the name should be 'ditto'

Scenario: Get an invalid pokemon	
	When I retrieve pokemon 'dittoooo'
	Then the pokemon should not be found

Scenario: Get a valid pokemon when api service is down
	Given the pokemon api service is down
	When I retrieve pokemon 'ditto'
	Then the pokemon should not be found