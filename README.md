## About

This project is a full-stack application using clean architecture and CQRS pattern, that allows users to create, read, update, and delete blog posts.

## Architecture
Clean architecture is used to separate the application into layers. The layers are as follows: application, domain, and infrastructure. The application layer contains use-cases and all core implementations. The domain layer contains the business logic and
models. The infrastructure layer contains the database context and migrations.

The implementation between the Api and the rest of the application is built with CQRS (Command and Query Responsibility Segregation), a pattern that separates read and update operations for a data store.


## Technologies Used
- .NET 7
- ASP.NET MVC
- Entity Framework Core with InMemory Database
- C#
- HTML5
- CSS3
- Tailwindcss
- JWT Authentication

## Goals
- [ ] Implement WebUI with aspnet

- [x] Create a blog post
- [x] Read a blog post
- [x] Update a blog post
- [x] Delete a blog post
- [x] Set published/not published the blog post (by author)

- [x] Create a Tag
- [x] Read a Tag
- [x] Update a Tag
- [x] Delete a Tag

- [x] Create a Category
- [x] Read a Category
- [x] Update a Category
- [x] Delete a Category

- [x] Create a User
- [x] Read a User
- [x] Update a User
- [x] Delete a User

- [x] Authenticate a User
- [ ] Validate if an user is authenticated

- [ ] Create a comment
- [ ] Read a comment
- [ ] Update a comment
- [ ] Delete a comment

- [ ] Create a like
- [ ] Read a like
- [ ] Update a like
- [ ] Delete a like

- [ ] Should be able to search for a blog post by title
- [ ] Should be able to search for a blog post by category
- [ ] Should be able to search for a blog post by tag
- [ ] Should be able to search for a blog post by author
- [ ] Should be able to search all posts of an user or author

- [ ] Should be able comment a post
- [ ] Should be able reply an comment in an post
- [ ] Should be able delete a comment in an post (by author of comment or author of the post)
- [ ] Should be able update a comment in an post (by author of commment)


- [ ] Should be able to search for a user by email
