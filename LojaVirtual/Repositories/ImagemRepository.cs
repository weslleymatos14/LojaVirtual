using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        private readonly LojaVirtualContext _banco;

        public ImagemRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void CadastrarImagens(List<Imagem> listaImagens, int produtoId)
        {
            if (listaImagens != null && listaImagens.Count > 0)
            {
                foreach (var Imagem in listaImagens)
                {
                    Cadastrar(Imagem);
                }
                _banco.SaveChanges();
            }          
        }

        public void Cadastrar(Imagem imagem)
        {
            _banco.Imagens.Add(imagem);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Imagem imagem = _banco.Imagens.Find(id);
            _banco.Remove(imagem);
            _banco.SaveChanges();
        }

        public void ExcluirImagensDoProduto(int produtoId)
        {
            List<Imagem> imagens = _banco.Imagens.Where(x => x.ProdutoId == produtoId).ToList();

            foreach(Imagem imagem in imagens)
            {
                _banco.Remove(imagem);
            }

            _banco.SaveChanges();
        }
    }
}
