using LojaVirtual.Libraries.Arquivo;
using LojaVirtual.Libraries.Filter;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ProdutoController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IImagemRepository _imagemRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IImagemRepository imagemRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _imagemRepository = imagemRepository;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            //Obtem os dados para demosntrar
            var produtos = _produtoRepository.ObterTodosProdutos(pagina, pesquisa);
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            //Obtem os dados para demosntrar
            ViewBag.Categorias =  _categoriaRepository.ObterTodasCategorias().Select(a=> new SelectListItem(a.Nome, a.Id.ToString()));
            return View();      
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                //Cadastra produto no banco
                _produtoRepository.Cadastrar(produto);

                //Pega campo com o caminho da Imagem
                List<Imagem> ListaImagensDef = GerenciadorArquivo.MoverImagemProduto(new List<string>(Request.Form["Imagem"]), produto.Id);

                //Insere as imagens no banco
                _imagemRepository.CadastrarImagens(ListaImagensDef, produto.Id);
                
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //Obtem os dados para demosntrar
                produto.Imagens = new List<string>(Request.Form["Imagem"]).Where(x => x.Trim().Length > 0).Select(x => new Imagem() { Caminho = x }).ToList();
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                return View(produto);
            }                      
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            //Obtem os dados para demosntrar
            Produto produto = _produtoRepository.ObterProdutos(id);
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(produto);
        }

        [HttpPost]
        public IActionResult Atualizar(Produto produto, int id)
        {
            if (ModelState.IsValid)
            {
                //Cadastra produto no banco
                _produtoRepository.Atualizar(produto);

                //Pega campo com o caminho da Imagem
                List<Imagem> ListaImagensDef = GerenciadorArquivo.MoverImagemProduto(new List<string>(Request.Form["Imagem"]), produto.Id);

                //Deleta as imagens no banco
                _imagemRepository.ExcluirImagensDoProduto(produto.Id);

                //Insere as imagens no banco
                _imagemRepository.CadastrarImagens(ListaImagensDef, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                produto.Imagens = new List<string>(Request.Form["Imagem"]).Where(x => x.Trim().Length > 0).Select(x => new Imagem() { Caminho = x }).ToList();
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                return View(produto);
            }          
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            //Deletar as imagens do produto
            Produto produto = _produtoRepository.ObterProdutos(id);
            GerenciadorArquivo.ExcluirImagensProduto(produto.Imagens.ToList());
            _imagemRepository.ExcluirImagensDoProduto(id);

            //Deleta produto
            _produtoRepository.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_S003;
            return RedirectToAction(nameof(Index));
        }
    }
}