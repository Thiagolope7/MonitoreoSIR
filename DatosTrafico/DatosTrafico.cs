
namespace DatosTrafico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
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
            ConexionSQL();
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
            FileStream ArchivoTxT = new FileStream("C:/Traza/MAIL.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            DateTime TimeMail = DateTime.Now;
            string TimeMail1 = TimeMail.ToString("yyyy-MM-dd HH:mm");
            var fromAddress = new MailAddress("diagindra@gmail.com", "Rutina Hermes");
            var toAddress = new MailAddress("santiagolopera13@gmail.com", "Santiago Agudelo");
            const string fromPassword = "Medellin2017a!";
            const string subject = "Alerta | Datos tráfico";
            string body = Mensaje+TimeMail1;
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
                    Escribir.Write(DateTime.Now+" Se envió mail correctamente con el mensaje -> "+Mensaje+TimeMail1);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();                 
                }
                catch (Exception ex)
                {
                    Escribir.Write(DateTime.Now + " Lo siento en algún lado me perdí este debe ser el error:" + ex);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                }        
        }

        public void CCTV()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/DAI.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
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
                Escribir.Write(DateTime.Now + " ERROR No se encontró datos");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                string Mensaje = "No se encontró datos de CCTV a esta hora ->";
                Mail(Mensaje);
                this.lblCCTV.Visible = true;
                this.lblCCTV.Text = "Stopped";
                this.lblCCTV.BackColor= Color.Red ;
                Conexion.Close();
            }
            else
            {        
                
                Escribir.Write(DateTime.Now + " INFO Se encontró datos");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.lblCCTV.Visible = true;
                this.lblCCTV.Text = "Running";
                Conexion.Close();
                return;
            }    
           
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            this.btnStart.Enabled = false;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            CCTV();
            ARS();
            FDT();

        }
        public void ARS()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/ARS.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            string AhoraString = Ahora.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-30);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_1MIN where Logic_code in (select ELEM_GEN_COD_LOG " +
                                "from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where TIP_EQUIP_CODIGO = 3 and ELE_TIP_EQUIP_CODIGO is null) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' order by fecha desc ", Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Escribir.Write(DateTime.Now + " ERROR No se encontró datos ARS");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label5.Visible = true; 
                this.label5.BackColor = Color.Red;
                this.label5.Text = "Stopped";
                string Mensaje = "No se encontró datos de ARS a esta hora -> ";
                Mail(Mensaje);
                Conexion.Close();
            }
            else
            {
                
                Escribir.Write(DateTime.Now + " INFO Se encontró datos de ARS");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label5.Visible = true;
                this.label5.Text = "Running";
                Conexion.Close();
                return;
            }
        }
        public void FDT()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/FDT.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            DateTime Ahora2 = Ahora.AddMinutes(-90);
            string AhoraString = Ahora2.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-180);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
        
                SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_HORA where Logic_code in (select ELEM_GEN_COD_LOG " +
                                "from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where ELE_TIP_EQUIP_CODIGO = 1004) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' ", Conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                Escribir.Write(DateTime.Now + " ERROR no encontrarón datos de FDT");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label6.Visible = true;
                this.label6.BackColor = Color.Red;
                this.label6.Text = "Stopped";
                string Mensaje = "No se encontró datos de FDT a esta hora -> ";
                Mail(Mensaje);
                Conexion.Close();
            }
        
            else
            {
                Escribir.Write(DateTime.Now + " INFO Se encontrarón datos de FDT");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label6.Visible = true;
                this.label6.Text = "Running";
                Conexion.Close();
                return;
            }
        }
        public void Logger()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/Logger.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "Logger-final";
            Process[] Logger = Process.GetProcessesByName(Proceso);
            if (Logger.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label10.Visible = true;
                this.label10.Text = "Running";
                this.label10.BackColor = Color.Green;

            }
            else
            {
                if (Logger.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV Logger se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label10.Visible = true;
                    this.label10.Text = "Stopped";
                    this.label10.BackColor = Color.Red;
                }
                if (Logger.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV Logger se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label10.Visible = true;
                    this.label10.Text = "Duplicated";
                    this.label10.BackColor = Color.Yellow;
                }
                return;

            }
        }

        public void BdvDAI()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/BdvDAI.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "Bdv_DAI-final";
            Process[] Bdv_DAI = Process.GetProcessesByName(Proceso);
            if (Bdv_DAI.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label11.Visible = true;
                this.label11.Text = "Running";
                this.label11.BackColor = Color.Green;

            }
            else
            {
                if (Bdv_DAI.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV DAI se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Stopped";
                    this.label11.BackColor = Color.Red;
                }
                if (Bdv_DAI.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + ",esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV DAI se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Duplicated";
                    this.label11.BackColor = Color.Yellow;
                }
                return;

            }
        }

        public void ETD()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/BdvETD.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "BDVIntegNTCIP-ETD";
            Process[] BdvETD = Process.GetProcessesByName(Proceso);
            if (BdvETD.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label12.Visible = true;
                this.label12.Text = "Running";
                this.label12.BackColor = Color.Green;

            }
            else
            {
                if (BdvETD.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV ETD se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label12.Visible = true;
                    this.label12.Text = "Stopped";
                    this.label12.BackColor = Color.Red;
                }
                if (BdvETD.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + ",esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV ETD se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label12.Visible = true;
                    this.label12.Text = "Duplicated";
                    this.label12.BackColor = Color.Yellow;
                }
                return;

            }
        }
        public void DriveDAI()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/DriverDAI.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "DriverDAI-final";
            Process[] DriverDAI = Process.GetProcessesByName(Proceso);
            if (DriverDAI.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label13.Visible = true;
                this.label13.Text = "Running";
                this.label13.BackColor = Color.Green;

            }
            else
            {
                if (DriverDAI.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La Driver-DAI se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Stopped";
                    this.label13.BackColor = Color.Red;
                }
                if (DriverDAI.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + ",esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "El Driver-DAI se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Duplicated";
                    this.label13.BackColor = Color.Yellow;
                }
                return;

            }
        }


        private void BtnProcesos_Click(object sender, EventArgs e)
        {
          
            timer2.Enabled = true;
            timer2.Interval = 60000;
            timer2.Start();
            this.btnProcesos.Enabled = false; 
          
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
    
            Logger();
            BdvDAI();
        }
    }

}

