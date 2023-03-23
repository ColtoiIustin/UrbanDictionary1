using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using UrbanDictionary1.Data;
using UrbanDictionary1.Data.Services;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Controllers
{
    public class ExpressionsController : Controller
    {
        private readonly IExpressionsService _service;

        public ExpressionsController(IExpressionsService service)
        {
            _service = service;
        }

        //Expressions/Index
        public async Task<IActionResult> Index()
        {
            var allExpressions = await _service.GetAllAsync();
            return View(allExpressions);
        }

        
        //POST: Expressions/Index
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            var allExpressions = await _service.GetAllBySearchAsync(searchString);
            return View(allExpressions);
        }


        //Get: Expressions/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Expressions/TakeDataAndCreate
        [HttpPost]
        public async Task<IActionResult> TakeDataAndCreate([Bind("Name,Explication,Example1,Author,Likes,Dislikes")] Expression expression)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",expression);
            }

            await _service.AddAsync(expression);
            return RedirectToAction(nameof(Index));

        }

        //Get Expressions/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }


        //Get: Expressions/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }

        //POST: Expressions/TakeDataAndEdit
        [HttpPost]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("Id,Name,Explication,Example1,CreationDate,Author,Likes,Dislikes")] Expression expression)
        {
            if (!ModelState.IsValid)
            {
                return View(expression);
            }

            await _service.UpdateAsync(id, expression);
            return RedirectToAction(nameof(Index));

        }


        //Get: Expressions/Delete/1
       
        public async Task<IActionResult> Delete(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");
            return View(ExpressionDetails);
        }

        [HttpPost]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ExpressionDetails = await _service.GetByIdAsync(id);
            if (ExpressionDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }



    }
}
