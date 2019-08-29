using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Colaborador
    {
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E003")]
        public string Nome { get; set; }

        [Display(Name ="E-mail")]
        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(6, ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E002")]
        public string Senha { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmacaoSenha { get; set; }

        [Required(ErrorMessageResourceType = typeof(Pt_BR), ErrorMessageResourceName = "MSG_E001")]
        public string Tipo { get; set; }

    }
}
