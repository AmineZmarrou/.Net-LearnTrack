# .Net-LearnTrack

LearnTrack is a lightweight e-learning platform built with ASP.NET Core MVC. It lets users browse courses, watch lessons, track progress, and complete quizzes to earn certificates.

## Features
- Course catalog with lesson details
- Progress tracking per lesson and per course
- Quizzes with scoring and pass/fail logic
- Certificates for completed courses
- Admin area for user and course management

## Tech Stack
- Backend: ASP.NET Core MVC (.NET 8)
- ORM: Entity Framework Core 8
- Database: MySQL (Pomelo provider)
- Frontend: Razor Views, Bootstrap, jQuery

## Getting Started

### Prerequisites
- .NET 8 SDK
- MySQL 8+

### Setup
1) Clone the repository and open the solution:
   - `Projet_Binome.sln`
2) Configure your database connection string:
   - Update `Projet_Binome/appsettings.json` under `ConnectionStrings:DefaultConnection`
3) Run the application:
   - `dotnet run --project Projet_Binome`

On startup, the app applies EF Core migrations and seeds the database.

### Default Admin Account
The database seeding creates an admin account if it does not exist. Change the password after first login.

### Useful Commands
```bash
dotnet restore
dotnet build
dotnet run --project Projet_Binome
```

## Project Structure
- `Projet_Binome/Controllers` - MVC controllers
- `Projet_Binome/Views` - Razor views
- `Projet_Binome/Models` - Entities and view models
- `Projet_Binome/Data` - DbContext and seed logic
- `Projet_Binome/wwwroot` - Static assets

Directed by Amine Zmarrou
