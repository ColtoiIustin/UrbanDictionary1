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
            expression.CreationDate = DateTime.Today.ToString("dd/MM/yyyy");
            await _context.Expressions.AddAsync(expression);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Expressions.FirstOrDefaultAsync(x => x.Id == id);
            _context.Expressions.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expression>> GetAllAsync()
        {
            var result=await _context.Expressions.ToListAsync();
            return(result);
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


    }
}
