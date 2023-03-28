using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using UrbanDictionary1.Models;

namespace UrbanDictionary1.Data.Services
{
    public class SidebarService : ISidebarService
    {

        private readonly AppDbContext _context;
        public SidebarService(AppDbContext context)
        {
            _context = context;
            
        }

        public Expression FindExpOfTheDay()
        {   
            var i = 1;
            while(true) 
            {   
                var TheLastDate = DateTime.Today.AddDays(-i).ToString("dd.MM.yyyy");
                var expressions = from e in _context.Expressions
                                  where (e.IsVerified == true && e.CreationDate == TheLastDate)
                                  select e;
                var res = expressions.ToList();

                //result => expresia cu nr max de Likes
                var result = res.MaxBy(x => x.Likes);
                if (result != null) 
                    return result;
               
                i++;
            }
           
        }
        
        public string NameOfTheDay()
        {
            var result = FindExpOfTheDay().Name.ToString();
            return result;
        }
        public string DescriptionOfTheDay()
        {
            var result = FindExpOfTheDay().Explication.ToString();
            return result;
        }

        public string ExampleOfTheDay()
        {
            var result = FindExpOfTheDay().Example1.ToString();
            return result;
        }


        public string AuthorOfTheDay()
        {
            var result = FindExpOfTheDay().Author.ToString();
            return result;
        }

        public string DateOfTheDay()
        {
            var result = FindExpOfTheDay().CreationDate.ToString();
            return result;
        }
        
    }
}
