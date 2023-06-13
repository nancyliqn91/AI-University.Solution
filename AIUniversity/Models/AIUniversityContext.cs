using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AIUniversity.Models
{
  public class AIUniversityContext : IdentityDbContext<ApplicationUser>
  {

    public DbSet<Course> Courses { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Department> Departments { get;set;}
    public DbSet<Dorm> Dorms { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Student> Students { get; set; }

    public DbSet<StudentClub> StudentClubs { get; set;}
    public DbSet<StudentCourse> StudentCourses { get;set; }
    
    public AIUniversityContext(DbContextOptions options) : base(options) { }
  }
}
