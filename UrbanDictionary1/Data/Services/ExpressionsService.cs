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
            expression.Likes = 0;
            expression.Dislikes= 0;
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
            _context.Expressions.Update(newExpression);
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

        public int GetLikes(int expId)
        {
            var result = _context.Expressions.FirstOrDefault(x => x.Id == expId).Likes;
            return result;
        }
        public int GetDislikes(int expId)
        {
            var result = _context.Expressions.FirstOrDefault(x => x.Id == expId).Dislikes;
            return result;
        }
        public string LikeDislike(int expId, string likeType, string userId)
        {
            string action = null;
            var existingLike = _context.Likes.SingleOrDefault(l => l.ExpressionId == expId && l.UserId == userId);
            var currentExp = _context.Expressions.FirstOrDefault(e => e.Id == expId);

            if (existingLike != null)
            {
                if (likeType == "like")
                {
                    switch(existingLike.Type)
                    {
                        case "like":
                            currentExp.Likes--;
                            _context.Likes.Remove(existingLike);
                            action = "like-like";
                            break;
                        case "dislike":
                            currentExp.Dislikes--;
                            currentExp.Likes++;
                            action = "dislike-like";
                            break;
                    }             
                }
                else
                {
                    switch (existingLike.Type)
                    {
                        case "like":
                            currentExp.Likes--;
                            currentExp.Dislikes++;
                            action = "like-dislike";
                            break;
                        case "dislike":
                            currentExp.Dislikes--;
                            _context.Likes.Remove(existingLike);
                            action = "dislike-dislike";
                            break;
                    }
                }
                // Update the existing like
                existingLike.Type = likeType;
            }
            else
            {
                if (likeType == "like")
                {
                    currentExp.Likes++;
                    action = "none-like";
                }
                else
                {
                    currentExp.Dislikes++;
                    action = "none-dislike";
                }


                    // Create a new like
                    var like = new Likes
                     {
                          ExpressionId = expId,
                          UserId = userId,
                          Type = likeType
                     };
                _context.Likes.Add(like);
            }
            _context.SaveChanges();

            return action;
        }

        public string GetUserActionForPost(int expId , string userId)
        {
            var existingLike = _context.Likes.SingleOrDefault(l => l.ExpressionId == expId && l.UserId == userId);
            if (existingLike != null)
                return existingLike.Type;
            else
                return null;
                        
        }

        public void ChangeAuthor(string oldUsername, string newUsername)
        {
            var expressions = from e in _context.Expressions
                              where e.Author == oldUsername
                              select e;
            foreach (var exp in expressions) {
                exp.Author = newUsername;
            }
            _context.SaveChangesAsync();
        }
    }
}
