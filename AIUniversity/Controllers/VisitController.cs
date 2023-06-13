using Microsoft.AspNetCore.Mvc;
using AIUniversity.Models;
using System.Collections.Generic;
using System.Linq;

namespace AIUniversity.Controllers
{
  public class VisitController : Controller
  {

    private readonly AIUniversityContext _db;
    public VisitController(AIUniversityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }   
       
  }
}