using LojaVirtual.Libraries.Lang;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Models
{
    public class NewsLetterEmail
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }
    }
}
