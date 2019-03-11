
namespace DatosTrafico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class DatosTrafico : Form
    {
        SqlConnection Conexion;
        public DatosTrafico()
        {
            InitializeComponent();     
        }

        public void ConexionSQL()
        {
            try
            {
                Conexion = new SqlConnection("Data Source=10.158.64.91;Initial Catalog=MEDELLIN_HIST;Persist Security Info=True;User ID=indra;Password=0f120400DdBblog");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la base de datos:" + ex.ToString());

            }
            return;

        }

        static void Mail(string Mensaje)
        {
            var fromAddress = new MailAddress("diagindra@gmail.com", "Rutina Hermes");
            var toAddress = new MailAddress("santiagolopera13@gmail.com", "Santiago Agudelo");
            const string fromPassword = "Medellin2017a!";
            const string subject = "Alerta";
            string body = Mensaje;
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
                    FileStream ArchivoTxT = new FileStream("C:/Traza/MAIL.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                    Escribir.Write(DateTime.Now+"Se envió mail correctamente con el mensaje"+Mensaje);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                }
                catch (Exception ex)
                {
                    FileStream ArchivoTxT = new FileStream("C:/Traza/MAIL.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                    Escribir.Write(DateTime.Now + " Lo siento en algún lado me perdí este debe ser el error:" + ex);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                }
        }


        public void CCTV()
        {
            ConexionSQL();
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            string AhoraString = Ahora.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-30);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");

            SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_1MIN where Logic_code in (select ELEM_GEN_COD_LOG  from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where TIP_EQUIP_CODIGO in (3, 14, 15) " +
                                             " and ELE_TIP_EQUIP_CODIGO in (14, null)) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' order by fecha desc ", Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                FileStream ArchivoTxT = new FileStream("C:/Traza/DAI.txt", FileMode.Append, FileAccess.Write);
                StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                Escribir.Write(DateTime.Now + " ERROR No se encontró datos");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                string Mensaje = "No se encontró datos de CCTV para las "+DateTime.Now;
                Mail(Mensaje);
                this.lblCCTV.Text = "Stoped";
                this.lblCCTV.BackColor= Color.Red ;

            }
            else
            {
           
                FileStream ArchivoTxT = new FileStream("C:/Traza/DAI.txt", FileMode.Append, FileAccess.Write);
                StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                Escribir.Write(DateTime.Now + " INFO Se encontró datos");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.lblCCTV.Text = "Running";
                return;
            }    
            Conexion.Close();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start(); 
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            CCTV();
        }
    }

}

