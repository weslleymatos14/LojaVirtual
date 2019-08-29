using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Libraries.Lang;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {
        private readonly ColaboradorRepository _colaboradorRepository;

        public ColaboradorController(ColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository= colaboradorRepository;
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
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Cadastrar(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
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
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);

                TempData["MSG_S"] = Mensagem.MSG_S002;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            return View();
        }
    }
}