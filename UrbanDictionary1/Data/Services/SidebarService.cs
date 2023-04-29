using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
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
            var i = 0;
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
                if (i >= 100) return null;
            }
           
        }

        public string NameOfTheDay()
        {
            var result = FindExpOfTheDay();
            if (result != null)     
                return result.Name.ToString(); 
            else return "inexistent";


        }
        
        public string DescriptionOfTheDay()
        {
            var result = FindExpOfTheDay();
            if (result != null)
                return result.Explication.ToString();
            else return "inexistent";
        }

        public string ExampleOfTheDay()
        {
            var result = FindExpOfTheDay();
            if (result != null)
                return result.Example1.ToString();
            else return "inexistent";
        }


        public string AuthorOfTheDay()
        {
            var result = FindExpOfTheDay();
            if (result != null)
                return result.Author.ToString();
            else return "inexistent";
        }

        public string DateOfTheDay()
        {
            var result = FindExpOfTheDay();
            if (result != null)
                return result.CreationDate.ToString();
            else return "inexistent";
        }
        
    }
}
