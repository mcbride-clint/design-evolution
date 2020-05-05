# design-evolution
Change is Web Application Design from no architechture to the "Clean Architecture".  Created in a .Net Core Application using Razor Pages to mimic WebForms style of pages.  No html was created, this is purely a focus on the backend.

 - Note: All the projects contain a FakeDB.cs.  This is purely meant to simulate an interaction with the database.  In most cases it could be substituted with an IDbConnection or other querying classes.
 - The Repository Pattern will also be used instead of Entity Framework

## 1 - All In Page

In Pages -> Users, there is UserMaint.cshtml and it's code-behind page that mimics our Grid and Save pages.  All the logic is directly in the page, no classes, and no architecture layers.  

There are no entry points for testing other than manually walking through possiblities and use cases through the UI.

## 2 - All In Page with Objects

Left all the code exactly the same except for using Dapper to map the Query result to the User.cs class.  It only has the UserId and Owner properties to keep this simple.  The objects are easy to organize and loop through in a list. Classes also make it easier to define and control scope of variables at any given time.

This is easier to work with over the DataTables but still does not provide any entry points for testing the application.  

Same as the last project, all the business is directly tied to chosen UI technology and any changes to either side will like cause changes on both sides.

## 3 - Move Logic out of the UI

Here we create a DesignApp.Application Project.  All the code from the code-behind page has now been moved into a UserService class.  This class contains all the actions for the application that relate to User, spanning any number of pages. With this logic move, we gain the ability to test a bit more of our logic without going directly though the UI.  It is still directly tied to the database though so all Tests would be integration tests of the business logic as well as the persistence logic.

I will get into Testing later in Step 7, but first I will extract out the SQL from the Application layer.

## 4 - Setting up the Repository Layer

Here is the biggest step in refactoring.  We now have two additional projects.  First, we created the DesignApp.Infrastructure project.  Inside here we created a Persistence folder and a UserRepository to manage storing User Objects to the Database.  Other application dependencies, such as file outputs or email providers, can be included in the project.

This had a project reference back to the Application project for the User.cs class.  So to prevent the circular reference between the two projects, I moved the Models in the newly created DesignApp.Domain project.  This project will contain basically a dictionary of everything that is core to your application.  Entities, Enums, or other Value Objects that can be used throughout your solution.

This is looking much better but any Tests we were to write would still depend on the database for any consistent results.

## 5 - Creating a Repository Interface

Creating Interfaces for the repositories, will reverse the dependencies of the Application and Infrastructure projects.  The interface is created in the Application project so that the Application dictates the requirements that is needs from a class to satisfy it's persistence.  This allows you to have multiple implementations of the interface that could be switched out in different environments or when testing.

Due to the Interface, what repository will satisfy the Interface is now a dependency of the Service so the responsibility of deciding the repository is moved up to the UI layer.

    var userRepo = new UserRepository();
    var userService = new UserService(userRepo);
    Users = userService.GetAllUsers();

This starts to add more logic to the UI layer and it remains tough to change out the implementation if needed (Everywhere that the service is used we would also need to change every page that is it used in.) In the next step, we can push that dependency up a bit further and simplify alot of code as well.

## 6 - Moving to Dependency Injection

The biggest area of change is in the UserMain.cshtml.cs code-behind page.  The declaration of the user service is now in the constructor and stored in a private field.  This way the service is just another dependency that the UI relies on to work.  In the Startup.cs file, .Net Core allows for setting up of a service collection to map up classes for injection. 

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<UserService>();

Implementations can be injected via constructor or parameter injection.  This project utilizes constructor injection.  Basically, the above two lines say that when an injection opportunity occurs when you see an `IUserRepository` provide an instance of `UserRepository` and a `UserService` when needed.  These constructors can be chained to get all the dependencies that are needed.  As you can see in our project we only have the UserMaint.cshtml.cs constructor that depends on a `UserService` but when that is constructed in needs an `IUserRepository` so it will satify that requiremnt for us as well.

There are multiple ways of customising the instance creation and lifetime scoping but I won't go into that here.

## 7 - Working with Unit Tests

This section has less to do with architecture and more to do with what you gain through all of these steps. 

Most of the code is identical to the previous step except for the new DesignApp.Tests projecct and some new User validation logic that I can test.  Inside of here, I created a new `IUserRepository` implementation called `UserRepositoryInMemory` so I now longer need to rely on testable data to be in the database to test my Application logic independently.

The tests that I have created test multiple test cases both working as expected and ensuring that it errors out when its supposed to.  It is normal to have much more test code than actual business logic but it prevents the need to constantly walk through use cases in using the UI.

## 8 - Moving to CQRS (TODO)

The Services style of organizing business logic is a good way of grouping related business logic but it can balloon up very quick in larger projects.  Combine that with the need to return different "ViewModels" of Data to the UI layer the organization of all the Code in one file and the "ViewModels" in multiple other files can be quite burdensome to maintain.  The Command Query Responsibility Segregation (CQRS) provides a better way to isolate Application actions to be accomplished.  The logic and viewmodels are closer and organized into smaller files, reducing possible Git conflicts and reducing the risk of affecting other business processes.

This is not accompished yet but will be soon.

## Additional Ideas

 - Additional Project folders can be created inside of each project or even extracted out to other projects for infrastucture that is shared between other projects.  More detailed folder structures can be seen at the links below.


More information on this architecture can be found at:
https://www.dandoescode.com/blog/clean-architecture-an-introduction/

A larger project can be seen here: (It uses EntityFramework for Persistence)
https://github.com/jasontaylordev/NorthwindTraders