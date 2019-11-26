using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Models.ViewModels
{
    public class ListagemProdutoViewModel
    {
        public IPagedList<Produto> lista { get; set; }
        public List<SelectListItem> ordenacao
        {
            get
            {
                return new List<SelectListItem>()
            {
                new SelectListItem("Ordem Alfabetica", "A"),
                new SelectListItem("Menor valor", "ME"),
                new SelectListItem("Maior valor", "MA"),
            };
            }
            private set { }
        }
    }
}
