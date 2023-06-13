using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIUniversity.Models
{
  public class Club
  {
    public int ClubId { get; set;}
    
    [Required(ErrorMessage = "The Club's name can't be empty!")]
    public string ClubName { get; set; }
    
    [Required(ErrorMessage = "The club must have a description!")]
    public string ClubDescription { get; set; }

    public string ClubPresident { get; set;}
    
    public List<StudentClub> StudentClubs { get; set;}

  }
}