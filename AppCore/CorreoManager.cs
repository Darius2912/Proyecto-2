using Entities_DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;

namespace AppCore
{
    public class CorreoManager
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        private readonly string _from;
        private readonly string _urlBase;

        public CorreoManager()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _host = config["Smtp:Host"];
            _port = int.Parse(config["Smtp:Port"]);
            _user = config["Smtp:User"];
            _password = config["Smtp:Password"];
            _from = config["Smtp:From"];
            _urlBase = config["AppSettings:UrlBase"];
        }

        public void EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sistema", "digitalcore62@gmail.com"));
            email.To.Add(MailboxAddress.Parse(destinatario));
            email.Subject = asunto;

            email.Body = new TextPart("plain")
            {
                Text = mensaje
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("digitalcore62@gmail.com", "tu_password");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void EnviarResultadoEvaluacion(string correo, string nombreFinca, string estado)
        {
            string asunto = "Resultado de evaluación de su finca";

            string mensaje = estado == "Aprobada"
                ? $"Su finca '{nombreFinca}' ha sido APROBADA exitosamente."
                : $"Su finca '{nombreFinca}' ha sido RECHAZADA. Revise las observaciones en el sistema.";

            EnviarCorreo(correo, asunto, mensaje);
        }

        public void EnviarEmailRecuperacion(string correoDestino, string token)
        {
            var link = $"{_urlBase}/RestablecerContrasena?token={token}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Soporte", _from));
            message.To.Add(new MailboxAddress("", correoDestino));
            message.Subject = "Recuperación de contraseña";
            message.Body = new TextPart("html")
            {
                Text = $@"
                <p>Hola,</p>
                <p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p>
                <a href='{link}'>{link}</a>
                <p>Este enlace expira en 1 hora.</p>"
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect(_host, _port, SecureSocketOptions.StartTls);
            client.Authenticate(_user, _password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}