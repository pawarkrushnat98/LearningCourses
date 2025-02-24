using System.Collections.Generic;
using System.Threading.Tasks;
using LearningCourses.Controllers;
using LearningCourses.Models;
using LearningCourses.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using X.PagedList;

namespace LearningCourses.Tests
{
    public class CoursesControllerTests
    {
        private readonly Mock<ICourseRepository> _mockRepo;
        private readonly Mock<ILogger<CoursesController>> _mockLogger;
        private readonly CoursesController _controller;

        public CoursesControllerTests()
        {
            _mockRepo = new Mock<ICourseRepository>();
            _mockLogger = new Mock<ILogger<CoursesController>>();
            _controller = new CoursesController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewWithPagedCourses()
        {
            
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Course 1", Description = "Description 1", Category = "Category 1" },
                new Course { Id = 2, Name = "Course 2", Description = "Description 2", Category = "Category 2" }
            };
            _mockRepo.Setup(repo => repo.GetPagedCoursesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new StaticPagedList<Course>(courses, 1, 5, courses.Count));

            
            var result = await _controller.Index(1) as ViewResult;

            
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IPagedList<Course>>(result.Model);
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            
            var course = new Course { Id = 1, Name = "New Course", Description = "New Description", Category = "New Category" };

          
            var result = await _controller.Create(course) as RedirectToActionResult;

            
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockRepo.Verify(repo => repo.AddAsync(course), Times.Once);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsView()
        {
            
            var course = new Course { Id = 1, Name = "", Description = "Description", Category = "Category" };
            _controller.ModelState.AddModelError("Name", "Name is required");

            
            var result = await _controller.Create(course) as ViewResult;

         
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ValidId_RedirectsToIndex()
        {
            
            var result = await _controller.Delete(1) as RedirectToActionResult;

            
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
