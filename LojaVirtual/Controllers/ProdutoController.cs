using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private Produto GetProduto()
        {
            return new Produto()
            {
                Id = 1,
                Nome = "Xbox one",
                Descricao = "Joguem em 4k",
                Valor = 2000.00M
            };
        }

        public IActionResult Visualizar()
        {
            Produto produto = GetProduto();
            return View(produto);
        }
    }
}