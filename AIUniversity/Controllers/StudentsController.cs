using Microsoft.AspNetCore.Mvc;
using AIUniversity.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AIUniversity.Controllers;

  [Authorize]

  public class StudentsController : Controller
  {
    private readonly AIUniversityContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public StudentsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Student> myStudents = _db.Students
                            .OrderBy(student => student.StudentLastName)
                            .ToList();
      return View(myStudents);
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
          .Include(student => student.StudentClubs)
          .ThenInclude(join => join.Club)
          .Include(student => student.StudentCourses)
          .ThenInclude(join => join.Course)
          .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      ViewBag.DormId = new SelectList(_db.Dorms, "DormId", "DormName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
        ViewBag.DormId = new SelectList(_db.Dorms, "DormId", "DormName");
        return View(student);
      }
      // for full name
      else
      {
         student.StudentFullName = student.StudentFirstName;
         
        _db.Students.Add(student);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

    }
    
    public ActionResult AddCourse(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourse(Student student, int courseId)
    {
      #nullable enable
      StudentCourse? joinEntity = _db.StudentCourses.FirstOrDefault(join => (join.StudentId == student.StudentId && join.CourseId == courseId));
      #nullable disable
      
      if (joinEntity == null && courseId != 0)
      {
        _db.StudentCourses.Add(new StudentCourse() { CourseId = courseId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    public ActionResult AddClub(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      ViewBag.ClubId = new SelectList(_db.Clubs, "ClubId", "ClubName");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddClub(Student student, int clubId)
    {
      #nullable enable
      StudentClub? joinEntity = _db.StudentClubs.FirstOrDefault(join => (join.StudentId == student.StudentId && join.ClubId == clubId));
      #nullable disable
      
      if (joinEntity == null && clubId != 0)
      {
        _db.StudentClubs.Add(new StudentClub() { ClubId = clubId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    public ActionResult Edit(int id)
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      ViewBag.DormId = new SelectList(_db.Dorms, "DormId", "DormName");
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
      return RedirectToAction("Index");
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");

    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      StudentCourse joinEntry = _db.StudentCourses.FirstOrDefault(entry => entry.StudentCourseId == joinId);
      Student thisStudent = _db.Students.FirstOrDefault(entry => entry.StudentId == joinEntry.StudentId);
      _db.StudentCourses.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
}