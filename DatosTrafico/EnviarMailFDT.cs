using System;
using System.Net;
using System.Net.Mail;

namespace DatosTrafico
{
    class EnviarMailFDT
    {
        public static String Logarchivo;
        public static void Mail(string[] Mensaje)
        {
            Logarchivo = "MAIL.txt";
            DateTime TimeMail = DateTime.Now;
            DateTime Timeahora = TimeMail.AddDays(-1);
            string TimeMail1 = Timeahora.ToString("yyyy-MM-dd");
            var fromAddress = new MailAddress("diagindra@gmail.com", "Rutina Hermes");
            var toAddress = new MailAddress("santiagolopera13@gmail.com", "Santiago Agudelo");
            const string fromPassword = "Medellin2017a!";
            string subject = "Hermes | FDT | Menos de 90% de datos en el día " + TimeMail1;
            string body = "<html xmlns= \"http://www.w3.org/1999/xhtml \" lang = \"es\" xml: lang = \"es\" >"+
                           "<head><meta http - equiv = \"Content-Type\" content = \"text/html; charset=UTF-8 \" /></ head ><body> "+
                          "<h1> Listado de carriles</h1><table border=\"1\" bordercolor=\"666633\" cellpadding=\"2\" cellspacing=\"0\"><tr>" +
                          "<th scope= \"col\"> Carril </th>" +
                          " <th scope= \"col\">Número de registros </th> " +
                          "</tr> "+string.Join("", Mensaje)+ "</table></ body ></ html >";
           // MailAddress copyD = new MailAddress("davidmartinez.189@gmail.com", "David Martinez");
            //MailAddress copyE = new MailAddress("elmer.aua@gmail.com", "Elmer Usuga");
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
                IsBodyHtml = true,
                Body = body
            })
                try
                {
                   //message.CC.Add(copyD);
                   //message.CC.Add(copyE);
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
