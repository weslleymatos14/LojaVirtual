using LojaVirtual.Models;
using System.Collections.Generic;


namespace LojaVirtual.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int Id);
        Categoria ObterCategoria(int Id);
        IEnumerable<Categoria> ObterTodasCategorias();
    }
}
