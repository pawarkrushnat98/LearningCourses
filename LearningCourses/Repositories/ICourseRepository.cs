﻿using LearningCourses.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningCourses.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);

        Task<IPagedList<Course>> GetPagedCoursesAsync(int pageNumber, int pageSize);
    }
}
