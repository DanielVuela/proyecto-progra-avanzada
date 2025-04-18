using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace MacroBalance.Service
{
    public static class CorreoService
    {
        public static void Enviar(string destinatario, string asunto, string mensaje)
        {
            var remitente = "macrobalancecr@gmail.com"; //---
            var contrasena = "dlqe eiip shxb tefj\r\n";     //---

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(remitente, contrasena)
            };

            var mail = new MailMessage(remitente, destinatario)
            {
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = false
            };

            smtp.Send(mail);
        }
    }
}