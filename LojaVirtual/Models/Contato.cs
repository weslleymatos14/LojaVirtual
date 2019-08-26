using System.ComponentModel.DataAnnotations;
using LojaVirtual.Libraries.Lang;

namespace LojaVirtual.Models
{
    public class Contato
    {
        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E003")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(500, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E003")]
        public string Texto { get; set; }
    }
}
