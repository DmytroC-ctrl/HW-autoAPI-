Feature: API
	In order to have my account with personal information
	As a user
	I want to have opportunity register in the web-site

	Background: 
	Given creat new rest client

@auth
Scenario: RegistrationOk
Given data for registration is ready
When I send post registration request
Then status code request ok
Then name from responce equal name from request
Then email from responce equal email from request


Scenario: Registration of an account with an invalid email data
Given data for registration with invalid email is ready
When I send post registration request
Then status code request ok
Then the request response contains an error message

Scenario: Creat new company
Given data about the new company is ready
When I send post company registration request
Then status code request ok
Then the request response contains an type -success

Scenario: Creat new user
Given data about new user is ready
When I send new user post request
Then status code request ok
Then email from responce equal email from request

Scenario: Creat user with task
Given data about new user with task is ready
When I send post new user with task is ready
Then status code request ok
Then email from responce equal email from request

Scenario: Creat new task
Given data about new task is ready
When I send new post new task request 
Then status code request ok
Then the request response contains an type -success and id

Scenario: Search my company
Given data for company search is prepared
When I am sending a request to search a company
Then status code request okey 
Then the response to the request contains the type -success and has the required id

Scenario: Search user
Given data for user search is prepared
When I am sending a request to search a user
Then status code request okay
Then the response to the request contains the type -success














	