using System.Collections.Generic;
using System.Linq;
using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _config;
        private readonly LojaVirtualContext _banco;

        public ClienteRepository(LojaVirtualContext banco, IConfiguration config)
        {
            _banco = banco;
            _config = config;
        }

        public void Atualizar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            _banco.Clientes.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Cliente cliente = ObterCliente(Id);
            _banco.Remove(cliente);
            _banco.SaveChanges();
        }

        public Cliente Login(string Email, string Senha)
        {
            return _banco.Clientes.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
        }

        public Cliente ObterCliente(int Id)
        {
            return _banco.Clientes.Find(Id);
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina)
        {
            int numeroPagina = pagina ?? 1;

            return _banco.Clientes.ToPagedList<Cliente>(numeroPagina, _config.GetValue<int>("Registro:RegistroPorPagina"));
        }
    }
}
