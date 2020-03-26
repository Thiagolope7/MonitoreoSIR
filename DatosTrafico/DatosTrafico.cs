
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
        public static String Logarchivo;
        SqlConnection Conexion= SqlConnect.ConexionSQL();
        public int Cuenta = 60;
        public DatosTrafico()
        {
            InitializeComponent();
            
        }

 

        public void CCTV()
        {
            Logarchivo = "DAI.txt";
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            string AhoraString = Ahora.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-30);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
           

            SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_1MIN where Logic_code in (select ELEM_GEN_COD_LOG  from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where TIP_EQUIP_CODIGO in (3, 14, 15) " +
                                             " and ELE_TIP_EQUIP_CODIGO in (14, null)) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' order by fecha desc ", Conexion);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                EscribeLog.escribe(" ERROR No se encontró datos",Logarchivo);              
                string Mensaje = "No se encontró datos de CCTV a esta hora ->";
                EnviarMail.Mail(Mensaje);
                this.lblCCTV.Visible = true;
                this.lblCCTV.Text = "Stopped";
                this.lblCCTV.BackColor= Color.Red ;
                Conexion.Close();
            }
            else
            {

                EscribeLog.escribe(" INFO Se encontró datos",Logarchivo);               
                this.lblCCTV.Visible = true;
                this.lblCCTV.Text = "Running";
                Conexion.Close();
                return;
            }
            }
            catch (SqlException e)
            {
                EscribeLog.escribe(e.ToString(), Logarchivo);
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
            if (Cuenta != 0)
            {
                this.label21.Text = Cuenta.ToString();
                Cuenta -= 1;
            }
            else
            {
                CCTV();
                ARS();
                FDT();
                Cuenta = 60;
            }
            

        }
        public void ARS()
        {
            Logarchivo = "ARS.txt";
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            string AhoraString = Ahora.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-30);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_1MIN where Logic_code in (select ELEM_GEN_COD_LOG " +
                                "from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where TIP_EQUIP_CODIGO = 3 and ELE_TIP_EQUIP_CODIGO is null) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' order by fecha desc ", Conexion);
            DataTable dt = new DataTable();
            try { 
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                EscribeLog.escribe( " ERROR No se encontró datos ARS",Logarchivo);               
                this.label5.Visible = true; 
                this.label5.BackColor = Color.Red;
                this.label5.Text = "Stopped";
                string Mensaje = "No se encontró datos de ARS a esta hora -> ";
                EnviarMail.Mail(Mensaje);
                Conexion.Close();
            }
            else
            {

                EscribeLog.escribe( " INFO Se encontró datos de ARS",Logarchivo);
                this.label5.Visible = true;
                this.label5.Text = "Running";
                Conexion.Close();
                return;
            }
            }
            catch (SqlException e)
            {
                EscribeLog.escribe(e.ToString(), Logarchivo);
            }
        }
        public void FDT()
        {
            Logarchivo = "FDT.txt";
            Conexion.Open();
            DateTime Ahora = DateTime.Now;
            DateTime Ahora2 = Ahora.AddMinutes(-90);
            string AhoraString = Ahora2.ToString("yyyy-MM-dd HH:mm:ss.FFF");
            DateTime Ahora1 = Ahora.AddMinutes(-180);
            string Ahora1String = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
        
                SqlDataAdapter da = new SqlDataAdapter("select top 10 Logic_code,Fecha,Intensidad,VehiculoLongitud1,VehiculoLongitud2,VehiculoLongitud3,Ocupacion,Velocidad from MEDELLIN_HIST..H_TRAF_DETECTOR_DATA_HORA where Logic_code in (select ELEM_GEN_COD_LOG " +
                                "from MEDELLIN_CONF..TUN_ELEMENTO_GENERICO where ELE_TIP_EQUIP_CODIGO = 1004) and fecha between '" + Ahora1String + "' and '" + AhoraString + "' ", Conexion);
                DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    EscribeLog.escribe(" ERROR no encontrarón datos de FDT", Logarchivo);
                    this.label6.Visible = true;
                    this.label6.BackColor = Color.Red;
                    this.label6.Text = "Stopped";
                    string Mensaje = "No se encontró datos de FDT a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    Conexion.Close();
                }

                else
                {
                    EscribeLog.escribe(" INFO Se encontrarón datos de FDT", Logarchivo);
                    this.label6.Visible = true;
                    this.label6.Text = "Running";
                    Conexion.Close();
                    return;
                }
            }
            catch (SqlException e) {
                EscribeLog.escribe(e.ToString(), Logarchivo);
            }
        }
        public void Logger()
        {
            Logarchivo ="Logger.txt";

            string Proceso = "Logger-final";
            Process[] Logger = Process.GetProcessesByName(Proceso);
            if (Logger.Length == 1)
            {
                EscribeLog.escribe( ",El proceso " + Proceso + " esta corriendo",Logarchivo);
               
                this.label10.Visible = true;
                this.label10.Text = "Running";
                this.label10.BackColor = Color.Green;

            }
            else
            {
                if (Logger.Length == 0)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + " esta detenido",Logarchivo);                   
                    string Mensaje = "La BDV Logger se encuentra detenida a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label10.Visible = true;
                    this.label10.Text = "Stopped";
                    this.label10.BackColor = Color.Red;
                }
                if (Logger.Length > 1)
                {
                    EscribeLog.escribe(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez",Logarchivo);                
                    string Mensaje = "La BDV Logger se encuentra duplicada a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label10.Visible = true;
                    this.label10.Text = "Duplicated";
                    this.label10.BackColor = Color.Yellow;
                }
                return;

            }
        }

        public void BdvDAI()
        {
            Logarchivo = "Bdv_DAI.txt";
            string Proceso = "Bdv_DAI-final";
            Process[] Bdv_DAI = Process.GetProcessesByName(Proceso);
            if (Bdv_DAI.Length == 1)
            {
                EscribeLog.escribe( ",El proceso " + Proceso + " esta corriendo",Logarchivo);
              
                this.label11.Visible = true;
                this.label11.Text = "Running";
                this.label11.BackColor = Color.Green;

            }
            else
            {
                if (Bdv_DAI.Length == 0)
                {
                    EscribeLog.escribe(DateTime.Now + ",El proceso " + Proceso + " esta detenido",Logarchivo);
                   
                    string Mensaje = "La BDV DAI se encuentra detenida a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Stopped";
                    this.label11.BackColor = Color.Red;
                }
                if (Bdv_DAI.Length > 1)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + ",esta más de una vez",Logarchivo);                  
                    string Mensaje = "La BDV DAI se encuentra duplicada a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Duplicated";
                    this.label11.BackColor = Color.Yellow;
                }
                return;

            }
        }

        public void ETD()
        {
            Logarchivo = "BdvETD.txt";
            string Proceso = "BDVIntegNTCIP-ETD";
            Process[] BdvETD = Process.GetProcessesByName(Proceso);
            if (BdvETD.Length == 1)
            {
                EscribeLog.escribe( ",El proceso " + Proceso + " esta corriendo",Logarchivo);               
                this.label12.Visible = true;
                this.label12.Text = "Running";
                this.label12.BackColor = Color.Green;

            }
            else
            {
                if (BdvETD.Length == 0)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + " esta detenido",Logarchivo);
                  
                    string Mensaje = "La BDV ETD se encuentra detenida a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label12.Visible = true;
                    this.label12.Text = "Stopped";
                    this.label12.BackColor = Color.Red;
                }
                if (BdvETD.Length > 1)
                {
                    EscribeLog.escribe( ",El proceso " + Proceso + ",esta más de una vez",Logarchivo);
                    string Mensaje = "La BDV ETD se encuentra duplicada a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label12.Visible = true;
                    this.label12.Text = "Duplicated";
                    this.label12.BackColor = Color.Yellow;
                }
                return;

            }
        }
        public void DriverDAI()
        {
            Logarchivo = "DriverDAI.txt";
            string Proceso = "DriverDAI-final";
            Process[] DriverDAI = Process.GetProcessesByName(Proceso);
            if (DriverDAI.Length == 1)
            {
                EscribeLog.escribe(",El proceso " + Proceso + " esta corriendo",Logarchivo);               
                this.label13.Visible = true;
                this.label13.Text = "Running";
                this.label13.BackColor = Color.Green;

            }
            else
            {
                if (DriverDAI.Length == 0)
                {
                    EscribeLog.escribe( ",El proceso " + Proceso + " esta detenido",Logarchivo);                    
                    string Mensaje = "La Driver-DAI se encuentra detenida a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Stopped";
                    this.label13.BackColor = Color.Red;
                }
                if (DriverDAI.Length > 1)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + ",esta más de una vez",Logarchivo);                 
                    string Mensaje = "El Driver-DAI se encuentra duplicada a esta hora -> ";
                    EnviarMail.Mail(Mensaje);
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
            timer2.Interval = 1000;
            timer2.Start();
            this.btnProcesos.Enabled = false; 
          
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (Cuenta != 0)
            {
                this.label19.Text = Cuenta.ToString();
                Cuenta -= 1;
            }
            else
            {
                Logger();
                BdvDAI();
                DriverDAI();
                ETD();
                Cuenta = 60;
            }
            
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            this.btnStart.Enabled = true;
            this.btnProcesos.Enabled = true;

        }

        private void DatosTrafico_Load(object sender, EventArgs e)
        {

        }
    }

}

