using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AIUniversity.Models
{
  
  public class Department
  {
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "The department must have a name!")]
    public string DepartmentName { get; set; }

    [Required(ErrorMessage = "The department must have a description!")]
    public string DepartmentDescription { get; set;}

    public List<Professor> Professors { get; set; }
    public List<Course> Courses { get; set; }
    public List<Student> Students {get; set;}
  }
}