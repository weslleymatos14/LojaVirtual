using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private readonly SmtpClient _smtp;
        private readonly IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {
            string mensagem = $"<h2>Contato - Loja Virtual</h2><br />" +
                $"<b>Nome: </b> {contato.Nome} <br />" +
                $"<b>E-mail: </b> {contato.Email} <br />" +
                $"<b>Texto: </b> {contato.Texto} <br />" +
                $"<br /> E-mail enviado automaticamente.";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mailMessage.To.Add("weslleymatos.github@gmail.com");
            mailMessage.Subject = "Contato - Loja Virtual - E-mail: " + contato.Email;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;

            _smtp.Send(mailMessage);
        }

        public void EnviarSenhaColaborador(Colaborador colaborador)
        {
            string mensagem = $"<h2>Colaborador - Loja Virtual</h2><br />" +
                            $"Suas informaçoes são:  " +
                            $"<b>E-mail: </b> {colaborador.Email} <br />" +
                            $"<b>Nome: </b> {colaborador.Senha} <br />" +
                            $"<br /> E-mail enviado automaticamente.";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mailMessage.To.Add(colaborador.Email);
            mailMessage.Subject = "Colaborador - Loja Virtual - Informações de cadastro: " + colaborador.Nome;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;

            _smtp.Send(mailMessage);
        }
    }
}
