using LojaVirtual.Models;
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
            if (_session.Existe(Key))
            {
                string clienteJson = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJson);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _session.RemoverTodos();
        }

    }
}
