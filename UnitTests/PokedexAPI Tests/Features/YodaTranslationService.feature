Feature: Yoda Translation Service
	As a user
	I want to be abe to use the Yoda translation service
	to translate text into Yoda speak

Background: 
	Given I am using the Yoda translation service

Scenario: Translate basic text	
	When I translate 'this is the description for the pokemon ditto character'
	Then the text should be translated as 'The description for the pokemon ditto character,  this is'

Scenario: Translate basic text	when api down
	Given the translation api service is down
	When I translate 'this is the description for the pokemon ditto character'
	Then the text should be translated as 'this is the description for the pokemon ditto character'