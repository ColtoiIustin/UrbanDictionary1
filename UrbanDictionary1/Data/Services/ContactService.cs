using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        public ContactService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Message message)
        {
            await _context.Message.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Message.FirstOrDefaultAsync(x => x.Id == id);
            _context.Message.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            var result = await _context.Message.ToListAsync();
            return (result);
        }
    }
}
