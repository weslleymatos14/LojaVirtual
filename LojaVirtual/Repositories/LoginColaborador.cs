using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private readonly Session.Session _session;
        public LoginColaborador(Session.Session session)
        {
            _session = session;
        }

        public void Login(Colaborador colaborador)
        {
            string colaboradorJson = JsonConvert.SerializeObject(colaborador);
            _session.Cadastrar(Key, colaboradorJson);
        }

        public Colaborador GetColaborador()
        {
            if (_session.Existe(Key))
            {
                string colaboradorJson = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJson);
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
