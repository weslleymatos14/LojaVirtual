using System;
using System.Collections.Generic;
using System.Linq;
using LojaVirtual.Data;
using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;
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
            _banco.Entry(colaborador).Property(x => x.Senha).IsModified = false;
            _banco.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.Entry(colaborador).Property(x => x.Nome).IsModified = false;
            _banco.Entry(colaborador).Property(x => x.Email).IsModified = false;
            _banco.Entry(colaborador).Property(x => x.Tipo).IsModified = false;
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
            return _banco.Colaboradores.Where(x => x.Tipo != "Gerente").ToPagedList<Colaborador>(numeroPagina, _config.GetValue<int>("Registro:RegistroPorPagina"));
        }

        public List<Colaborador> ObterColaborarPorEmail(string email)
        {
            return _banco.Colaboradores.Where(x => x.Email == email).AsNoTracking().ToList();
        }
    }
}
