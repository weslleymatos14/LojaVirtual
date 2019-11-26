using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _config;
        private readonly LojaVirtualContext _banco;

        public ProdutoRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _config = configuration;
        }

        public void Atualizar(Produto produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _banco.Produtos.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Produto produto = ObterProdutos(id);
            _banco.Remove(produto);
            _banco.SaveChanges();
        }

        public Produto ObterProdutos(int id)
        {
            return _banco.Produtos.Include(x => x.Imagens).OrderBy(x => x.Nome).Where(x => x.Id == id).FirstOrDefault();
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa)
        {
            return ObterTodosProdutos(pagina, pesquisa, "A");
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao)
        {
            int numeroPagina = pagina ?? 1;
            var bancoProduto = _banco.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(x => x.Nome.Contains(pesquisa.Trim()));
            }

            //Ordena conforme escolhido no site
            if (ordenacao == "A")
            {
                bancoProduto = bancoProduto.OrderBy(x => x.Nome);
            }

            if (ordenacao == "ME")
            {
                bancoProduto = bancoProduto.OrderBy(x => x.Valor);
            }

            if (ordenacao == "MA")
            {
                bancoProduto = bancoProduto.OrderByDescending(x => x.Valor);
            }

            return bancoProduto.Include(x => x.Imagens).ToPagedList<Produto>(numeroPagina, _config.GetValue<int>("Registro:RegistroPorPagina"));
        }
    }
}
