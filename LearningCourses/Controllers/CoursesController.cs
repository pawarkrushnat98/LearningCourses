using LearningCourses.Models;
using LearningCourses.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningCourses.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            try
            {
                _logger.LogInformation("Fetching courses for page {PageNumber}", pageNumber);
                var pagedCourses = await _courseRepository.GetPagedCoursesAsync(pageNumber, pageSize);
                return View(pagedCourses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching courses for page {PageNumber}", pageNumber);
                return View("Error");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation("Fetching details for course ID: {CourseId}", id);
                var course = await _courseRepository.GetByIdAsync(id);

                if (course == null)
                {
                    _logger.LogWarning("Course with ID {CourseId} not found.", id);
                    return NotFound();
                }

                return Json(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching details for course ID {CourseId}", id);
                return BadRequest("An error occurred while fetching course details.");
            }
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("Opening Create Course page.");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Course creation failed due to invalid model state.");
                return View(course);
            }

            try
            {
                _logger.LogInformation("Creating new course: {CourseName}", course.Name);
                await _courseRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course: {CourseName}", course.Name);
                return View("Error");
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                _logger.LogInformation("Fetching course for editing. Course ID: {CourseId}", id);
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null)
                {
                    _logger.LogWarning("Course with ID {CourseId} not found.", id);
                    return NotFound();
                }
                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching course for editing. Course ID: {CourseId}", id);
                return View("Error");
            }
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Course course)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Course update failed due to invalid model state.");
                return View(course);
            }

            try
            {
                _logger.LogInformation("Updating course: {CourseName}", course.Name);
                await _courseRepository.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course: {CourseName}", course.Name);
                return View("Error");
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting course with ID: {CourseId}", id);
                await _courseRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course with ID: {CourseId}", id);
                return View("Error");
            }
        }
    }
}
