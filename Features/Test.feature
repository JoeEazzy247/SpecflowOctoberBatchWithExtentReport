Feature: Test

Background: 
	Given I navigate to demoQa page

@ignore
Scenario: Elements test
	When I click Elements
		And I click Text Box
		And I enter the following data
		| FullName | Email        | CurrentAddress | ParmanentAddress |
		| Testname | test@abc.com | Testaddress    | TestPAddress     |

		And I click submit button
	Then following data has been added
	| FullName | Email        | CurrentAddress | ParmanentAddress |
	| Testname | test@abc.com | Testaddress    | TestPAddress     |