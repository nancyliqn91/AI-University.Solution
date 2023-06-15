// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using AIUniversity.Models;
// using System.Threading.Tasks;
// using AIUniversity.ViewModels;

// namespace AIUniversity.Controllers
// {
//   public class AccountController : Controller
//   {
//     private readonly AIUniversityContext _db;
//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly SignInManager<ApplicationUser> _signInManager;

//     public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AIUniversityContext db)
//     {
//       _userManager = userManager;
//       _signInManager = signInManager;
//       _db = db;
//     }

//     public ActionResult Index()
//     {
//       return View();
//     }

// [Authorize(Roles = "Administrator")]
// public class AdministrationController : Controller
// {
//     public IActionResult Index() =>
//         Content("Administrator");
// }