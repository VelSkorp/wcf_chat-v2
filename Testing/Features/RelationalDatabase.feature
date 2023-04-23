@Api
Feature: Relational database

Scenario Outline: The new user is added
	When the user is added with the username: <Username>, first name: <FirstName>, last name: <LastName>, password: <Password>
	Then the user exists with the id: 1, username: <Username>, first name: <FirstName>, last name: <LastName>, password: <Password>

	Examples:
		| Username | FirstName | LastName   | Password     |
		| Test     | Test      | Test       | Test         |
		| VelScorp | Vlad      | Kontsevich | TestPassword |

Scenario Outline: The user can change profile details
	Given the user is added with the id: 1, username: <Username>, first name: <FirstName>, last name: <LastName>, password: <Password>
	When the user with an id: 1 changed username to <NewUsername>, first name to <NewFirstName>, last name to <NewLastName>
	Then the user exists with the id: 1, username: <NewUsername>, first name: <NewFirstName>, last name: <NewLastName>, password: <Password>

	Examples:
		| Username | FirstName | LastName   | Password     | NewUsername | NewFirstName | NewLastName |
		| Test     | Test      | Test       | Test         | VelScorp    | Vlad         | Kontsevich  |
		| VelScorp | Vlad      | Kontsevich | TestPassword | Test        | Test         | Test        |

Scenario Outline: The new chat is added
	Given the user is added with the id: 1, username: TestUser, first name: Test, last name: Test, password: Test
		And the user is added with the id: 2, username: VelScorp, first name: Vlad, last name: Kontsevich, password: TestPassword
	When the chat is added with the name: <ChatName>, owner id: <OwnerId> and users:
		| Id | FirstName | LastName | Username   |
		| 1  | Test      | Test     | Test       |
		| 2  | VelScorp  | Vlad     | Kontsevich |
	Then the user with the id: <UserId>, username: <Username> is a member of chat rooms: <ChatName>

	Examples:
		| ChatName | OwnerId | Username | UserId |
		| TestChat | 2       | TestUser | 1      |
		| VelScorp | 2       | VelScorp | 2      |