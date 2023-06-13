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

  public class ProfessorsController : Controller
  {
    private readonly AIUniversityContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public ProfessorsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;

      _db = db;
    }
    
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Professor> allProfessors = _db.Professors
      .OrderBy(professor => professor.ProfessorDateOfBirth).ToList();                            
      return View(allProfessors);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Professor professor)
    {
      if (!ModelState.IsValid)
      {
        return View(professor);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

        professor.User = currentUser;

        _db.Professors.Add(professor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      
    }
   
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Professor thisProfessor = _db.Professors
                             .Include(professor => professor.Deparment)

                             .ThenInclude(professor => professor.Courses)

                             .FirstOrDefault(professor => professor.ProfessorId == id);
      return View(thisProfessor);
    }

    public async Task<ActionResult> Edit(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      Professor thisProfessor = _db.Professors.FirstOrDefault(professor => professor.ProfessorId == id);
      if (thisProfessor.User == currentUser)
      {
        return View(thisProfessor);
      }
      else
      {
        return RedirectToAction("Index");
      }
    }

    [HttpPost]
    public ActionResult Edit(Professor professor)
    {
      _db.Professors.Update(professor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> Delete(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      Professor thisProfessor = _db.Professors.FirstOrDefault(professor => professor.ProfessorId == id);
      if (thisProfessor.User == currentUser)
      {
        return View(thisProfessor);
      }
      else
      {
        return RedirectToAction("Index");
      }
      
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Professor thisProfessor = _db.Professors.FirstOrDefault(professor => professor.ProfessorId == id);
      _db.Professors.Remove(thisProfessor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
 
  }
}
 
