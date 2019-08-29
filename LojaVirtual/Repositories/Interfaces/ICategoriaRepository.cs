using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int Id);
        Categoria ObterCategoria(int Id);
        IPagedList<Categoria> ObterTodasCategorias(int? pagina);
    }
}
