using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UrbanDictionary1.Models
{
    public class Expression
    {
        [Key]
        public int Id { get; set; }  


        [DisplayName("Expression")]
        [Required(ErrorMessage= "Expression is required")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Explication is required")]
        public string? Explication { get; set; }


        [Required(ErrorMessage = "Example is required")]
        [StringLength(50, MinimumLength =  1, ErrorMessage = "Example1 must be between 1 and 50 characters")]
        public string? Example1 { get; set; }

        public string? CreationDate { get; set; }


        [Required(ErrorMessage = "Author is required")]
        [DisplayName("Author")]
        public string? Author { get; set; }


        [DisplayName("Number Of Likes")]
        public int Likes { get; set; }


        [DisplayName("Number Of Dislikes")]
        public int Dislikes { get; set; }


    }
}
