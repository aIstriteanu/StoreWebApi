Homework

Create a new ASP.NET Core Web API project using Visual Studio or another IDE of your choice.

Set up the necessary database schema using Entity Framework Core. You should have tables for customers, articles, payments, transactions, and a join table for the many-to-many relationship between transactions and articles.

Implement CRUD operations for customers, articles, and payments. Use HTTP GET for retrieving data, POST for creating new data, PUT for updating existing data, and DELETE for removing data.

Implement an endpoint for creating transactions that can include one or more articles and one or more payments. When a new transaction is created, the inventory of each article should be updated and each payment should be processed. Use a many-to-many relationship between transactions and articles to handle cases where a transaction includes multiple articles.

Implement input validation for all endpoints to ensure that data is correctly formatted and meets any necessary constraints.

Implement error handling for all endpoints to handle cases where an error occurs during processing.

Write unit tests for all endpoints using the NUnit testing framework. The tests should cover all use cases and should include edge cases and error handling scenarios.

Note:
Keep things simple but separate responsibilities properly in code/projects.

[Optional/do not spend a whole lot of time on this if it is needed]
Create a Dockerfile for the API that specifies the necessary dependencies and configuration.

Build a Docker image of the API using the Dockerfile.

Run the API in a Docker container.
