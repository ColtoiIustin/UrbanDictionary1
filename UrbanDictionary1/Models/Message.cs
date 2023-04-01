using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UrbanDictionary1.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }


        [DisplayName("Email")]
        [EmailAddress (ErrorMessage = "Emailul nu este valid")]
        [Required(ErrorMessage = "Adauga emailul")]
        public string Email { get; set; }


        [DisplayName("Mesaj")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Adauga mesajul")]
        public string Content { get; set; }
    }
}
