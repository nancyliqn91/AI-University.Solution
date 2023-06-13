using Microsoft.AspNetCore.Mvc;
using AIUniversity.Models;
using System.Collections.Generic;
using System.Linq;

namespace AIUniversity.Controllers
{
  public class AboutController : Controller
  {
    private readonly AIUniversityContext _db;
    public AboutController(AIUniversityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }   
       
  }
}