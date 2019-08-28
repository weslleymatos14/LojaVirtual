using Microsoft.AspNetCore.Http;


namespace LojaVirtual.Libraries.Session
{
    public class Session
    {
        private readonly IHttpContextAccessor _contexto;
        public Session(IHttpContextAccessor contexto)
        {
            _contexto = contexto;
        }

        public void Cadastrar(string key, string valor)
        {
            _contexto.HttpContext.Session.SetString(key, valor);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                _contexto.HttpContext.Session.Remove(key);
            }
            _contexto.HttpContext.Session.SetString(key, valor);
        }

        public void Remover(string key)
        {
            if (Existe(key))
            {
                _contexto.HttpContext.Session.Remove(key);
            }
        }

        public string Consultar(string key)
        {
            return _contexto.HttpContext.Session.GetString(key);
        }

        public bool Existe(string key)
        {
            if (_contexto.HttpContext.Session.GetString(key) == null)
            {
                return false;
            }

            return true;
        }

        public void RemoverTodos()
        {
            _contexto.HttpContext.Session.Clear();
        }
    }
}
