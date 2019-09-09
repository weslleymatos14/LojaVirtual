﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filter;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index(int? pagina)
        {
            IPagedList<Cliente> clientes = _clienteRepository.ObterTodosClientes(pagina);
            return View(clientes);
        }

        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            Cliente cliente = _clienteRepository.ObterCliente(id);

            cliente.Situacao = (cliente.Situacao == SituacaoConstant.Ativo) ?  cliente.Situacao = SituacaoConstant.Desativado : cliente.Situacao = SituacaoConstant.Ativo;
            _clienteRepository.Atualizar(cliente);

            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}