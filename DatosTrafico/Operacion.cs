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
        public int Cuenta = 60;
        public static int CountUpdate = 0;
        public static int countPB = 0;
        public static int ejecucciones = 0;

        SqlConnection Conexion;
        public Operacion()
        {
            InitializeComponent();
        }
        public void ConexionSQL()
        {
            try
            {
                Conexion = new SqlConnection("Data Source=10.5.3.58;Initial Catalog=MEDELLIN_HIST;Persist Security Info=True;User ID=indra;Password=0f120400DdBblog");
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
            string path = "C:/Traza/EstadosLOG/Logistica.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }  
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher(
             "SELECT CommandLine FROM Win32_Process WHERE name = 'Bdv_ES_CProg-Final.exe' "))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    FileStream ArchivoTxT = new FileStream("C:/Traza/EstadosLOG/Logistica.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                    Escribir.Write(mo["CommandLine"]);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    Console.WriteLine(mo["CommandLine"]);
                }
            }
            StreamReader objReader = new StreamReader("C:/Traza/EstadosLOG/Logistica.txt");
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
                this.label5.Text = "Stopped";
                this.label5.BackColor = Color.Red;
                FileStream ArchivoTxT = new FileStream("C:/Traza/Logistica.txt", FileMode.Append, FileAccess.Write);
                StreamWriter Escr = new StreamWriter(ArchivoTxT);
                Escr.Write(DateTime.Now + " Bdv Logística tiene problemas");
                Escr.WriteLine();
                Escr.Flush();
                Escr.Close();
                string Mensaje = "La BDV Logistica se encuentra detenida a esta hora -> ";
                Mail(Mensaje);
                timer2.Stop();
                try
                {
                    FileStream Auto = new FileStream("C:/Traza/EstadosLOG/Logistica.txt", FileMode.Append, FileAccess.Write);
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
                    Conexion.Open();
                    SqlCommand comando = new SqlCommand("select tvpee.EL_GEN_EXT_CODIGO, tvpee.VAL_PARAMETRO_ENT from TUN_ELEMENTO_GENERICO as teg join TUN_ELEMENTO_GENERICO_EXTERNO as tege on teg.ELEM_GEN_CODIGO = tege.ELEM_GEN_CODIGO and teg.TIP_EQUIP_CODIGO = tege.TIP_EQUIP_CODIGO join TUN_VALOR_PARAM_EQ_EXTERNO as tvpee on tvpee.EL_GEN_EXT_CODIGO = tege.EL_GEN_EXT_CODIGO and teg.TIP_EQUIP_CODIGO = tvpee.TIP_EQUIP_CODIGO and tvpee.PARAM_CODIGO = 14 where teg.TIP_EQUIP_CODIGO = 1009 and teg.ELEM_GEN_ACTIVO = 1 order by teg.ELEM_GEN_CODIGO",Conexion);
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
                    Conexion.Close();
                    this.btnUpdate.Enabled = true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        public void Main()
        {
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher(
          "SELECT CommandLine FROM Win32_Process WHERE name = 'Bdv_ES_CProg-Final.exe' "))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    FileStream ArchivoTxT = new FileStream("C:/Traza/EstadosLOG/Logistica.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escribir = new StreamWriter(ArchivoTxT);
                    Escribir.Write(mo["CommandLine"]);
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    Console.WriteLine(mo["CommandLine"]);
                }
                StreamReader objReader = new StreamReader("C:/Traza/EstadosLOG/Logistica.txt");
                string sLine = "";
                ArrayList arrLOG = new ArrayList();
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrLOG.Add(sLine);
                }
                objReader.Close();
                for (int i = 0; i < arrLOG.Count; i++)
                {
                    string a = @"C:\AreaTrafico\Proyectos\Medellin\bin\Bdv_ES_CProg-Final.exe  SIGA\COLOMBIA\BDV_ES_LOGIST";
                    string b = arrLOG[i].ToString();
                    if (a == b)
                    {
                        CountUpdate = CountUpdate + 1;
                        break;
                    }
                }
                if (CountUpdate != 0)
                {

                    FileStream Escrbir = new FileStream("C:/Traza/EstadosLOG/Automatico/El_GEN+Val_ParAgente.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escriba = new StreamWriter(Escrbir);
                    FileStream Escr = new FileStream("C:/Traza/EstadosLOG/Automatico/El_GEN+Val_ParEstado.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Escri = new StreamWriter(Escr);
                    FileStream Esayar = new FileStream("C:/Traza/EstadosLOG/Automatico/El_GEN+Val_ParUpdate.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter Ensayar = new StreamWriter(Esayar);
                    string[] rows = File.ReadAllLines(@"C:/Traza/EstadosLOG/Automatico/El_GEN+Val_Par.txt", Encoding.Default);
                    var dt = new DataTable();
                    dt.Columns.Add("Col1", typeof(string));
                    dt.Columns.Add("Col2", typeof(string));
                    Conexion.Open();
                    string sqlUpdate = "";
                    progressBar1.Maximum = rows.Length;
                    for (int e = 0; e < /*rows.GetLength(0) - e*/rows.Length; e++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Col1"] = rows[e].Split(';')[0];
                        dr["Col2"] = rows[e].Split(';')[1];
                        Escriba.Write(dr["Col1"].ToString());
                        Escri.Write(dr["Col2"].ToString());
                        Escriba.WriteLine();
                        Escri.WriteLine();
                        Escriba.Flush();
                        Escri.Flush();
                        sqlUpdate = "update TUN_VALOR_PARAM_EQ_EXTERNO set VAL_PARAMETRO_ENT =" + dr["Col2"].ToString() + " where TIP_EQUIP_CODIGO = 1009 and PARAM_CODIGO = 14 and EL_GEN_EXT_CODIGO = '" + dr["Col1"].ToString() + "'";
                        //SqlCommand cmd = new SqlCommand(sqlUpdate, Conexion);
                        //cmd.ExecuteNonQuery();
                        Ensayar.Write(sqlUpdate.ToString());
                        Ensayar.WriteLine();
                        Ensayar.Flush();
                        dt.Rows.Add(dr);
                        progressBar1.Value += 1;
                        ejecucciones = ejecucciones + 1;
                    }
                    Escri.Close();
                    Escriba.Close();
                    Ensayar.Close();
                    Conexion.Close();
                    CountUpdate = 0;
                    this.btnUpdate.Enabled = false;
       
                    this.label18.Text = "Se procesaron " + rows.Length + " registros";
                    
                    MessageBox.Show("Registros actualizados");
                }
                else
                {
                    MessageBox.Show("Recuerda primero restablecer el proceso de la Bdv de logística");
                    this.btnUpdate.Enabled = true;
                    
                }
            }

        }



        public void FDT()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/FDT.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            StreamReader objReader = new StreamReader("C:/Traza/EstadosLOG/Logistica.txt");
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

                string a = @"C:\AreaTrafico\Proyectos\Medellin\bin\Bdv_ES_CProg-Final.exe SIGA\COLOMBIA\BDV_ES_FOTODET";
                string b = arrLOG[i].ToString();
                if (a == b)
                {
                    CountVAL = CountVAL + 1;
                  
                    break;

                }
            }
            objReader.Close();
            if (CountVAL == 1)
            {
                this.label6.Visible = true;
                this.label6.Text = "Running";
                this.label6.BackColor = Color.Green;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                return;
            }
            else if(CountVAL > 1)
            {
                this.label6.Visible = true;
                this.label6.Text = "Duplicated";
                this.label6.BackColor = Color.Yellow;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta duplicado");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                string Mensaje = "La BDV Logistica se encuentra duplicada a esta hora -> ";
                Mail(Mensaje);
                return;
            }
            else if(CountVAL == 0)
            {
                this.label6.Visible = true;
                this.label6.Text = "Stopped";
                this.label6.BackColor = Color.Red;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta detenido");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                string Mensaje = "La BDV Logistica se encuentra detenida a esta hora -> ";
                Mail(Mensaje);
                return;
            }
        }
        public void AVL()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/AVL.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            StreamReader objReader = new StreamReader("C:/Traza/EstadosLOG/Logistica.txt");
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

                string a = @"C:\AreaTrafico\Proyectos\Medellin\bin\Bdv_ES_CProg-Final.exe SIGA\COLOMBIA\BDV_ES_AVL";
                string b = arrLOG[i].ToString();
                if (a == b)
                {
                    CountVAL = CountVAL + 1;

                    break;

                }
            }
            objReader.Close();
            if (CountVAL == 1)
            {
                this.label7.Visible = true;
                this.label7.Text = "Running";
                this.label7.BackColor = Color.Green;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                return;
            }
            else if (CountVAL > 1)
            {
                this.label7.Visible = true;
                this.label7.Text = "Duplicated";
                this.label7.BackColor = Color.Yellow;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta duplicado");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                string Mensaje = "La BDV Logistica se encuentra duplicada a esta hora -> ";
                Mail(Mensaje);
                return;
            }
            else if (CountVAL == 0)
            {
                this.label7.Visible = true;
                this.label7.Text = "Stopped";
                this.label7.BackColor = Color.Red;
                Escribir.Write(DateTime.Now + ",El proceso Bdv_Fotodetección esta detenido");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                CountVAL = 0;
                string Mensaje = "La BDV Logistica se encuentra detenida a esta hora -> ";
                Mail(Mensaje);
                return;
            }
        }

        public void PMV()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/PMV.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "BDVIntegNTCIP-Final";
            Process[] PMV = Process.GetProcessesByName(Proceso);
            if (PMV.Length == 1)
            {
                Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta corriendo");
                Escribir.WriteLine();
                Escribir.Flush();
                Escribir.Close();
                this.label9.Visible = true;
                this.label9.Text = "Running";
                this.label9.BackColor = Color.Green;
            }
            else
            {
                if (PMV.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV PMV se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label9.Visible = true;
                    this.label9.Text = "Stopped";
                    this.label9.BackColor = Color.Red;
                }
                if (PMV.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV PMV se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label9.Visible = true;
                    this.label9.Text = "Duplicated";
                    this.label9.BackColor = Color.Yellow;
                }
                return;
            }
        }

        public void MTV()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/MTV.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "BDVMATRIZ_Final";
            Process[] MTV = Process.GetProcessesByName(Proceso);
            if (MTV.Length == 1)
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
                if (MTV.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV MTV se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Stopped";
                    this.label11.BackColor = Color.Red;
                }
                if (MTV.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV MTV se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Duplicated";
                    this.label11.BackColor = Color.Yellow;
                }
                return;
            }
        }

        public void JMS()
        {
            FileStream ArchivoTxT = new FileStream("C:/Traza/JMS.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            string Proceso = "InterfazIntegracionesJMS-Final";
            Process[] JMS = Process.GetProcessesByName(Proceso);
            if (JMS.Length == 1)
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
                if (JMS.Length == 0)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta detenido");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV MTV se encuentra detenida a esta hora -> ";
                    Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Stopped";
                    this.label13.BackColor = Color.Red;
                }
                if (JMS.Length > 1)
                {
                    Escribir.Write(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez");
                    Escribir.WriteLine();
                    Escribir.Flush();
                    Escribir.Close();
                    string Mensaje = "La BDV MTV se encuentra duplicada a esta hora -> ";
                    Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Duplicated";
                    this.label13.BackColor = Color.Yellow;
                }
                return;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            timer2.Enabled = true;
            timer2.Interval = 1000;
            timer2.Start();
            this.btnStart.Enabled = false;

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
         
            if (Cuenta != 0)
            {
                this.label21.Text = Cuenta.ToString();
                Cuenta -= 1;
            }
            else
            {
                Kernel();  
                AVL();
                FDT();
                PMV();
                MTV();
                JMS();
                Cuenta = 60;
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            Bdv_Logistica();
        }

        private void BtnPausar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            this.btnStart.Enabled = true;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            Main();
            this.btnUpdate.Enabled = true; 
        }
    }
}

