using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Data;

namespace UrbanDictionary1.Models
{
    public class Expression
    {
        [Key]
        public int Id { get; set; }  


        [DisplayName("Expression")]
        [Required(ErrorMessage= "Expression is required")]
        [DataType(DataType.MultilineText)]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Explication is required")]
        [DataType(DataType.MultilineText)]
        public string? Explication { get; set; }


        [Required(ErrorMessage = "Example is required")]
        [StringLength(50, MinimumLength =  1, ErrorMessage = "Example1 must be between 1 and 50 characters")]
        [DataType(DataType.MultilineText)]
        public string? Example1 { get; set; }

        public string? CreationDate { get; set; }

        public string? Author { get; set; }
        
        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public bool IsVerified { get; set; }

        public LikeType Type { get; set; }


    }
}
