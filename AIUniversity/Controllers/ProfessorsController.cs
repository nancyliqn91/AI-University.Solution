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
      .OrderBy(professor => professor.ProfessorDateOfBirth)
      .Include(professor => professor.Department).ToList();                            
      return View(allProfessors);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Professor professor)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
        return View(professor);
      }
      else
      {
        _db.Professors.Add(professor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      
    }
   
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Professor thisProfessor = _db.Professors
                             .Include(professor => professor.Department)

                             .Include(professor => professor.Courses)

                             .FirstOrDefault(professor => professor.ProfessorId == id);
      return View(thisProfessor);
    }

    public ActionResult Edit(int id)
    {
      Professor thisProfessor = _db.Professors.FirstOrDefault(professor => professor.ProfessorId == id);
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      return View (thisProfessor);
    }

    [HttpPost]
    public ActionResult Edit(Professor professor)
    {
      _db.Professors.Update(professor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {

      Professor thisProfessor = _db.Professors.FirstOrDefault(professor => professor.ProfessorId == id);
      return View(thisProfessor);
      
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
 
