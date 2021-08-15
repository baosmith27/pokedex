Feature: Retrieve Pokemon information from the api
	As an API user
	I want to access Pokemn character information
	So that I can consume them in my own application

Background: 
	Given I have a pokemon "ditto", "this is the ditto description", "cave", is_legendary "false"
	And I have a pokemon "ditto_legendary", "this is the ditto legendary description", "cave", is_legendary "true"
	And I have a pokemon "ditto_no_cave", "this is the ditto no cave description", "no cave", is_legendary "false"
	And I have a pokemon "ditto_legendary_no_cave", "this is the ditto legendary but no cave description", "no cave", is_legendary "true"

Scenario: Get basic description for non legendary	
	When I get pokemon information for 'ditto'
	Then the description should be 'this is the ditto description'
	And is_legendary is 'false'

Scenario: Get basic description for a legendary	character
	When I get pokemon information for 'ditto_legendary'
	Then the description should be 'this is the ditto legendary description'
	And is_legendary is 'true'
	
Scenario: Get basic description for non existent character
	When I get pokemon information for 'does not exist'
	Then the return code should be '404'

Scenario: Get translated description for non legendary and non cave dweller
	When I get pokemon translated information for 'ditto_no_cave'
	Then the description should be 'shakespeare' translated

Scenario: Get translated description for non legendary but cave dweller
	When I get pokemon translated information for 'ditto'
	Then the description should be 'yoda' translated

Scenario: Get translated description for legendary but not cave dweller
	When I get pokemon translated information for 'ditto_legendary_no_cave'
	Then the description should be 'yoda' translated

Scenario: Get translated description for legendary and cave dweller
	When I get pokemon translated information for 'ditto_legendary'
	Then the description should be 'yoda' translated