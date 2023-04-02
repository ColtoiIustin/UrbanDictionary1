using UrbanDictionary1.Models;
using Microsoft.AspNetCore.Mvc;

namespace UrbanDictionary1.Data.Services
{
    public interface IContactService
    {
        Task AddAsync(Message message);
        Task<IEnumerable<Message>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<Message> GetByIdAsync(int id);
    }
}
