using LearningCourses.Models;
using LearningCourses.Data;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

using X.PagedList.Extensions;



namespace LearningCourses.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetAllAsync() =>
            await _context.Courses.ToListAsync();

        public async Task<Course> GetByIdAsync(int id) =>
            await _context.Courses.FindAsync(id);

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IPagedList<Course>> GetPagedCoursesAsync(int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
                _context.Courses.OrderBy(c => c.Id).ToPagedList(pageNumber, pageSize)
            );
        }

    }
}

