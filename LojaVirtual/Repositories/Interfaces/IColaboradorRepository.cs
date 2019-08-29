using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string email, string senha);
        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void Excluir(int id);

        Colaborador ObterColaborador(int id);
        IPagedList<Colaborador> ObterTodosColaboradores(int? pagina);
     }
}
