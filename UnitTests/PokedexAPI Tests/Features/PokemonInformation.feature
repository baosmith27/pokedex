Feature: Retrieve Pokemon information from the api
	As an API user
	I want to access Pokemn character information
	So that I can consume them in my own application

Background: 
	Given I have a pokemon "ditto", "ditto description", "cave", is_legendary "false"
	And I have a pokemon "ditto_legendary", "ditto legendary description", "cave", is_legendary "true"

Scenario: Get basic description for non legendary	
	When I get pokemon information for 'ditto'
	Then the description should be 'ditto description'
	And is_legendary is 'false'

Scenario: Get basic description for a legendary	character
	When I get pokemon information for 'ditto_legendary'
	Then the description should be 'ditto legendary description'
	And is_legendary is 'true'
	
Scenario: Get basic description for non existent character
	When I get pokemon information for 'does not exist'
	Then the return code should be '404'



	