Feature: Retrieve Pokemon information from the api
	As an API user
	I want to access Pokemn character information
	So that I can consume them in my own application

Background: 
	Given I have a pokemon "ditto", "ditto description", "cave", is_legendary "false"

Scenario: Get basic description for non legendary	
	When I get pokemon information for 'ditto'
	Then the description should be 'ditto description'
	And is_legendary is 'false'
	