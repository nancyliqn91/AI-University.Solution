# 🐄 AI University 🐄

#### By _Qian Li_ _Joe Wahbeh_ _Max Betich_  _Kymani Stephens_   😊

#### This is our c# team week project which creates a fully-functional MVC APP for college campus virtual brochure with dorms, classes, professors, clubs and a student body.

## Technologies Used

* C#
* .NET
* HTML
* MVC
* Entity Framework
* MySQL Workbench
* VS code

## Description

* Users can review all the information about the university on the page. 
* Users can see professors, courses, dorms, and departments details from home page.
* For courses, only logged in users are able to  add, edit, delete, and add student to the course.
* For professors, only logged users can update information, for example, the logged professor can edit their identity information, including department and add courses.
* For students, only logged users can update information, for example, the logged student can edit their identity information, including department and dorm, and add clubs and courses.

## Setup/Installation Requirements

* _Clone “AI University“ from the repository to your desktop_.
* _Navigate to "AI University" directory via your local terminal command line_.
* Run the app, first navigate to this project's production directory called "AIUniversity". 
* Run `dotnet restore` to restore all the packages.
* Add appsettings.json file, please see the "Database Connection String Setup" instruction below.
* Create the database using the migrations in the "AIUniversity" project. Open your shell (e.g., Terminal or GitBash) to the production directory "FantacyRecipe", and `run dotnet ef database update`.
* Within the production directory "AIUniversity", run `dotnet watch run` in the command line to start the project in development mode with a watcher.
* Open the browser to _https://localhost:5001_. If you cannot access localhost:5001 it is likely because you have not configured a .NET developer security certificate for HTTPS. To learn about this, review this lesson: [Redirecting to HTTPS and Issuing a Security Certificate](https://www.learnhowtoprogram.com/c-and-net/basic-web-applications/redirecting-to-https-and-issuing-a-security-certificate).

## Database Connection String Setup 

* Create an appsetting.json file in the "AI University" directory of the project. The example is below.
* Within appsettings.json, put in the following code, replacing the uid and pwd values with your own username and password for MySQL Workbench.


```
AI University/AIUniversity/appsettings.json

 {
    "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;
      database=[Your-DATA-BASE];uid=[YOUR-USER-HERE];
      pwd=[YOUR-PASSWORD-HERE];"
    }
 }
```

## Known Bugs

No bugs 

## License
[MIT](license.txt)
Copyright (c) 2023 Qian Li, Joe Wahbeh, Max Betich and Kymani Stephens

