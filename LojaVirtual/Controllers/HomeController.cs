using System;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Email;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteRepository _repositoryCliente;
        private readonly INewsLetterRepository _repositoryNewsLetter;

        public HomeController(IClienteRepository repositoryCliente, INewsLetterRepository repositoryNewsLetter)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLetter = repositoryNewsLetter;
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
            if (cliente.Email == "weslley@gmail.com" && cliente.Senha == "02021999")
            {
                HttpContext.Session.Set("ID", new byte[] { 52 });
                HttpContext.Session.SetString("Email", cliente.Email);
                HttpContext.Session.SetInt32("Idade", 25);

                return new ContentResult() { Content = "Logado" };
            }
            else
            {
                return new ContentResult() { Content = "Não logado" };
            }
        }

        [HttpGet]
        public IActionResult Painel()
        {
            byte[] UsuarioID; 
            if (HttpContext.Session.TryGetValue("ID", out UsuarioID))
            {
                return new ContentResult() { Content = "Acesso concedido: " + UsuarioID[0] };
            }
            return new ContentResult() { Content = "Acesso negado: " + UsuarioID[0] };
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