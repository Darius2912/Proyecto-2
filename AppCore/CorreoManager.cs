using Entities_DTOs;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AppCore
{
    public class CorreoManager
    {
        private readonly IConfiguration _config;

        public CorreoManager(IConfiguration config)
        {
            _config = config;
        }

        public void SendWelcomeEmail(Usuario u)
        {
            try
            {
                var server = _config["SmtpSettings:Server"];
                var port = int.Parse(_config["SmtpSettings:Port"]);
                var user = _config["SmtpSettings:User"];
                var password = _config["SmtpSettings:Password"];
                var enableSsl = bool.Parse(_config["SmtpSettings:EnableSsl"]);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(user);
                mail.To.Add(u.Correo);
                mail.Subject = "Bienvenido al sistema";
                mail.Body = $"Hola {u.Nombre}\n\n¡Bienvenid@! Gracias por registrarte en nuestra App.";

                SmtpClient smtp = new SmtpClient(server, port);
                smtp.Credentials = new NetworkCredential(user, password);
                smtp.EnableSsl = enableSsl;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error enviando correo: " + ex.Message);
            }
        }
    }
}

