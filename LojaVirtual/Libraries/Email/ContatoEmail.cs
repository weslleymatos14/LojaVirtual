using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            SmtpClient smpt = new SmtpClient("smtp.gmail.com",587);
            smpt.UseDefaultCredentials = false;
            smpt.Credentials = new NetworkCredential("weslleymatos.github@gmail.com","");
            smpt.EnableSsl = true;

            string mensagem = $"<h2>Contato - Loja Virtual</h2><br />" +
                $"<b>Nome: </b> {contato.Nome} <br />" +
                $"<b>E-mail: </b> {contato.Email} <br />" +
                $"<b>Texto: </b> {contato.Texto} <br />" +
                $"<br /> E-mail enviado automaticamente.";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("weslleymatos.github@gmail.com");
            mailMessage.To.Add("weslleymatos.github@gmail.com");
            mailMessage.Subject = "Contato - Loja Virtual - E-mail: " + contato.Email;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;
            smpt.Send(mailMessage);
        }
    }
}
