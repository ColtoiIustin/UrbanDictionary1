using System.ComponentModel.DataAnnotations;
using UrbanDictionary1.Data;

namespace UrbanDictionary1.Models
{
    public class Likes
    {
        [Key]
        public int Id { get; set; }
        public int ExpressionId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
    }
}
