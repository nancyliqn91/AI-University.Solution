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

  public class CoursesController : Controller
  {
    private readonly AIUniversityContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public CoursesController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Course> allCourses = _db.Courses
      // add department and professor
      .Include(course => course.Department)
      .Include(course => course.Professor)

      .OrderBy(course => course.CourseName).ToList();
      return View(allCourses);
    }

    public async Task<ActionResult> MyCourses()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      List<Course> userCourses = _db.Courses
      .Where(entry => entry.User.Id == currentUser.Id)
      .OrderBy(course => course.CourseName).ToList();
      return View(userCourses);
    }

    public ActionResult Create()
    {
      // STORE DEPARTMENTS IN VIEWBAG
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      ViewBag.ProfessorId = new SelectList(_db.Professors, "ProfessorId", "ProfessorLastName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Course course)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
        ViewBag.ProfessorId = new SelectList(_db.Professors, "ProfessorId", "ProfessorLastName");
        return View(course);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        course.User = currentUser;

        _db.Courses.Add(course);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
                          .Include(item => item.Department)
                          .Include(item => item.Professor)
                             .Include(course => course.StudentCourses)
                             .ThenInclude(join => join.Student)
                             .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "DepartmentName");
      ViewBag.ProfessorId = new SelectList(_db.Professors, "ProfessorId", "ProfessorLastName");

      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      // if (thisCourse.User == currentUser)
      // {
      //   return View(thisCourse);
      // }
      // else
      // {
      //   return RedirectToAction("Index");
      // }
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> Delete(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      if (thisCourse.User == currentUser)
      {
        return View(thisCourse);
      }
      else
      {
        return RedirectToAction("Index");
      }

    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      _db.Courses.Remove(thisCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddStudent(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      List<Student> userStudents = _db.Students
                                // .Where(entry => entry.User.Id == currentUser.Id)
                                .OrderBy(student => student.StudentLastName)
                                .ToList();

      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);

      ViewBag.StudentId = new SelectList(userStudents, "StudentId", "StudentFullName");

      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult AddStudent(Course course, int studentId)
    {
      #nullable enable
      StudentCourse? joinEntity = _db.StudentCourses.FirstOrDefault(join => (join.StudentId == studentId && join.CourseId == course.CourseId));
      #nullable disable

      if (joinEntity == null && studentId != 0)
      {
        _db.StudentCourses.Add(new StudentCourse() { StudentId = studentId, CourseId = course.CourseId });
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = course.CourseId });
    }

    [HttpPost]
    public async Task<ActionResult> DeleteJoin(int joinId)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      StudentCourse joinEntry = _db.StudentCourses.FirstOrDefault(entry => entry.StudentCourseId == joinId);
      Course thisCourse = _db.Courses.FirstOrDefault(entry => entry.CourseId == joinEntry.CourseId);
      if (thisCourse.User == currentUser)
      {
        _db.StudentCourses.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }

  }
}

