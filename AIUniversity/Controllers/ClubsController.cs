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
    [AllowAnonymous]
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
    public ActionResult Create(Club club)
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
      Club thisClub = _db.Clubs
                        .Include(club => club.StudentClubs)
                        .ThenInclude(join => join.Student)
                        .FirstOrDefault(club => club.ClubId == id);
      return View(thisClub);
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
        _db.Clubs.Update(club);
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
    
    public ActionResult AddStudent(int id)
    {
      Club thisClub = _db.Clubs.FirstOrDefault(clubs => clubs.ClubId == id);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "StudentLastName");

 
      return View(thisClub);
    }

    [HttpPost]
    public ActionResult AddStudent(Club club, int studentId)
    {
      #nullable enable
      StudentClub? joinEntity = _db.StudentClubs.FirstOrDefault(join => (join.StudentId == studentId && join.ClubId == club.ClubId));
      #nullable disable

      if (joinEntity == null && studentId != 0)
      {
        _db.StudentClubs.Add(new StudentClub() { StudentId = studentId, ClubId = club.ClubId });
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = club.ClubId });
    }  

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      StudentClub joinEntry = _db.StudentClubs.FirstOrDefault(entry => entry.StudentClubId == joinId);
      _db.StudentClubs.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
 
