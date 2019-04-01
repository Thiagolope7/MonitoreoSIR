using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using System.Data.SqlClient;

namespace DatosTrafico
{
    public partial class Operacion : Form
    {
        public static int CountVAL = 0;
        public static int counLeer = 0;
        SqlConnection cn;
        public Operacion()
        {
            InitializeComponent();
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
                    Escribir.Write(DateTime.Now + " Se envió mail correctamente con el mensaje -> " + Mensaje + TimeMail1);
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
        
        public void Kernel()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/Kernel.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "Kernel-final";
            Process[] Kernel = Process.GetProcessesByName(Proceso);
            if (Kernel.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label4.Visible = true;
                this.label4.Text = "Running";
                this.label4.BackColor = Color.Green;
            }
            else
            {
                if (Kernel.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV Kernel se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label4.Visible = true;
                    this.label4.Text = "Stopped";
                    this.label4.BackColor = Color.Red;
                }
                if (Kernel.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV Kernel se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label4.Visible = true;
                    this.label4.Text = "Duplicated";
                    this.label4.BackColor = Color.Yellow;
                }
                return;
            }
        }
        public void Bdv_Logistica()
        {
            string path = "C:/EstadosLOG/Logistica/PRUEBA.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }  
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher(
             "SELECT CommandLine FROM Win32_Process WHERE name = 'Bdv_ES_CProg-Final.exe' "))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    FileStream ArchivoTxT = new FileStream("C:/Traza/ES_CPROG.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                    Escribir.Write(mo["CommandLine"]);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    Console.WriteLine(mo["CommandLine"]);
                }
            }
            StreamReader objReader = new StreamReader("C:/Traza/ES_CPROG.txt");
            string sLine;
            ArrayList arrLOG = new ArrayList();
            while (!objReader.EndOfStream)
            {
                sLine = objReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(sLine))
                    arrLOG.Add(sLine);
            }
            for (int i = 0; i < arrLOG.Count; i++)
            {

                string a = @"C:\AreaTrafico\Proyectos\Medellin\bin\Bdv_ES_CProg-Final.exe  SIGA\COLOMBIA\BDV_ES_LOGIST";
                string b = arrLOG[i].ToString();
                if (a == b)
                {
                    CountVAL = CountVAL + 1;
                    //Escriba.Write(arrLOG[i].ToString());
                    //Escriba.WriteLine();
                    break;

                }
            }
           
            objReader.Close();
            if (CountVAL != 0)
            {
                this.label5.Visible = true;
                this.label5.Text = "Running";
                this.label5.BackColor = Color.Green;
             
                FileStream ArchivoTxT = new FileStream("C:/Traza/Logistica.txt", FileMode.Append, FileAccess.Write);
                StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                Escribir.Write(DateTime.Now + " Bdv Logística está bien");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                return;
            }
            else
            {
                this.label5.Visible = true;
                this.label5.Text = "Stoped";
                this.label5.BackColor = Color.Red;
                FileStream ArchivoTxT = new FileStream("C:/Traza/Logistica.txt", FileMode.Append, FileAccess.Write);
                StreamWriter Escr = new StreamWriter(ArchivoTxT);
                Escr.Write(DateTime.Now + " Bdv Logística tiene problemas");
                Escr.WriteLine();
                Escr.Flush();
                Escr.Close();
                //timer1.Stop();
                try
                {
                    FileStream Auto = new FileStream("C:/EstadosLOG/Bdv_Logistica.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter auto = new StreamWriter(Auto);
                    DateTime Ahora = DateTime.Now;
                    string AhoraString = Ahora.ToString("yyyyMMddHHmmss");
                    string de = @"C:/EstadosLOG/Automatico/El_GEN+Val_Par.txt";
                    string para = @"C:/EstadosLOG/Archivo/" + AhoraString + "_El_GEN+Val_Par.txt";
                    if (File.Exists(de))
                    {
                        File.Move(de, para);
                        auto.Write(DateTime.Now + " Se ha movido un backup automatico a " + para);
                        auto.WriteLine();
                        auto.Flush();
                    }
                    auto.Close();
                    cn.Open();
                    SqlCommand comando = new SqlCommand("select tvpee.EL_GEN_EXT_CODIGO, tvpee.VAL_PARAMETRO_ENT from TUN_ELEMENTO_GENERICO as teg join TUN_ELEMENTO_GENERICO_EXTERNO as tege on teg.ELEM_GEN_CODIGO = tege.ELEM_GEN_CODIGO and teg.TIP_EQUIP_CODIGO = tege.TIP_EQUIP_CODIGO join TUN_VALOR_PARAM_EQ_EXTERNO as tvpee on tvpee.EL_GEN_EXT_CODIGO = tege.EL_GEN_EXT_CODIGO and teg.TIP_EQUIP_CODIGO = tvpee.TIP_EQUIP_CODIGO and tvpee.PARAM_CODIGO = 14 where teg.TIP_EQUIP_CODIGO = 1009 and teg.ELEM_GEN_ACTIVO = 1 order by teg.ELEM_GEN_CODIGO", cn);
                    SqlDataReader leer;
                    leer = comando.ExecuteReader();

                    FileStream Ensayo = new FileStream("C:/EstadosLOG/Automatico/El_GEN+Val_Par.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Ensayar = new StreamWriter(Ensayo);
                    while (leer.Read() == true)
                    {

                        Ensayar.Write(leer[0].ToString() + ";");
                        Ensayar.Write(leer[1].ToString());
                        Ensayar.WriteLine();
                        Ensayar.Flush();
                        counLeer = counLeer + 1;
                    }
                    Ensayar.Close();
                    MessageBox.Show("Se ha guardado estado actual de los agentes", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.label8.Text = counLeer.ToString();
                    cn.Close();
                    this.btnUpdate.Enabled = true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


    }
}

