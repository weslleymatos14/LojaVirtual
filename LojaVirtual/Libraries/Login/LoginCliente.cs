using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private readonly Session.Session _session;
        public LoginCliente(Session.Session session)
        {
            _session = session;
        }

        public void Login(Cliente cliente)
        {
            string clienteJson = JsonConvert.SerializeObject(cliente);
            _session.Cadastrar(Key, clienteJson);
        }

        public Cliente GetCliente()
        {
            string clienteJson = _session.Consultar(Key);
            return JsonConvert.DeserializeObject<Cliente>(clienteJson);
        }

        public void Logout()
        {
            _session.RemoverTodos();
        }

    }
}
