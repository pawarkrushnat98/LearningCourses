using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningCourses.Data;
using LearningCourses.Models;
using LearningCourses.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Xunit;

namespace LearningCourses.Tests
{
    public class CourseRepositoryTests
    {
        private readonly CourseRepository _repository;
        private readonly ApplicationDbContext _context;

        public CourseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new CourseRepository(_context);
        }

        [Fact]
        public async Task AddAsync_AddsCourse()
        {
            
            var course = new Course { Id = 1, Name = "Test Course", Description = "Test Description", Category = "Test Category" };

         
            await _repository.AddAsync(course);
            var retrievedCourse = await _context.Courses.FindAsync(1);

          
            Assert.NotNull(retrievedCourse);
            Assert.Equal("Test Course", retrievedCourse.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourse()
        {
            
            var course = new Course { Id = 2, Name = "Course 2", Description = "Description 2", Category = "Category 2" };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(2);

            
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCourse()
        {
            
            var course = new Course { Id = 3, Name = "Course 3", Description = "Description 3", Category = "Category 3" };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            
            await _repository.DeleteAsync(3);
            var deletedCourse = await _context.Courses.FindAsync(3);

            Assert.Null(deletedCourse);
        }
    }
}
