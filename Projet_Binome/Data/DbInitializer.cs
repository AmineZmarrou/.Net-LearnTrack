using Microsoft.AspNetCore.Identity;
using Projet_Binome.Models;
using System;
using System.Linq;

namespace Projet_Binome.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure an admin account exists (one-time seed)
            const string adminEmail = "lolaminos143@gmail.com";
            const string adminPassword = "lolaminos143@gmail.com"; // change after first login
            var adminEmailNormalized = adminEmail.ToUpperInvariant();
            var adminExists = context.Users.Any(u => u.NormalizedEmail == adminEmailNormalized);
            if (!adminExists)
            {
                var adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    NormalizedEmail = adminEmailNormalized,
                    FullName = "Admin",
                    IsAdmin = true
                };
                var hasher = new PasswordHasher<ApplicationUser>();
                adminUser.PasswordHash = hasher.HashPassword(adminUser, adminPassword);
                context.Users.Add(adminUser);
                context.SaveChanges();
            }

            var hasCourses = context.Courses.Any();
            if (!hasCourses)
            {

            var courses = new Course[]
            {
                new Course{Title="Introduction to C#",Description="Learn the basics of C# programming .",ImageUrl="/images/Csharp.jpg"},
                new Course{Title="ASP.NET Core MVC Fundamentals",Description="Build web applications with ASP.NET Core MVC.",ImageUrl="/images/ASP.jpg"}, 
                new Course{Title="Entity Framework Core Deep Dive",Description="Master data access with EF Core.",ImageUrl="/images/Entity Framework Core Deep Dive.jpg"}, 
                new Course{Title="Introduction to Python",Description="Learn Python from scratch.",ImageUrl="/images/Python.png"},
                new Course{Title="JavaScript for Beginners",Description="A comprehensive guide to JavaScript.",ImageUrl="/images/javascript.jpg"},
                new Course{Title="DevOps and CI/CD Foundations",Description="Learn how to ship code with Docker, pipelines, and basic observability.",ImageUrl="/images/ci-cd.png"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var lessons = new Lesson[]
            {
                new Lesson{CourseId=courses[0].Id,Title="C# Basics",Content="Explore the fundamental concepts of C#, including variables, data types, and operators.", YouTubeLink="https://www.youtube.com/watch?v=gfkTfcpWqAY"},
                new Lesson{CourseId=courses[0].Id,Title="Control Flow in C#",Content="Learn how to control the flow of your C# programs using if statements, loops, and switch cases.", YouTubeLink="https://www.youtube.com/watch?v=gfkTfcpWqAY"},
                new Lesson{CourseId=courses[0].Id,Title="Object-Oriented Programming in C#",Content="Dive into the principles of object-oriented programming (OOP) with C#, including classes, objects, inheritance, and polymorphism.", YouTubeLink="https://www.youtube.com/watch?v=2sXBXh8DxYo&list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO"},
                new Lesson{CourseId=courses[1].Id,Title="Understanding MVC Architecture",Content="Get a solid understanding of the Model-View-Controller (MVC) architectural pattern and how it's used in ASP.NET Core.", YouTubeLink="https://www.youtube.com/watch?v=2sXBXh8DxYo&list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO"},
                new Lesson{CourseId=courses[1].Id,Title="Routing in ASP.NET Core",Content="Learn how ASP.NET Core maps incoming requests to controller actions and how to configure custom routes.", YouTubeLink="https://www.youtube.com/watch?v=2sXBXh8DxYo&list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO"},
                new Lesson{CourseId=courses[1].Id,Title="Working with Data in ASP.NET Core",Content="Discover how to use Entity Framework Core to interact with a database in your ASP.NET Core applications.", YouTubeLink="https://www.youtube.com/watch?v=SryQxUeChMc"},
                new Lesson{CourseId=courses[2].Id,Title="Introduction to Entity Framework Core",Content="Learn how to set up and configure Entity Framework Core in your .NET projects to start working with databases.", YouTubeLink="https://www.youtube.com/watch?v=0LmzcRVrvKY&list=PLN5rV4x2x5Xfob-AfkhkixASFveutHuWm"},
                new Lesson{CourseId=courses[2].Id,Title="Database Migrations with EF Core",Content="Master the art of managing your database schema changes over time using EF Core's powerful migration tools.", YouTubeLink="https://www.youtube.com/watch?v=0LmzcRVrvKY&list=PLN5rV4x2x5Xfob-AfkhkixASFveutHuWm"},
                new Lesson{CourseId=courses[2].Id,Title="Querying Data with EF Core",Content="Learn how to efficiently query data from your database using LINQ and other EF Core features.", YouTubeLink="https://www.youtube.com/watch?v=0LmzcRVrvKY&list=PLN5rV4x2x5Xfob-AfkhkixASFveutHuWm"},
                new Lesson{CourseId=courses[3].Id,Title="Python Basics for Beginners",Content="Start your Python journey by learning the basic syntax, data structures, and concepts.", YouTubeLink="https://www.youtube.com/watch?v=5O_m0IGwQLw&list=PLuXY3ddo_8nzrO74UeZQVZOb5-wIS6krJ"},
                new Lesson{CourseId=courses[3].Id,Title="Functions and Modules in Python",Content="Learn how to create and use functions and modules to organize and reuse your Python code.", YouTubeLink="https://www.youtube.com/watch?v=5O_m0IGwQLw&list=PLuXY3ddo_8nzrO74UeZQVZOb5-wIS6krJ"},
                new Lesson{CourseId=courses[4].Id,Title="JavaScript Fundamentals",Content="Get started with JavaScript by learning the core concepts, including variables, functions, and DOM manipulation.", YouTubeLink="https://www.youtube.com/watch?v=PWuTLTFMtYw&list=PLknwEmKsW8OuTqUDaFRBiAViDZ5uI3VcE"},
                new Lesson{CourseId=courses[4].Id,Title="Working with APIs in JavaScript",Content="Learn how to fetch data from external APIs and use it in your JavaScript applications.", YouTubeLink="https://www.youtube.com/watch?v=PWuTLTFMtYw&list=PLknwEmKsW8OuTqUDaFRBiAViDZ5uI3VcE"},
                new Lesson{CourseId=courses[5].Id,Title="Why DevOps Matters",Content="Understand how DevOps practices shorten feedback loops and improve release quality.", YouTubeLink="https://www.youtube.com/watch?v=Xrgk023l4lI"},
                new Lesson{CourseId=courses[5].Id,Title="Containerizing with Docker",Content="Write a Dockerfile, build multi-stage images, and run containers locally.", YouTubeLink="https://www.youtube.com/watch?v=0qotVMX-J5s"},
                new Lesson{CourseId=courses[5].Id,Title="CI/CD with GitHub Actions",Content="Create a build-and-test pipeline, add caching, and gate deployments.", YouTubeLink="https://www.youtube.com/watch?v=R8_veQiYBjI"}
            };
            foreach (Lesson l in lessons)
            {
                context.Lessons.Add(l);
            }
            context.SaveChanges();

            var quizzes = new Quiz[]
            {
                new Quiz{CourseId=courses[0].Id,Title="C# Basics Quiz"},
                new Quiz{CourseId=courses[1].Id,Title="ASP.NET Core MVC Quiz"},
                new Quiz{CourseId=courses[2].Id,Title="Entity Framework Core Quiz"},
                new Quiz{CourseId=courses[3].Id,Title="Python Basics Quiz"},
                new Quiz{CourseId=courses[4].Id,Title="JavaScript Basics Quiz"},
                new Quiz{CourseId=courses[5].Id,Title="DevOps Foundations Quiz"}
            };
            foreach (Quiz q in quizzes)
            {
                context.Quizzes.Add(q);
            }
            context.SaveChanges();


            var questions = new Question[]
            {
                // C# Quiz (10 questions)
                new Question{QuizId=quizzes[0].Id,Text="What is a variable in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="Which of the following is a loop in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the correct way to declare an integer variable in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the purpose of the 'using' statement in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the difference between '==' and '.Equals()' in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is a namespace in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the base class for all exceptions in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the purpose of the 'static' keyword in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the difference between a class and a struct in C#?"},
                new Question{QuizId=quizzes[0].Id,Text="What is the entry point of a C# console application?"},

                // ASP.NET Core MVC Quiz (10 questions)
                new Question{QuizId=quizzes[1].Id,Text="What does MVC stand for?"},
                new Question{QuizId=quizzes[1].Id,Text="What is the role of the Model in MVC?"},
                new Question{QuizId=quizzes[1].Id,Text="What is the role of the View in MVC?"},
                new Question{QuizId=quizzes[1].Id,Text="What is the role of the Controller in MVC?"},
                new Question{QuizId=quizzes[1].Id,Text="What is Razor in ASP.NET Core MVC?"},
                new Question{QuizId=quizzes[1].Id,Text="What is the purpose of routing in ASP.NET Core MVC?"},
                new Question{QuizId=quizzes[1].Id,Text="What is dependency injection in ASP.NET Core?"},
                new Question{QuizId=quizzes[1].Id,Text="What is middleware in ASP.NET Core?"},
                new Question{QuizId=quizzes[1].Id,Text="What is the purpose of the 'wwwroot' folder in an ASP.NET Core project?"},
                new Question{QuizId=quizzes[1].Id,Text="How do you pass data from a controller to a view in ASP.NET Core MVC?"},

                // Entity Framework Core Quiz (10 questions)
                new Question{QuizId=quizzes[2].Id,Text="What is Entity Framework Core?"},
                new Question{QuizId=quizzes[2].Id,Text="What is the purpose of a DbContext in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="What is a migration in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="How do you add a new migration in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="How do you apply migrations to the database?"},
                new Question{QuizId=quizzes[2].Id,Text="What is the purpose of DbSet<TEntity> in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="What is the difference between eager, explicit, and lazy loading in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="What is the purpose of the OnModelCreating method in DbContext?"},
                new Question{QuizId=quizzes[2].Id,Text="How do you perform a raw SQL query in EF Core?"},
                new Question{QuizId=quizzes[2].Id,Text="What is change tracking in EF Core?"},

                // Python Quiz (10 questions)
                new Question{QuizId=quizzes[3].Id,Text="What is the correct file extension for Python files?"},
                new Question{QuizId=quizzes[3].Id,Text="How do you create a variable with the numeric value 5?"},
                new Question{QuizId=quizzes[3].Id,Text="What is the correct way to write a comment in Python?"},
                new Question{QuizId=quizzes[3].Id,Text="Which data type is used to store a sequence of characters?"},
                new Question{QuizId=quizzes[3].Id,Text="How do you create a list in Python?"},
                new Question{QuizId=quizzes[3].Id,Text="Which method can be used to return the length of a string?"},
                new Question{QuizId=quizzes[3].Id,Text="Which of the following is a mutable data type in Python?"},
                new Question{QuizId=quizzes[3].Id,Text="How do you start a 'for' loop in Python?"},
                new Question{QuizId=quizzes[3].Id,Text="What is the purpose of the 'def' keyword in Python?"},
                new Question{QuizId=quizzes[3].Id,Text="How do you import a module in Python?"},

                // JavaScript Quiz (10 questions)
                new Question{QuizId=quizzes[4].Id,Text="What is the correct way to declare a variable in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="Which of the following is a JavaScript data type?"},
                new Question{QuizId=quizzes[4].Id,Text="What is the purpose of the 'addEventListener' method?"},
                new Question{QuizId=quizzes[4].Id,Text="How do you write a comment in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="What is the difference between '==' and '===' in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="What is a closure in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="What is the purpose of the 'this' keyword in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="What is the DOM in the context of web development?"},
                new Question{QuizId=quizzes[4].Id,Text="How do you create an array in JavaScript?"},
                new Question{QuizId=quizzes[4].Id,Text="What is JSON?"},

                // DevOps Quiz (8 questions)
                new Question{QuizId=quizzes[5].Id,Text="What problem does Continuous Integration primarily address?"},
                new Question{QuizId=quizzes[5].Id,Text="Why are container images valuable in DevOps workflows?"},
                new Question{QuizId=quizzes[5].Id,Text="What does a multi-stage Dockerfile help you achieve?"},
                new Question{QuizId=quizzes[5].Id,Text="When should automated tests run in a CI pipeline?"},
                new Question{QuizId=quizzes[5].Id,Text="How is YAML typically used in CI/CD pipelines?"},
                new Question{QuizId=quizzes[5].Id,Text="Why store credentials as encrypted secrets in pipelines?"},
                new Question{QuizId=quizzes[5].Id,Text="Which command builds a Docker image from a Dockerfile?"},
                new Question{QuizId=quizzes[5].Id,Text="What indicates a healthy pipeline run before deployment?"}
            };
            foreach (Question q in questions)
            {
                context.Questions.Add(q);
            }
            context.SaveChanges();

            var choices = new Choice[]
            {
                // C# Choices
                new Choice{QuestionId=questions[0].Id,Text="A storage location in memory.",IsCorrect=true},
                new Choice{QuestionId=questions[0].Id,Text="A type of function.",IsCorrect=false},
                new Choice{QuestionId=questions[0].Id,Text="A conditional statement.",IsCorrect=false},

                new Choice{QuestionId=questions[1].Id,Text="If-else",IsCorrect=false},
                new Choice{QuestionId=questions[1].Id,Text="For",IsCorrect=true},
                new Choice{QuestionId=questions[1].Id,Text="Switch",IsCorrect=false},

                new Choice{QuestionId=questions[2].Id,Text="int x = 5;",IsCorrect=true},
                new Choice{QuestionId=questions[2].Id,Text="integer x = 5;",IsCorrect=false},
                new Choice{QuestionId=questions[2].Id,Text="num x = 5;",IsCorrect=false},

                new Choice{QuestionId=questions[3].Id,Text="To import namespaces",IsCorrect=true},
                new Choice{QuestionId=questions[3].Id,Text="To create an alias for a class",IsCorrect=false},
                new Choice{QuestionId=questions[3].Id,Text="To define a new variable",IsCorrect=false},

                new Choice{QuestionId=questions[4].Id,Text="'==' compares reference equality, '.Equals()' compares content equality.",IsCorrect=true},
                new Choice{QuestionId=questions[4].Id,Text="'.Equals()' compares reference equality, '==' compares content equality.",IsCorrect=false},
                new Choice{QuestionId=questions[4].Id,Text="There is no difference.",IsCorrect=false},

                new Choice{QuestionId=questions[5].Id,Text="A way to organize code and prevent naming conflicts.",IsCorrect=true},
                new Choice{QuestionId=questions[5].Id,Text="A special type of class.",IsCorrect=false},
                new Choice{QuestionId=questions[5].Id,Text="A file that contains C# code.",IsCorrect=false},

                new Choice{QuestionId=questions[6].Id,Text="System.Exception",IsCorrect=true},
                new Choice{QuestionId=questions[6].Id,Text="System.ApplicationException",IsCorrect=false},
                new Choice{QuestionId=questions[6].Id,Text="System.SystemException",IsCorrect=false},

                new Choice{QuestionId=questions[7].Id,Text="To create a member that belongs to the type itself rather than to a specific object.",IsCorrect=true},
                new Choice{QuestionId=questions[7].Id,Text="To make a class non-instantiable.",IsCorrect=false},
                new Choice{QuestionId=questions[7].Id,Text="To indicate that a method is thread-safe.",IsCorrect=false},

                new Choice{QuestionId=questions[8].Id,Text="Classes are reference types, structs are value types.",IsCorrect=true},
                new Choice{QuestionId=questions[8].Id,Text="Structs can have methods, classes cannot.",IsCorrect=false},
                new Choice{QuestionId=questions[8].Id,Text="There is no difference.",IsCorrect=false},

                new Choice{QuestionId=questions[9].Id,Text="Main()",IsCorrect=true},
                new Choice{QuestionId=questions[9].Id,Text="Program()",IsCorrect=false},
                new Choice{QuestionId=questions[9].Id,Text="Start()",IsCorrect=false},

                // ASP.NET Core MVC Choices
                new Choice{QuestionId=questions[10].Id,Text="Model View Controller",IsCorrect=true},
                new Choice{QuestionId=questions[10].Id,Text="Most Valuable Coder",IsCorrect=false},
                new Choice{QuestionId=questions[10].Id,Text="My Very Cool App",IsCorrect=false},

                new Choice{QuestionId=questions[11].Id,Text="To represent the application's data and business logic.",IsCorrect=true},
                new Choice{QuestionId=questions[11].Id,Text="To handle user input.",IsCorrect=false},
                new Choice{QuestionId=questions[11].Id,Text="To render the user interface.",IsCorrect=false},

                new Choice{QuestionId=questions[12].Id,Text="To display the data from the model to the user.",IsCorrect=true},
                new Choice{QuestionId=questions[12].Id,Text="To process user requests.",IsCorrect=false},
                new Choice{QuestionId=questions[12].Id,Text="To connect to the database.",IsCorrect=false},

                new Choice{QuestionId=questions[13].Id,Text="To handle user requests and interact with the model and view.",IsCorrect=true},
                new Choice{QuestionId=questions[13].Id,Text="To define the application's data structure.",IsCorrect=false},
                new Choice{QuestionId=questions[13].Id,Text="To create the user interface.",IsCorrect=false},

                new Choice{QuestionId=questions[14].Id,Text="A markup syntax for embedding server-side code into webpages.",IsCorrect=true},
                new Choice{QuestionId=questions[14].Id,Text="A JavaScript library for building user interfaces.",IsCorrect=false},
                new Choice{QuestionId=questions[14].Id,Text="A database management system.",IsCorrect=false},

                new Choice{QuestionId=questions[15].Id,Text="To map incoming HTTP requests to controller action methods.",IsCorrect=true},
                new Choice{QuestionId=questions[15].Id,Text="To validate user input.",IsCorrect=false},
                new Choice{QuestionId=questions[15].Id,Text="To manage application state.",IsCorrect=false},

                new Choice{QuestionId=questions[16].Id,Text="A design pattern that allows for loose coupling between components.",IsCorrect=true},
                new Choice{QuestionId=questions[16].Id,Text="A way to encrypt data.",IsCorrect=false},
                new Choice{QuestionId=questions[16].Id,Text="A technique for optimizing database queries.",IsCorrect=false},

                new Choice{QuestionId=questions[17].Id,Text="A component that processes HTTP requests and responses.",IsCorrect=true},
                new Choice{QuestionId=questions[17].Id,Text="A tool for debugging code.",IsCorrect=false},
                new Choice{QuestionId=questions[17].Id,Text="A type of controller.",IsCorrect=false},

                new Choice{QuestionId=questions[18].Id,Text="To serve static files like CSS, JavaScript, and images.",IsCorrect=true},
                new Choice{QuestionId=questions[18].Id,Text="To store application configuration files.",IsCorrect=false},
                new Choice{QuestionId=questions[18].Id,Text="To keep track of user sessions.",IsCorrect=false},

                new Choice{QuestionId=questions[19].Id,Text="Using ViewData, ViewBag, or a strongly-typed model.",IsCorrect=true},
                new Choice{QuestionId=questions[19].Id,Text="By writing to a text file.",IsCorrect=false},
                new Choice{QuestionId=questions[19].Id,Text="It's not possible to pass data from a controller to a view.",IsCorrect=false},

                // Entity Framework Core Choices
                new Choice{QuestionId=questions[20].Id,Text="An ORM (Object-Relational Mapper) for .NET.",IsCorrect=true},
                new Choice{QuestionId=questions[20].Id,Text="A UI framework for building web applications.",IsCorrect=false},
                new Choice{QuestionId=questions[20].Id,Text="A testing framework for .NET.",IsCorrect=false},

                new Choice{QuestionId=questions[21].Id,Text="To represent a session with the database.",IsCorrect=true},
                new Choice{QuestionId=questions[21].Id,Text="To define the application's business logic.",IsCorrect=false},
                new Choice{QuestionId=questions[21].Id,Text="To handle HTTP requests.",IsCorrect=false},

                new Choice{QuestionId=questions[22].Id,Text="A way to evolve the database schema over time.",IsCorrect=true},
                new Choice{QuestionId=questions[22].Id,Text="A tool for data backup and recovery.",IsCorrect=false},
                new Choice{QuestionId=questions[22].Id,Text="A method for optimizing database queries.",IsCorrect=false},

                new Choice{QuestionId=questions[23].Id,Text="dotnet ef migrations add [MigrationName]",IsCorrect=true},
                new Choice{QuestionId=questions[23].Id,Text="dotnet add migration [MigrationName]",IsCorrect=false},
                new Choice{QuestionId=questions[23].Id,Text="ef migrations create [MigrationName]",IsCorrect=false},

                new Choice{QuestionId=questions[24].Id,Text="dotnet ef database update",IsCorrect=true},
                new Choice{QuestionId=questions[24].Id,Text="dotnet ef update database",IsCorrect=false},
                new Choice{QuestionId=questions[24].Id,Text="ef database apply",IsCorrect=false},

                new Choice{QuestionId=questions[25].Id,Text="To represent a collection of entities in the DbContext.",IsCorrect=true},
                new Choice{QuestionId=questions[25].Id,Text="To define a database table.",IsCorrect=false},
                new Choice{QuestionId=questions[25].Id,Text="To execute raw SQL queries.",IsCorrect=false},

                new Choice{QuestionId=questions[26].Id,Text="Eager loads related data immediately, explicit loads on demand, lazy loads automatically on access.",IsCorrect=true},
                new Choice{QuestionId=questions[26].Id,Text="They are all the same.",IsCorrect=false},
                new Choice{QuestionId=questions[26].Id,Text="Lazy loads immediately, eager loads on demand, explicit loads automatically on access.",IsCorrect=false},

                new Choice{QuestionId=questions[27].Id,Text="To configure the model and relationships.",IsCorrect=true},
                new Choice{QuestionId=questions[27].Id,Text="To define database connection strings.",IsCorrect=false},
                new Choice{QuestionId=questions[27].Id,Text="To seed initial data.",IsCorrect=false},

                new Choice{QuestionId=questions[28].Id,Text="_context.Database.ExecuteSqlRaw(\"SELECT * FROM MyTable\")",IsCorrect=true},
                new Choice{QuestionId=questions[28].Id,Text="_context.MyTable.FromSqlRaw(\"SELECT * FROM MyTable\")",IsCorrect=false},
                new Choice{QuestionId=questions[28].Id,Text="_context.ExecuteSql(\"SELECT * FROM MyTable\")",IsCorrect=false},

                new Choice{QuestionId=questions[29].Id,Text="The mechanism that detects changes to entities and persists them to the database.",IsCorrect=true},
                new Choice{QuestionId=questions[29].Id,Text="A way to track user activity in the application.",IsCorrect=false},
                new Choice{QuestionId=questions[29].Id,Text="A feature for version control of the database.",IsCorrect=false},

                // Python Choices
                new Choice{QuestionId=questions[30].Id,Text=".py",IsCorrect=true},
                new Choice{QuestionId=questions[30].Id,Text=".python",IsCorrect=false},
                new Choice{QuestionId=questions[30].Id,Text=".pt",IsCorrect=false},

                new Choice{QuestionId=questions[31].Id,Text="x = 5",IsCorrect=true},
                new Choice{QuestionId=questions[31].Id,Text="int x = 5",IsCorrect=false},
                new Choice{QuestionId=questions[31].Id,Text="variable x = 5",IsCorrect=false},

                new Choice{QuestionId=questions[32].Id,Text="#This is a comment",IsCorrect=true},
                new Choice{QuestionId=questions[32].Id,Text="//This is a comment",IsCorrect=false},
                new Choice{QuestionId=questions[32].Id,Text="/*This is a comment*/",IsCorrect=false},

                new Choice{QuestionId=questions[33].Id,Text="string",IsCorrect=true},
                new Choice{QuestionId=questions[33].Id,Text="char",IsCorrect=false},
                new Choice{QuestionId=questions[33].Id,Text="text",IsCorrect=false},

                new Choice{QuestionId=questions[34].Id,Text="my_list = [1, 2, 3]",IsCorrect=true},
                new Choice{QuestionId=questions[34].Id,Text="my_list = (1, 2, 3)",IsCorrect=false},
                new Choice{QuestionId=questions[34].Id,Text="my_list = {1, 2, 3}",IsCorrect=false},

                new Choice{QuestionId=questions[35].Id,Text="len()",IsCorrect=true},
                new Choice{QuestionId=questions[35].Id,Text="length()",IsCorrect=false},
                new Choice{QuestionId=questions[35].Id,Text="size()",IsCorrect=false},

                new Choice{QuestionId=questions[36].Id,Text="List",IsCorrect=true},
                new Choice{QuestionId=questions[36].Id,Text="Tuple",IsCorrect=false},
                new Choice{QuestionId=questions[36].Id,Text="String",IsCorrect=false},

                new Choice{QuestionId=questions[37].Id,Text="for x in y:",IsCorrect=true},
                new Choice{QuestionId=questions[37].Id,Text="for x in y()",IsCorrect=false},
                new Choice{QuestionId=questions[37].Id,Text="for each x in y:",IsCorrect=false},

                new Choice{QuestionId=questions[38].Id,Text="To define a function.",IsCorrect=true},
                new Choice{QuestionId=questions[38].Id,Text="To declare a variable.",IsCorrect=false},
                new Choice{QuestionId=questions[38].Id,Text="To create a class.",IsCorrect=false},

                new Choice{QuestionId=questions[39].Id,Text="import module_name",IsCorrect=true},
                new Choice{QuestionId=questions[39].Id,Text="include module_name",IsCorrect=false},
                new Choice{QuestionId=questions[39].Id,Text="using module_name",IsCorrect=false},

                // JavaScript Choices
                new Choice{QuestionId=questions[40].Id,Text="var myVar;",IsCorrect=true},
                new Choice{QuestionId=questions[40].Id,Text="variable myVar;",IsCorrect=false},
                new Choice{QuestionId=questions[40].Id,Text="v myVar;",IsCorrect=false},

                new Choice{QuestionId=questions[41].Id,Text="String",IsCorrect=true},
                new Choice{QuestionId=questions[41].Id,Text="Integer",IsCorrect=false},
                new Choice{QuestionId=questions[41].Id,Text="Character",IsCorrect=false},

                new Choice{QuestionId=questions[42].Id,Text="To attach an event handler to an element.",IsCorrect=true},
                new Choice{QuestionId=questions[42].Id,Text="To create a new HTML element.",IsCorrect=false},
                new Choice{QuestionId=questions[42].Id,Text="To change the CSS style of an element.",IsCorrect=false},

                new Choice{QuestionId=questions[43].Id,Text="// This is a comment",IsCorrect=true},
                new Choice{QuestionId=questions[43].Id,Text="<!-- This is a comment -->",IsCorrect=false},
                new Choice{QuestionId=questions[43].Id,Text="# This is a comment",IsCorrect=false},

                new Choice{QuestionId=questions[44].Id,Text="'==' performs type coercion, '===' does not.",IsCorrect=true},
                new Choice{QuestionId=questions[44].Id,Text="'===' performs type coercion, '==' does not.",IsCorrect=false},
                new Choice{QuestionId=questions[44].Id,Text="There is no difference.",IsCorrect=false},

                new Choice{QuestionId=questions[45].Id,Text="A function that has access to its outer function's scope, even after the outer function has returned.",IsCorrect=true},
                new Choice{QuestionId=questions[45].Id,Text="A special type of variable.",IsCorrect=false},
                new Choice{QuestionId=questions[45].Id,Text="A built-in JavaScript method.",IsCorrect=false},

                new Choice{QuestionId=questions[46].Id,Text="It refers to the object that is executing the current function.",IsCorrect=true},
                new Choice{QuestionId=questions[46].Id,Text="It refers to the global window object.",IsCorrect=false},
                new Choice{QuestionId=questions[46].Id,Text="It refers to the parent element of the current element.",IsCorrect=false},

                new Choice{QuestionId=questions[47].Id,Text="Document Object Model",IsCorrect=true},
                new Choice{QuestionId=questions[47].Id,Text="Data Object Model",IsCorrect=false},
                new Choice{QuestionId=questions[47].Id,Text="Dynamic Object Model",IsCorrect=false},

                new Choice{QuestionId=questions[48].Id,Text="var myArray = [];",IsCorrect=true},
                new Choice{QuestionId=questions[48].Id,Text="var myArray = ();",IsCorrect=false},
                new Choice{QuestionId=questions[48].Id,Text="var myArray = {};",IsCorrect=false},

                new Choice{QuestionId=questions[49].Id,Text="JavaScript Object Notation",IsCorrect=true},
                new Choice{QuestionId=questions[49].Id,Text="JavaScript Online Notation",IsCorrect=false},
                new Choice{QuestionId=questions[49].Id,Text="JavaScript Object Naming",IsCorrect=false},

                // DevOps Choices
                new Choice{QuestionId=questions[50].Id,Text="Integration issues by merging and testing changes frequently.",IsCorrect=true},
                new Choice{QuestionId=questions[50].Id,Text="Manual deployments to production.",IsCorrect=false},
                new Choice{QuestionId=questions[50].Id,Text="Tracking application logs.",IsCorrect=false},

                new Choice{QuestionId=questions[51].Id,Text="They package code and dependencies into a consistent runtime.",IsCorrect=true},
                new Choice{QuestionId=questions[51].Id,Text="They automatically scale databases.",IsCorrect=false},
                new Choice{QuestionId=questions[51].Id,Text="They replace source control systems.",IsCorrect=false},

                new Choice{QuestionId=questions[52].Id,Text="Reduce final image size by separating build and runtime stages.",IsCorrect=true},
                new Choice{QuestionId=questions[52].Id,Text="Encrypt secrets directly into the image.",IsCorrect=false},
                new Choice{QuestionId=questions[52].Id,Text="Enable GPU support by default.",IsCorrect=false},

                new Choice{QuestionId=questions[53].Id,Text="On every push or pull request before deployment.",IsCorrect=true},
                new Choice{QuestionId=questions[53].Id,Text="Only after production deployment.",IsCorrect=false},
                new Choice{QuestionId=questions[53].Id,Text="Only during monthly maintenance windows.",IsCorrect=false},

                new Choice{QuestionId=questions[54].Id,Text="Defining pipeline steps and configuration.",IsCorrect=true},
                new Choice{QuestionId=questions[54].Id,Text="Styling HTML pages.",IsCorrect=false},
                new Choice{QuestionId=questions[54].Id,Text="Managing database backups.",IsCorrect=false},

                new Choice{QuestionId=questions[55].Id,Text="It keeps credentials out of source control.",IsCorrect=true},
                new Choice{QuestionId=questions[55].Id,Text="It makes builds faster by default.",IsCorrect=false},
                new Choice{QuestionId=questions[55].Id,Text="It removes the need for environment variables.",IsCorrect=false},

                new Choice{QuestionId=questions[56].Id,Text="docker build -t myapp .",IsCorrect=true},
                new Choice{QuestionId=questions[56].Id,Text="docker start myapp",IsCorrect=false},
                new Choice{QuestionId=questions[56].Id,Text="docker push myapp",IsCorrect=false},

                new Choice{QuestionId=questions[57].Id,Text="All stages (build, test, package) succeed without errors.",IsCorrect=true},
                new Choice{QuestionId=questions[57].Id,Text="There are warnings but tests were skipped.",IsCorrect=false},
                new Choice{QuestionId=questions[57].Id,Text="Manual approvals are ignored to save time.",IsCorrect=false}
            };
            foreach (Choice ch in choices)
            {
                context.Choices.Add(ch);
            }
            context.SaveChanges();
                return;
            }

            // Add the DevOps course and its content if it was introduced after the initial seed.
            if (!context.Courses.Any(c => c.Title == "DevOps and CI/CD Foundations"))
            {
                var devOpsCourse = new Course
                {
                    Title = "DevOps and CI/CD Foundations",
                    Description = "Learn how to ship code with Docker, pipelines, and basic observability.",
                    ImageUrl = "/images/ci-cd.png"
                };
                context.Courses.Add(devOpsCourse);
                context.SaveChanges();

                var devOpsLessons = new Lesson[]
                {
                    new Lesson{CourseId=devOpsCourse.Id,Title="Why DevOps Matters",Content="Understand how DevOps practices shorten feedback loops and improve release quality.", YouTubeLink="https://www.youtube.com/watch?v=Xrgk023l4lI"},
                    new Lesson{CourseId=devOpsCourse.Id,Title="Containerizing with Docker",Content="Write a Dockerfile, build multi-stage images, and run containers locally.", YouTubeLink="https://www.youtube.com/watch?v=0qotVMX-J5s"},
                    new Lesson{CourseId=devOpsCourse.Id,Title="CI/CD with GitHub Actions",Content="Create a build-and-test pipeline, add caching, and gate deployments.", YouTubeLink="https://www.youtube.com/watch?v=R8_veQiYBjI"}
                };
                foreach (Lesson lesson in devOpsLessons)
                {
                    context.Lessons.Add(lesson);
                }
                context.SaveChanges();

                var devOpsQuiz = new Quiz { CourseId = devOpsCourse.Id, Title = "DevOps Foundations Quiz" };
                context.Quizzes.Add(devOpsQuiz);
                context.SaveChanges();

                var devOpsQuestions = new Question[]
                {
                    new Question{QuizId=devOpsQuiz.Id,Text="What problem does Continuous Integration primarily address?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="Why are container images valuable in DevOps workflows?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="What does a multi-stage Dockerfile help you achieve?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="When should automated tests run in a CI pipeline?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="How is YAML typically used in CI/CD pipelines?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="Why store credentials as encrypted secrets in pipelines?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="Which command builds a Docker image from a Dockerfile?"},
                    new Question{QuizId=devOpsQuiz.Id,Text="What indicates a healthy pipeline run before deployment?"}
                };
                foreach (Question question in devOpsQuestions)
                {
                    context.Questions.Add(question);
                }
                context.SaveChanges();

                var devOpsChoices = new Choice[]
                {
                    new Choice{QuestionId=devOpsQuestions[0].Id,Text="Integration issues by merging and testing changes frequently.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[0].Id,Text="Manual deployments to production.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[0].Id,Text="Tracking application logs.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[1].Id,Text="They package code and dependencies into a consistent runtime.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[1].Id,Text="They automatically scale databases.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[1].Id,Text="They replace source control systems.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[2].Id,Text="Reduce final image size by separating build and runtime stages.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[2].Id,Text="Encrypt secrets directly into the image.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[2].Id,Text="Enable GPU support by default.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[3].Id,Text="On every push or pull request before deployment.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[3].Id,Text="Only after production deployment.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[3].Id,Text="Only during monthly maintenance windows.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[4].Id,Text="Defining pipeline steps and configuration.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[4].Id,Text="Styling HTML pages.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[4].Id,Text="Managing database backups.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[5].Id,Text="It keeps credentials out of source control.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[5].Id,Text="It makes builds faster by default.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[5].Id,Text="It removes the need for environment variables.",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[6].Id,Text="docker build -t myapp .",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[6].Id,Text="docker start myapp",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[6].Id,Text="docker push myapp",IsCorrect=false},

                    new Choice{QuestionId=devOpsQuestions[7].Id,Text="All stages (build, test, package) succeed without errors.",IsCorrect=true},
                    new Choice{QuestionId=devOpsQuestions[7].Id,Text="There are warnings but tests were skipped.",IsCorrect=false},
                    new Choice{QuestionId=devOpsQuestions[7].Id,Text="Manual approvals are ignored to save time.",IsCorrect=false}
                };
                foreach (Choice choice in devOpsChoices)
                {
                    context.Choices.Add(choice);
                }
                context.SaveChanges();
            }

            var lessonLinkFixes = new (string Title, string Url)[]
            {
                ("Why DevOps Matters", "https://www.youtube.com/watch?v=Xrgk023l4lI"),
                ("Containerizing with Docker", "https://www.youtube.com/watch?v=0qotVMX-J5s"),
                ("Working with Data in ASP.NET Core", "https://www.youtube.com/watch?v=SryQxUeChMc")
            };

            var titlesToFix = lessonLinkFixes.Select(l => l.Title).ToList();
            var lessonsToFix = context.Lessons
                .Where(l => titlesToFix.Contains(l.Title))
                .ToList();

            foreach (var lesson in lessonsToFix)
            {
                var fix = lessonLinkFixes.First(l => l.Title == lesson.Title);
                if (!string.Equals(lesson.YouTubeLink, fix.Url, StringComparison.OrdinalIgnoreCase))
                {
                    lesson.YouTubeLink = fix.Url;
                }
            }

            if (lessonsToFix.Any())
            {
                context.SaveChanges();
            }
        }
    }
}

