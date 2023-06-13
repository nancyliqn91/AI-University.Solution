using Microsoft.AspNetCore.Mvc;
using AIUniversity.Models;
using System.Collections.Generic;
using System.Linq;

namespace AIUniversity.Controllers
{
  public class AcademicsController : Controller
  {
    private readonly AIUniversityContext _db;
    public AcademicsController(AIUniversityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }   
       
  }
}