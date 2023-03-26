using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Data;
using UrbanDictionary1.Data.Services;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Controllers
{
    public class ExpressionsController : Controller
    {
        private readonly IExpressionsService _service;
        private readonly ISidebarService _sidebar;
        private readonly UserManager<ApplicationUser> _userManager;


        public ExpressionsController(IExpressionsService service, ISidebarService sidebar, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _sidebar = sidebar;
            _userManager = userManager;
            
        }

        
        //Expressions/Index
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = _sidebar.NameOfTheDay();
            ViewBag.Description = _sidebar.DescriptionOfTheDay();
            ViewBag.Example = _sidebar.ExampleOfTheDay();
            ViewBag.Author = _sidebar.AuthorOfTheDay();
            ViewBag.Date = _sidebar.DateOfTheDay();
       
            var allExpressions = await _service.GetAllAsync();
            return View(allExpressions);
        }

                   
        //POST: Expressions/Index
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewBag.Name = _sidebar.NameOfTheDay();
            ViewBag.Description = _sidebar.DescriptionOfTheDay();
            ViewBag.Example = _sidebar.ExampleOfTheDay();
            ViewBag.Author = _sidebar.AuthorOfTheDay();
            ViewBag.Date = _sidebar.DateOfTheDay();

            var allExpressions = await _service.GetAllBySearchAsync(searchString);
            return View(allExpressions);
        }


        //Get: Expressions/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Name = _sidebar.NameOfTheDay();
            ViewBag.Description = _sidebar.DescriptionOfTheDay();
            ViewBag.Example = _sidebar.ExampleOfTheDay();
            ViewBag.Author = _sidebar.AuthorOfTheDay();
            ViewBag.Date = _sidebar.DateOfTheDay();
            return View();
        }

        //POST: Expressions/TakeDataAndCreate
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> TakeDataAndCreate([Bind("Name,Explication,Example1,Likes,Dislikes")] Expression expression)
        {
            ViewBag.Name = _sidebar.NameOfTheDay();
            ViewBag.Description = _sidebar.DescriptionOfTheDay();
            ViewBag.Example = _sidebar.ExampleOfTheDay();
            ViewBag.Author = _sidebar.AuthorOfTheDay();
            ViewBag.Date = _sidebar.DateOfTheDay();

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
        public async Task<IActionResult> EditConfirmed(int id, [Bind("Id,Name,Explication,Example1,CreationDate,Author,Likes,Dislikes")] Expression expression)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit" , expression);
            }

            await _service.UpdateAsync(id, expression);
            return RedirectToAction(nameof(Index));

        }


        //Get: Expressions/Delete/1
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


    }
}
