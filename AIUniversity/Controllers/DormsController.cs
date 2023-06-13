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
  public class DormsController : Controller
  {
    private readonly AIUniversityContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public DormsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Dorm> allDorms = _db.Dorms
                                .OrderBy(dorm => dorm.DormName)
                                .ToList();
      return View(allDorms);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Dorm dorm)
    {
      if (!ModelState.IsValid)
      {
        return View(dorm);
      }
      else
      {
        _db.Dorms.Add(dorm);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      Dorm thisDorm = _db.Dorms.FirstOrDefault(dorm => dorm.DormId == id);
      return View(thisDorm);                    
    }

    public ActionResult AddStudent(int id)
    {
      Dorm thisDorm = _db.Dorms.FirstOrDefault(dorm => dorm.DormId == id);
      List<Student> students = _db.Students
                                    .Where(entry => entry.DormId == 0)
                                    .OrderBy(student => student.StudentLastName)
                                    .ToList();
      ViewBag.StudentId = new SelectList(students, "StudentId", "StudentLastName");
      return View(thisDorm);
    }

    [HttpPost]
    public ActionResult AddStudent(Dorm dorm, int studentId)
    {
      #nullable enable
      Student? joinStudent = _db.Students.FirstOrDefault(entry => entry.StudentId == studentId);
      #nullable disable
      if (studentId == 0 || dorm.Students.Exists(entry => entry.StudentId == studentId))
      {

      }
      else
      {
        dorm.Students.Add(joinStudent);
        _db.Dorms.Update(dorm);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new {id = dorm.DormId});
    }

    [HttpPost]
    public ActionResult RemoveStudent(Dorm dorm, int studentId)
    {
      #nullable enable
      Student? joinStudent = _db.Students.FirstOrDefault(entry => entry.StudentId == studentId);
      #nullable disable
      if (dorm.Students.Exists(entry => entry.StudentId == studentId))
      {
        dorm.Students.Remove(joinStudent);
        _db.Dorms.Update(dorm);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new {id = dorm.DormId});
    }
  }
}