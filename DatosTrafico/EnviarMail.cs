using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DatosTrafico
{
    class EnviarMail
    {
        public static String Logarchivo;
        public static void Mail(string Mensaje)
        {
            Logarchivo = "MAIL.txt";

            DateTime TimeMail = DateTime.Now;
            string TimeMail1 = TimeMail.ToString("yyyy-MM-dd HH:mm");
            var fromAddress = new MailAddress("diagindra@gmail.com", "Rutina Hermes");
            var toAddress = new MailAddress("santiagolopera13@gmail.com", "Santiago Agudelo");
            const string fromPassword = "Medellin2017a!";
            const string subject = "Alerta | Hermes";
            string body = Mensaje + TimeMail1;
            MailAddress copyD = new MailAddress("davidmartinez.189@gmail.com", "David Martinez");
            MailAddress copyE = new MailAddress("elmer.aua@gmail.com", "Elmer Usuga");
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
                try
                {
                    message.CC.Add(copyD);
                    message.CC.Add(copyE);
                    smtp.Send(message);
                    EscribeLog.escribe(" Se envió mail correctamente con el mensaje -> " + Mensaje + TimeMail1, Logarchivo);

                }
                catch (Exception ex)
                {
                    EscribeLog.escribe(" Lo siento en algún lado me perdí este debe ser el error:" + ex, Logarchivo);

                }
        }
    }
}
