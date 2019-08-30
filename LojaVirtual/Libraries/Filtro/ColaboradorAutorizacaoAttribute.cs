﻿using LojaVirtual.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace LojaVirtual.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAutorizado = "Colaborador")
        {
            _tipoColaboradorAutorizado = tipoColaboradorAutorizado;
        }

        LoginColaborador _loginColaborador;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Models.Colaborador colaborador = _loginColaborador.GetColaborador();
            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (colaborador.Tipo != "Gerente" && _tipoColaboradorAutorizado == "Gerente")
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
