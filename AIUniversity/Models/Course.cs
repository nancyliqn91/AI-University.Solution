using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AIUniversity.Models
{
  
  public class Course
  {
    public int CourseId { get; set; }
    
    [Required(ErrorMessage = "The Course's name can't be empty!")]
    public string CourseName { get; set; }
    [Required(ErrorMessage = "The Course's description can't be empty!")]
    public string CourseDescription { get; set;}

    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } 

    public List<StudentCourse> StudentCourses { get; set; }
    
    public ApplicationUser User { get; set; }
  }
}