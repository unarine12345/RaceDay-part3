# RaceDay Event Management System

## 1. Project Description

RaceDay is a web-based event management system developed using ASP.NET Core MVC and ASP.NET Core Web API. The system allows event organisers to create and manage events, while participants can browse events, enrol in available categories and view their results.

The MVC application communicates with the RESTful API for data operations. It does not connect directly to the database.

## 2. Technologies Used

- C#
- ASP.NET Core MVC (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- HTML, CSS and Bootstrap
- JavaScript
- Azure Blob Storage
- Docker
- GitHub Actions

## 3. User Roles

### Organiser
Organisers can:
- Register and log in.
- Access the organiser dashboard.
- Create and manage events.
- Manage event categories.
- Manage participant results.

### Participant
Participants can:
- Register and log in.
- View available events.
- Browse event categories.
- Enrol in events.
- View their event enrolments.
- View their results.

## 4. Project Structure

The solution contains the following main projects:

- **RaceDay.API** – Handles API requests, authentication, business logic and database operations.
- **RaceDay.MVC** – Provides the website interface and communicates with the API.
- **RaceDay.Tests** – Contains automated tests for the application.

## 5. Requirements

Before running the project, install:

- Visual Studio 2022.
- .NET 8 SDK.
- SQL Server.
- SQL Server Management Studio (SSMS).
- Docker Desktop (for containerisation).
- An Azure Storage account (for Azure Blob Storage features).

## 6. Database Configuration

1. Open the RaceDay solution in Visual Studio.
2. Open the `appsettings.json` file in the RaceDay.API project.
3. Locate the `DefaultConnection` connection string.
4. Update it to match your SQL Server instance.
5. Ensure the RaceDay database has been created and the required tables are available.

Example connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=RaceDayDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace `YOUR_SERVER` with your SQL Server instance name.

## 7. Running the Application

1. Open `RaceDay.sln` in Visual Studio.
2. Restore the NuGet packages.
3. Configure the API connection string.
4. Confirm that the MVC project's API base URL points to the running API.
5. Configure Visual Studio to start both `RaceDay.API` and `RaceDay.MVC`.
6. Build the solution.
7. Run the application.
8. Open the MVC website in your browser.
9. Register an account and log in.

The `RaceDay.Tests` project is a test project and should not be selected as a web application startup project.

## 8. Authentication

The application supports registration and login for two user roles:

- Organiser
- Participant

After successful login, users should be directed to the appropriate role-based interface.

## 9. Azure Blob Storage

Azure Blob Storage is intended to store application images, such as event images. Configure the Azure Storage connection settings before testing image uploads.

Do not publish storage account keys or connection strings to GitHub.

## 10. Docker

The project includes Docker configuration for containerising the MVC application.

Docker Desktop must be installed and running before building or running the container. The API must also be available to the MVC application.

## 11. Testing

The RaceDay.Tests project contains automated tests.

To run tests:

1. Open Visual Studio.
2. Select **Test** from the menu.
3. Select **Run All Tests**.
4. Review the Test Explorer results.

Registration, login, role-based access and the main event management features should also be tested manually.

## 12. Author

**Project:** RaceDay Event Management System  
**Module:** [Insert module name]  
**Student Name:** [Insert your name]  
**Student Number:** [Insert student number]  
**Year:** 2026

## 13. Conclusion

The RaceDay Event Management System aims to simplify the management of sporting events by providing separate interfaces for organisers and participants. It uses an ASP.NET Core MVC front end connected to a RESTful Web API, with SQL Server for data storage. The project also includes planned integration with Azure Blob Storage and Docker containerisation.
