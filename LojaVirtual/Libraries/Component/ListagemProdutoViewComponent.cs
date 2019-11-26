using LojaVirtual.Models.ViewModels;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{


    public class ListagemProdutoViewComponent : ViewComponent
    {
        private readonly IProdutoRepository _produtoRepository;

        public ListagemProdutoViewComponent(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Valores padrões
            int? pagina = 1;
            string pesquisa = "";
            string ordenacao = "A";

            //Verifica se existe pagina na QueryString
            if (HttpContext.Request.Query.ContainsKey("pagina")) {
                pagina = int.Parse(HttpContext.Request.Query["pagina"]);
            };

            //Verifica se existe pesquisa na QueryString
            if (HttpContext.Request.Query.ContainsKey("pesquisa"))
            {
                pesquisa = HttpContext.Request.Query["pesquisa"].ToString();
            };

            //Verifica se existe ordenação na QueryString
            if (HttpContext.Request.Query.ContainsKey("ordenacao"))
            {
                ordenacao = HttpContext.Request.Query["ordenacao"].ToString();
            };

            var viewModel = new ListagemProdutoViewModel() { lista = _produtoRepository.ObterTodosProdutos(pagina, pesquisa, ordenacao) };
            return View(viewModel);
        }
    }
}
