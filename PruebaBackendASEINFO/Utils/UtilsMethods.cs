using PruebaBackendASEINFO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PruebaBackendASEINFO.Utils
{
    public static class UtilsMethods
    {
        public static string GenerarSalt()
        {
            int longitud = 10;
            Guid miGuid = Guid.NewGuid();
            string token = miGuid.ToString().Replace("-", string.Empty).Substring(0, longitud);
            return token;
        }

        public static void EnviarCorreo(Usuario usuario)
        {
            string emailOrigen = "pruebabackendaseinfonv@gmail.com";
            string emailDestino = usuario.Correo;
            string contrasenia = "pruebaASEINFO!23";
            string mensaje = "<h1>Bienvenido a nuestra tienda</h1>" +
                "<p>Te has registrado con éxito</p>";

            MailMessage correo = new MailMessage(emailOrigen, emailDestino, "Registrado", mensaje);
            correo.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(emailOrigen, contrasenia);
            client.Send(correo);
            client.Dispose();
        }

        public static void EnviarCorreo(Usuario usuario, string Token)
        {
            string emailOrigen = "pruebabackendaseinfonv@gmail.com";
            string emailDestino = usuario.Correo;
            string contrasenia = "pruebaASEINFO!23";
            string url = "http://localhost:5000/Recuperacion/Recuperacion/?token=" + Token;
            string mensaje = "<h1>Recuperar contraseña</h1>" +
                "<p>Has solicitado recuperar tu contraseña</p>" +
                "<a href='" + url + "'>Clic para recuperar</a>";

            MailMessage correo = new MailMessage(emailOrigen, emailDestino, "Registrado", mensaje);
            correo.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential(emailOrigen, contrasenia);
            client.Send(correo);
            client.Dispose();
        }
    }
}
