# TaskManagerAPI
Task management system using .NET technologies. This system will allow users to manage their tasks efficiently, providing features to create, update, delete, and list tasks

This ASP.NET Web API project serves as a task manager application, allowing users to perform various actions related to task management. It consists of two main controllers:

## 1. Authentication Controller
**Route: /api/auth**

-This controller handles user authentication functionalities such as registration and login.

Endpoints:

	POST /register: Register a new user.
	POST /login: Log in an existing user
 
- Login: Authenticate users based on provided credentials (email and password).

 
	1- If the provided email is not found, returns an unauthorized response with the message "Invalid email."
	  
	2- Verifies the password by computing the hash and comparing it with the stored hash in the database.
	  
	3- Generates a JWT token upon successful authentication and returns a response containing the user's username and token.
  

- Register: Register new users within the system.


	1- Hashes the password using HMACSHA512 algorithm for secure storage.
	
	2- Saves the user details to the database after successful registration.
	
	3- Generates a JWT token for the newly registered user.
	
	4- Returns a response containing the user's username and token upon successful registration.

## 2. Task Controller

**Route: /api/tasks**

This controller manages tasks and their associated functionalities.
Endpoints:

	1- GET /: Retrieve all tasks. 
 	(Verification: No authentication or authorization required.)
	
	2- GET /{id}: Retrieve a single task by its ID. 
 	(Verification: No authentication or authorization required.)
	
	3- GET /assigned: Retrieve tasks assigned to the authenticated user. 
 	(Verification: Requires user authentication.)
	
	4- GET /users/{userId}: Retrieve tasks assigned to a specific user by their ID. 
 	(Verification: Requires user authentication and authorization.)
	
	5- POST /: Add a new task.
 	(Verification: Requires user authentication and authorization.)
  
	6- PUT /: Update an existing task.
	(Verification: Requires user authentication and authorization. Only the creator of the task can update it.)
 
	7- DELETE /{id}: Delete a task.
	(Verification: Requires user authentication and authorization. Only the creator of the task or the assigned user can delete it.)
 
	8- PATCH /{taskId}/users/{userId}: Assign a task to a user.
 	(Verification: Requires user authentication and authorization. Only the creator of the task can assign it to another user.)
	
	9- PATCH /{id}/status: Update the status of a task.
 	(Verification: Requires user authentication and authorization. Only the creator of the task or the assigned user can update its status.)


## Project structure

      
      │   │   ├── Controllers //This folder contains controller classes that handle HTTP requests and associated actions.
      
      │   │   ├── DTOs (Data Transfer Objects) //Used to store classes that serve to transfer data betweent the layers of application.
      
      │   │   ├── Entities: //This folder contains classes representing the main entities of the application.
      
      │   │   ├── Services: //This folder contains classes that implement the business logic of the application.
      
      │   │   ├──├── IServices //This folder contains interfaces describing the contracts of application classes.

      │   │   ├── Migration: //This folder used to store the code files that represent database schema changes.

![image](https://github.com/AbdennourHsn/TaskManager/assets/119530347/68a8de11-4d09-40bd-9a8c-b525e7df36f6)


## Substantial Classes
**..DTOs/ResponseDto.cs** : Standardizes API responses with result, success status, message, and status code, unifying responses across requests for clarity and consistency, with static factory responce for each responce case.

**..HttpStatusCode.cs** : Provides integer constants for common HTTP status codes, simplifying status code usage in web applications.

	public static class HttpStatusCode
	{
        public const int OK = 200;
        public const int Created = 201;
        public const int NoContent = 204;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int InternalServerError = 500;
    }

**..Swagger/ConfigureSwaggerOptions.cs**: Implements SwaggerGenOptions configuration to add JWT authentication security definition and requirement for Swagger UI, facilitating secure API documentation with JWT support.




# TaskManager Unit Test

The project includes unit tests for the TaskController using xUnit.net and FluentAssertions.


## Getting Started

	1- Clone the repository.
	
	2- Configure the database connection in appsettings.json.
	
	3- Run the application.
	
	4- Explore the API endpoints using tools like Postman or Swagger.
 

