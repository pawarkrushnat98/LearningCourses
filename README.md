
This is a "Learning Courses Management" application built using ASP.NET Core MVC with SQL Server as the database.
The application allows users to manage courses by performing CRUD operations.

Features:
- Add, update, delete, and view courses.
- Pagination for course listing.
- Uses `X.PagedList` for efficient data handling.
- Unit tests using xUnit and Moq.
- In-memory database testing for repository layer.

Ensure you have the following installed:
- .NET 6.0 or later
- SQL Server 
- Visual Studio 
- Entity Framework Core

 Installation-

1.Clone the Repository
git clone https://github.com/pawarkrushnat98/LearningCourses.git
cd LearningCourses

2.Modify the appsettings.json file:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LearningCoursesDB;Trusted_Connection=True;"
}

3.Apply Migrations & Seed Data.
4.Run the application using visual studio.
