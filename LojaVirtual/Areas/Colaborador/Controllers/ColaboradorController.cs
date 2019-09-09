using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Generator;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Filter;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao("Gerente")]
    public class ColaboradorController : Controller
    {
        private readonly ColaboradorRepository _colaboradorRepository;
        private readonly GerenciarEmail _gerenciarEmail;
        public ColaboradorController(ColaboradorRepository colaboradorRepository, GerenciarEmail gerenciarEmail)
        {
            _colaboradorRepository= colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }

        public IActionResult Index(int pagina)
        {
            var colaboradores = _colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);
                _colaboradorRepository.Cadastrar(colaborador);
                _gerenciarEmail.EnviarSenhaColaborador(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GerarSenha(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);
            colaborador.Senha = KeyGenerator.GetUniqueKey(8);

            _colaboradorRepository.AtualizarSenha(colaborador);
            _gerenciarEmail.EnviarSenhaColaborador(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_S004;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm]Models.Colaborador colaborador, int id)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                
                TempData["MSG_S"] = Mensagem.MSG_S002;

                return RedirectToAction(nameof(Index));
            }

            TempData["MSG_E"] = "Ops! Aconteceu um erro. Tente novamente mais tarde.";
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_S003;

            return RedirectToAction(nameof(Index));
        }
    }
}