# news

News Management Web Application

A simple web application built with ASP.NET Core Razor for creating, reading, and deleting news articles. It features image uploads for both a main headline image and a gallery of additional images.

Features

- View a list of all news articles sorted by the newest first.
- Click on a news card to see a detailed view of the article.
- Add new news articles via a form.
- Upload a headline image for each article.
- Upload multiple additional images for a gallery on the detail page.
- Manage and delete existing news articles and their associated images from a dedicated page.

Technology Stack

- Framework: ASP.NET Core 8.0 (or your version)
- Language: C#
- Database: SQL Server (via Entity Framework Core)
- ORM: Entity Framework Core
- Frontend: HTML, CSS, JavaScript

Prerequisites
Before you begin, ensure you have the following installed on your system:


- .NET SDK: Download .NET 8.0 SDK or a version compatible with the project.
- IDE:
    - Visual Studio 2022 (Download here) with the "ASP.NET and web development" workload installed.
    or Visual Studio Code (Download here).
    - Database Server: SQL Server Express or Developer Edition (Download here).
  
- .NET EF Core Tools: The command-line tool for managing migrations. If you don't have it installed, run this command in your terminal:
code

use -> 
dotnet tool install --global dotnet-ef

git clone 

dotnet ef database update

docker-compose up --build

and Done!!!!
