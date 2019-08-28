using System;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Email;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Libraries.Login;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteRepository _repositoryCliente;
        private readonly INewsLetterRepository _repositoryNewsLetter;
        private readonly LoginCliente _loginCliente;

        public HomeController(IClienteRepository repositoryCliente, INewsLetterRepository repositoryNewsLetter, LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLetter = repositoryNewsLetter;
            _loginCliente = loginCliente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsLetterEmail newsLetter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositoryNewsLetter.Cadastrar(newsLetter);
                    TempData["MSG_S"] = "E-mail cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opps! Aconteceu um erro ao cadastrar o email!";
                return View();
            }
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao([FromForm]Contato contato)
        {
            try
            {
                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";                    
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opps! Aconteceu um erro ao enviar a mensagem!";
            }
            return View("Contato");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);
            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário e/ou senha inválido!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Painel()
        {
            Cliente cliente =_loginCliente.GetCliente();
            if(cliente != null)
            {
                return new ContentResult() { Content = "Acesso concedido: \n" + cliente.Email + "\n" + cliente.Nome };
            }
            return new ContentResult() { Content = "Acesso negado: "};
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(cliente);
                TempData["MSG_S"] = "Cadastro realizado com sucesso!";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }


        public IActionResult CarrinhoCompras()
        {
            return View();
        }

    }
}