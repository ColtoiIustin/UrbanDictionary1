using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using System.Drawing.Text;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Data;
using UrbanDictionary1.Data.Services;
using UrbanDictionary1.Migrations;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Controllers
{
    public class ExpressionsController : Controller
    {
        private readonly IExpressionsService _service;
        private readonly ISidebarService _sidebar;
        private readonly IContactService _contact;
        private readonly UserManager<ApplicationUser> _userManager;

        private void SidebarViewBags()
        {
            ViewBag.Name = _sidebar.NameOfTheDay();
            ViewBag.Description = _sidebar.DescriptionOfTheDay();
            ViewBag.Example = _sidebar.ExampleOfTheDay();
            ViewBag.Author = _sidebar.AuthorOfTheDay();
            ViewBag.Date = _sidebar.DateOfTheDay();
        }

        
        public ExpressionsController(IExpressionsService service,
            ISidebarService sidebar,
            IContactService contact,
            UserManager<ApplicationUser> userManager
            )
        {
            _service = service;
            _sidebar = sidebar;
            _userManager = userManager;
            _contact= contact;
            
        }

        
        //Expressions/Index
        public async Task<IActionResult> Index()
        {

            SidebarViewBags();
            var allExpressions = await _service.GetAllAsync();
            return View(allExpressions);
        }

                   
        //POST: Expressions/Index
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            SidebarViewBags();

            var allExpressions = await _service.GetAllBySearchAsync(searchString);
            return View(allExpressions);
        }


        //Get: Expressions/Create
        [Authorize]
        public IActionResult Create()
        {
            SidebarViewBags();
            return View();
        }

        //POST: Expressions/TakeDataAndCreate
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TakeDataAndCreate([Bind("Name,Explication,Example1")] Expression expression)
        {
            SidebarViewBags();

            if (!ModelState.IsValid)
            {
                return View("Create",expression);
            }
            expression.Author = _userManager.GetUserName(User);
            await _service.AddAsync(expression);
            return RedirectToAction(nameof(Index));

        }

        //Get Expressions/Details/1
        [Authorize(Roles ="Admx")]
        public async Task<IActionResult> Details(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }

        [Authorize(Roles = "Admx")]
        //Get: Expressions/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }

        //POST: Expressions/TakeDataAndEdit
        [HttpPost]
        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("Id,Name,Explication,Example1")] Expression expression)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit" , expression);
            }

            await _service.UpdateAsync(id, expression);
            return RedirectToAction(nameof(Index));

        }


        //Get: Expressions/1/1
        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> Delete(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }

        [HttpPost]
        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

        //Expressions/UnverifiedList
        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> UnverifiedList()
        {
            SidebarViewBags();
            var allExpressions = await _service.GetAllUnverifiedAsync();
            return View(allExpressions);
        }

        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> ApproveExpression(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");

            await _service.ExpressionApprovedAsync(id);
            return RedirectToAction(nameof(UnverifiedList));

        }

        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> RejectExpression(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");

            await _service.ExpressionRejectedAsync(id);
            return RedirectToAction(nameof(UnverifiedList));

        }

        public async Task <IActionResult> AuthorExpressions(int id)
        {
            SidebarViewBags();
            var ExpressionDetails = _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            ViewBag.Autor = ExpressionDetails.Result.Author;
            var allExpressions = await _service.GetAllAuthorExpressions(ExpressionDetails.Result);
            return View(allExpressions);
        }
        
        public async Task<IActionResult> AuthorExpressionsSidebar(string id)
        {
            SidebarViewBags();
            var ExpressionDetails = _service.GetByAuthorAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            ViewBag.Autor = ExpressionDetails.Result.Author;
            var allExpressions = await _service.GetAllAuthorExpressions(ExpressionDetails.Result);
            return View("AuthorExpressions" , allExpressions);
        }


        
        public JsonResult LikeDislike(int expId, string likeType)
        {

                if (!User.Identity.IsAuthenticated) 
                    return Json(new { userAuthenticated = "false" });

                string userId = _userManager.GetUserId(User);
                string action = _service.LikeDislike(expId, likeType, userId);

                // Return the updated number of likes and dislikes
                var totalLikes = _service.GetLikes(expId);
                var totalDislikes = _service.GetDislikes(expId);
                var data = new { likes = totalLikes, dislikes = totalDislikes, expId , action };
                return Json(data);
            
        }

        public JsonResult GetUserActionForPost(int expId)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { userAuthenticated = "false" });

            string userId = _userManager.GetUserId(User);
            string result = _service.GetUserActionForPost(expId, userId);

            var data = new { action = result };
            return Json(data);
        }

        public IActionResult Contact()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact([Bind("Email, Content")] Message message)
        {
            SidebarViewBags();

            if (!ModelState.IsValid)
            {
                return View("Contact", message);
            }
            await _contact.AddAsync(message);
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> ContactMessages()
        {
            SidebarViewBags();
            var allMessages = await _contact.GetAllAsync();
            return View(allMessages);
        }

        
        [Authorize(Roles = "Admx")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var MessageDetails = await _contact.GetByIdAsync(id);
            if (MessageDetails == null) return View("NotFound");

            await _contact.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }

        

    }
}
