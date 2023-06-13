using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AIUniversity.Models
{
  public class Professor
  {
    public int ProfessorId { get; set; }

    [Required(ErrorMessage = "Professor must have a first name!")]
    public string ProfessorFirstName { get; set; }
    [Required(ErrorMessage = "Prodessor must have a last name!")]
    public string ProfessorLastName { get; set; }

    [Required(ErrorMessage = "The professor must have a description!")]
    public string ProfessorDescription { get; set;}
    [Required(ErrorMessage = "Must enter birth date")]
    public DateTime ProfessorDateOfBirth { get; set; }
    [Required(ErrorMessage = "Professor must have a email!")]
    public string ProfessorEmail { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public List<Course> Courses { get; set; }
    
  }
}