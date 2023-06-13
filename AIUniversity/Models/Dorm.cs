using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIUniversity.Models
{
  public class Dorm
  {
    public int DormId { get; set;}
    
    [Required(ErrorMessage = "The dorm must have a name!")]
    public string DormName { get; set; }   

    public List<Student> Students { get; set; }

  }
}