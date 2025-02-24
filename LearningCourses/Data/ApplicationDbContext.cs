using LearningCourses.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LearningCourses.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
