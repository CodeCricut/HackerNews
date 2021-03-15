# HackerNews

## N-Tier Architecture

This project utilizes N-Tier Architecture. It is split up into 6 main layers:

1. Domain
2. Infrastructure
3. Application
4. Web
5. Api
6. MVC

### Domain Layer

The domain layer contains all domain-entities that are common to all projects in the solution. It has both the entities that are relevant to the database, and models that rae relevant to the reading and writing of those entities.

It also contains repository interfaces that provide an abstract way of reading and writing to the database.

### Infrastructure Layer

The infrastructure layer is responsible for bridging the gap between the abstract database interfaces provided by the domain layer, and an actual database. In this application, EF Core is used to interact with a localdb SQL Server.

### Application Layer

The application uses the CQRS pattern to perform operations on the database. The application layer is responsible for housing this behavior. It also provides a number of interfaces that are application-specific (not general enough for domain layer.)

### Web Layer

The web layer houses common authentication and authorization logic that is shared between different web project types. It implements some application-specific interfaces declared in the application layer.

### API Layer

The API layer is a typical ASP.NET Core API project that references all of the aforementioned projects to provide a public-facing API for interacting with the database on a data-level.

### MVC Layer

The MVC layer is a typical ASP.NET Core MVC project that references all of the aforementioned projects (less the API project) to provide a public-facing website that has basic CRUD functionality.
