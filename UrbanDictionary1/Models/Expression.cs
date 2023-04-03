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
        [Required(ErrorMessage= "Expresia este necesara")]
        [DataType(DataType.MultilineText)]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Explicatia este necesara")]
        [DataType(DataType.MultilineText)]
        public string? Explication { get; set; }


        [Required(ErrorMessage = "Exemplul este necesar")]
        [StringLength(150, MinimumLength =  1, ErrorMessage = "Exemplul trebuie sa contina intre 1 si 100 de caractere")]
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
