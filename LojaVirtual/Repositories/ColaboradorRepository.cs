using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Data;
using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Interfaces
{
    public class ColaboradorRepository : IColaboradorRepository
    {

        private readonly LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Colaboradores.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Colaborador colaborador = ObterColaborador(id);
            _banco.Remove(colaborador);
            _banco.SaveChanges();
        }

        public Colaborador Login(string email, string senha)
        {
            Colaborador colaborador = _banco.Colaboradores.Where(m => m.Email == email && m.Senha == senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaborador(int id)
        {
            return _banco.Colaboradores.Find(id);
        }

        public IEnumerable<Colaborador> ObterColaboradores()
        {
            return _banco.Colaboradores.ToList();
        }
    }
}
