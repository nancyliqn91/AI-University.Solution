using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AIUniversity.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    [Required(ErrorMessage = "Student must have a first name!")]
    public string StudentFirstName { get; set; }
    [Required(ErrorMessage = "Student must have a last name!")]
    public string StudentLastName { get; set; }    
    [Required(ErrorMessage = "Must add birthday!")]
    public DateTime StudentDateOfBirth { get; set; }
       
    [Required(ErrorMessage = "Must enter student email!")]
    public string StudentEmail { get; set; }   

    public int DormId {get;set;}
    public Dorm StudentDorm { get; set; }

    public int DepartmentId { get; set; }
    public Department StudentDepartment { get; set; }
    
    public List<StudentClub> StudentClubs { get; set; }
    public List<StudentCourse> StudentCourses { get; set;}

  }
}