// using Microsoft.AspNetCore.Mvc;
// using AIUniversity.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace AIUniversity.Controllers;

//   [Authorize]

//   public class StudentsController : Controller
//   {
//     private readonly AIUniversityContext _db;
//     private readonly UserManager<ApplicationUser> _userManager;

//     public StudentsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
//     {
//       _userManager = userManager;
//       _db = db;
//     }
    
//     [AllowAnonymous]
//     public ActionResult Index()
//     {
//       List<Student> myStudents = _db.Students
//                             .OrderBy(student => student.StudentLastName)
//                             .ToList();
//       return View(myStudents);
//     }

//     public async Task<ActionResult> MyStudents()
//     {
//       string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
//       List<Student> userStudents = _db.Students
//                                 .Where(entry =>entry.User.Id == currentUser.Id)
//                                 .OrderBy(Student => Student.StudentLastName)
//                                 .ToList();
//       return View(userStudents);
//     }

//     [AllowAnonymous]
//     public ActionResult Details(int id)
//     {
//       Student thisStudent = _db.Students
//           .Include(tag => Student.JoinEntities)
//           // .ThenInclude(join => join.Recipe)
//           .FirstOrDefault(student => student.Student == id);
//       return View(thisStudent);
//     }

//     public ActionResult Create()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Create(Student student)
//     {
//       if (!ModelState.IsValid)
//       {
//         return View(student);
//       }
//       else
//       {
//         string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
//         tag.User = currentUser;

//         _db.Students.Add(student);
//         _db.SaveChanges();
//         return RedirectToAction("Index");
//       }

//     }
    
//     public async Task<ActionResult> AddClass(int id)
//     {
//       string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

//       List<Class> userClasses = _db.Classes
//                                 .Where(e => e.User.Id == currentUser.Id)
//                                 .OrderBy(class => class.ClassName)
//                                 .ToList();
                                
//       Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
//       ViewBag.ClassId = new SelectList(userClasses, "ClassId", "ClassName");
//       return View(thisStudent);
//     }

//     [HttpPost]
//     public ActionResult AddClass(Student student, int classId)
//     {
//       #nullable enable
//       StudentClass? joinEntity = _db.StudentClasses.FirstOrDefault(join => (join.StudentId == classId && join.ClassId == student.ClassId));
//       #nullable disable
      
//       if (joinEntity == null && classId != 0)
//       {
//         _db.StudentClasses.Add(new StudentClass() { ClassId = classId, StudentId = student.StudentId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Details", new { id = student.StudentId });
//     }

//     public async Task<ActionResult> Edit(int id)
//     {
//       string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

//       Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
//       if (thisStudent.User == currentUser)
//       {
//         return View(thisStudent);
//       }
//       else 
//       {
//         return RedirectToAction("Index");
//       }
//     }

//     [HttpPost]
//     public ActionResult Edit(Student student)
//     {
//       _db.Students.Update(student);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public async Task<ActionResult> Delete(int id)
//     {
//       string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

//       Student thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
//       if (thisStudent.User == currentUser)
//       {
//         return View(thisStudent);
//       }
//       else 
//       {
//         return RedirectToAction("Index");
//       }
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       Student thisStudent = _db.Students.FirstOrDefault(students => student.StudentId == id);
      
//       _db.Students.Remove(thisStudent);
//       _db.SaveChanges();
//       return RedirectToAction("Index");

//     }

//     [HttpPost]
//     public async Task<ActionResult> DeleteJoin(int joinId)
//     {
//       string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

//       StudentClass joinEntry = _db.StudentClasses.FirstOrDefault(entry => entry.StudentClassId == joinId);
//       Student thisStudent = _db.Students.FirstOrDefault(entry => entry.StudentId == joinEntry.StudentId);
//       if (thisStudent.User == currentUser)
//       {
//         _db.StudentClasses.Remove(joinEntry);
//         _db.SaveChanges();
//         return RedirectToAction("Index");
//       }
//       else
//       {
//         return RedirectToAction("Index", "Home");
//       }
//     }
// }