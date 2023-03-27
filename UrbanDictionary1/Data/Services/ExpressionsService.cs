using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Models;
using Microsoft.AspNetCore.Authorization;


namespace UrbanDictionary1.Data.Services
{
    public class ExpressionsService : IExpressionsService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExpressionsService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }
        public async Task AddAsync(Expression expression)
        {
            expression.CreationDate = DateTime.Today.ToString("dd.MM.yyyy");
            expression.IsVerified = false;
            await _context.Expressions.AddAsync(expression);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Expressions.FirstOrDefaultAsync(x => x.Id == id);
            _context.Expressions.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expression>> GetAllBySearchAsync(string searchString) 
        {
            var expressions = from e in _context.Expressions
                              where e.IsVerified == true
                              select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                expressions = expressions.Where(s => s.Name!.Contains(searchString.Trim()));
            }
            var result = await expressions.ToListAsync();
            return (result);
        }

        public async Task<IEnumerable<Expression>> GetAllAsync()
        {
            var expressions = from e in _context.Expressions
                              where e.IsVerified == true
                              select e;
            var result = await expressions.ToListAsync();
            return (result);
        }

        public async Task<Expression> GetByIdAsync(int id)
        {
            var result =await _context.Expressions.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Expression> UpdateAsync(int id, Expression newExpression)
        {
            _context.Update(newExpression);
            await _context.SaveChangesAsync();
            return(newExpression);
        }


        public async Task<IEnumerable<Expression>> GetAllUnverifiedAsync()
        {
            var expressions = from e in _context.Expressions
                              where e.IsVerified == false
                              select e;

            var result = await expressions.ToListAsync();
            return (result);
        }

        public async Task ExpressionApprovedAsync(int id)
        {
            var result = await _context.Expressions.FirstOrDefaultAsync(x => x.Id == id);
            result.IsVerified = true;
            await _context.SaveChangesAsync();
        }

        public async Task ExpressionRejectedAsync(int id)
        {
            
            var result = await _context.Expressions.FirstOrDefaultAsync(x => x.Id == id);
            _context.Expressions.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expression>> GetAllAuthorExpressions(Expression ExpressionDetails)
        {

            var expressions = from e in _context.Expressions
                              where (e.IsVerified == true && e.Author == ExpressionDetails.Author)
                              select e;

            var result = await expressions.ToListAsync();
            return (result);
        }

        public async Task<Expression> GetByAuthorAsync(string author)
        {
            var result = await _context.Expressions.FirstOrDefaultAsync(x => x.Author == author);
            return result;
        }
    }
}
