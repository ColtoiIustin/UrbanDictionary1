using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data.Services
{
    public interface IExpressionsService
    {
        Task<IEnumerable<Expression>> GetAllAsync();
        Task<IEnumerable<Expression>> GetAllBySearchAsync(string searchString);
        Task <Expression> GetByIdAsync(int id);
        Task AddAsync(Expression expression);
        Task<Expression> UpdateAsync(int id, Expression newExpression);
        Task DeleteAsync(int id);
        Task<IEnumerable<Expression>> GetAllUnverifiedAsync();
        Task ExpressionApprovedAsync(int id);
        Task ExpressionRejectedAsync(int id);



    }
}
