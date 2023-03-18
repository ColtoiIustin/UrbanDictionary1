using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data.Services
{
    public interface IExpressionsService
    {
        Task<IEnumerable<Expression>> GetAllAsync();
        Task <Expression> GetByIdAsync(int id);
        Task AddAsync(Expression expression);
        Task<Expression> UpdateAsync(int id, Expression newExpression);
        void Delete(int id);

    }
}
