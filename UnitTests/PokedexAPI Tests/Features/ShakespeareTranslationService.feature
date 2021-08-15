Feature: Shakespeare Translation Service
	As a user
	I want to be abe to use the Shakespeare translation service
	to translate text into Yoda speak

Background: 
	Given I am using the Shakespeare translation service

Scenario: Translate basic text		
	When I translate 'no this is the description for the pokemon ditto character'
	Then the text should be translated as 'Nay this is the description for the pokemon ditto character'

Scenario: Translate basic text when api down
	Given the translation api service is down
	When I translate 'no this is the description for the pokemon ditto character'
	Then the text should be translated as 'no this is the description for the pokemon ditto character'