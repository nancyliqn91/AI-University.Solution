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
    public ActionResult Create(Dorm dorm)
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
      Dorm thisDorm = _db.Dorms
                              .Include(dorm => dorm.Students)
                              .FirstOrDefault(dorm => dorm.DormId == id);
      return View(thisDorm);                    
    }

    public ActionResult Edit(int id)
    {
      Dorm thisDorm = _db.Dorms.FirstOrDefault(dorm => dorm.DormId == id);
      return View(thisDorm);
    }

    [HttpPost]
    public ActionResult Edit(Dorm dorm)
    {
      _db.Dorms.Update(dorm);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Dorm thisDorm = _db.Dorms.FirstOrDefault(dorm => dorm.DormId == id);
      return View(thisDorm);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Dorm thisDorm = _db.Dorms.FirstOrDefault(dorm => dorm.DormId == id);
      _db.Dorms.Remove(thisDorm);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}