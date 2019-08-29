using System;
using System.Collections.Generic;
using System.Linq;
using LojaVirtual.Data;
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly IConfiguration _config;
        private readonly LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _config = configuration;
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

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            int numeroPagina = 1;
            if (pagina != 0)
                numeroPagina = pagina ?? 1;
            else
                numeroPagina = 1;
            return _banco.Colaboradores.Where(x => x.Tipo != "G").ToPagedList<Colaborador>(numeroPagina, _config.GetValue<int>("RegistroPorPagina"));
        }
    }
}
