# HealthChecker
Sample ASP.NET Core web application that executes background jobs using Hangfire.


### If you want to get it running locally, take these steps..
* This application requires .NET 5 to be installed on your computer. If not so, please install it from [here.](https://dotnet.microsoft.com/download)
* Get the solution from github (clone or download)
* Change conntection string in appsettings.json file in Mvc project, if needed. Note that this application uses Sql Server as its database.
* In the Package Manager Console in Visual Studio set the Default Project to Persistence and execute "Update-Database" command.
* Set the startup project Mvc if needed.
* Build and run..
