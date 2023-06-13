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
  public class DepartmentsController : Controller
  {
    private readonly AIUniversityContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public DepartmentsController(UserManager<ApplicationUser> userManager, AIUniversityContext db)
    {
      _userManager = userManager;

      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Department> allDepartments = _db.Departments.ToList();
      return View(allDepartments);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Department department)
    {
      if (!ModelState.IsValid)
      {
        return View(department);
      }
      else
      {
        _db.Departments.Add(department);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id)
    {
      //Department department = _db.Departments.ToList();
      Department thisdepartment = _db.Departments.FirstOrDefault(dep => dep.DepartmentId == id);
      return View(department);
    }

    public ActionResult Edit(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(dep => dep.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult Edit(Department department)
    {
      if(!ModelState.IsValid)
      {
        return View(department);
      }
      else 
      {
        _db.Departments.Update(department);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(dep => dep.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Department thisDepartment = _db.Departments.FirstOrDefault(dep => dep.DepartmentId == id);
      _db.Departments.Remove(thisDepartment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

}