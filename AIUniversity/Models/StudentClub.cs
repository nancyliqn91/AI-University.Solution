namespace AIUniversity.Models
{
  public class StudentClub
  {       
    public int StudentClubId { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int ClubId { get; set; }
    public Club Club { get; set; }
  }
}