using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data.Services
{
    public class ExpressionsService : IExpressionsService
    {
        private readonly AppDbContext _context;
        public ExpressionsService(AppDbContext context)
        {
            _context = context;
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

    }
}
