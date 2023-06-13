using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AIUniversity.Models;
using System.Collections.Generic;
using System.Linq; 
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AIUniversity.Controllers 
{
  [Authorize]

  public class ClubsController : Controller
  {
    private readonly AIUniversityContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public ClubsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;

      _db = db;
    }
    
    public ActionResult Index()
    {
      List<Club> allClubs = _db.Clubs
      .OrderBy(club => club.ClubName).ToList();                            
      return View(allClubs);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Club club)
    {
      if (!ModelState.IsValid)
      {
        return View(club);
      }
      else
      {
        _db.Clubs.Add(club);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
                             .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    public ActionResult Edit(int id)
    {
      Club thisClub = _db.Clubs.FirstOrDefault(club => club.ClubId == id);
      return View(thisClub);
    }

    [HttpPost]
    public ActionResult Edit(Club club)
    {
      if(!ModelState.IsValid)
      {
        return View(club);
      }
      else 
      {
        _db.Club.Update(club);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Club thisClub = _db.Clubs.FirstOrDefault(club => club.ClubId == id);
      return View(thisClub);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Club thisClub = _db.Clubs.FirstOrDefault(club => club.ClubId == id);
      _db.Clubs.Remove(thisClub);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
    [HttpPost]
    public ActionResult AddStudent(Club club, int studentId)
    {
      #nullable enable
      StudentClub? joinEntity = _db.StudentClub.FirstOrDefault(join => (join.StudentId == studentId && join.ClubId == club.ClubId));
      #nullable disable

      if (joinEntity == null && studentId != 0)
      {
        _db.StudentClubs.Add(new StudentClub() { StudentId = studentId, Club = club.ClubId });
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = club.ClubId });
    }  

    

    




   
    
  }
}
 
