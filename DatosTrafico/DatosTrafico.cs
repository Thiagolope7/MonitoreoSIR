﻿
namespace DatosTrafico
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class DatosTrafico : Form
    {
        int val = 0; 
        public static String Logarchivo;
        SqlConnection Conexion = SqlConnect.ConexionSQL();
        public int Cuenta = 10;
        public DatosTrafico()
        {
            InitializeComponent();
            this.btnProcesos.Enabled = false; 

        }
        public void CCTV()
        {
            Logarchivo = "DAI.txt";
            DateTime Val = DateTime.Now;
            string Val1 = Val.ToString("HH:mm");
            if (Val1.Contains("00:05"))
            {
                val += 1; 
                Conexion.Open();
                DateTime Ahora = DateTime.Now;
                DateTime Ahora1 = Ahora.AddMinutes(-10);
                string AhoraString = Ahora1.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                SqlCommand da = new SqlCommand("SELECT * FROM MEDELLIN_HIST..Seg_reg_min" +
                                                       " where fecha > '" + AhoraString + "' and c_registros <= 1296 and nombre like 'DAI-%' " +
                                                       "order by fecha desc ", Conexion);
                SqlDataReader leer;
                int count = 0;               
                string[] nombre = new string[400];
                string[] CRegistro = new string[400];
                leer = da.ExecuteReader();
                while (leer.Read() == true)
                {
                    nombre[count] = leer[1].ToString();
                    CRegistro[count] = leer[2].ToString();
                    count = 1 + count;
                }
                try
                {
                    if (count == 0)
                    {
                        EscribeLog.escribe("Todos los datos están completos", Logarchivo);
                        this.lblCCTV.Visible = true;
                        this.lblCCTV.Text = "Running";
                        this.lblCCTV.BackColor = Color.Green;
                        Conexion.Close();
                    }
                    else
                    {
                        EscribeLog.escribe("Algunos carriles tienen problema escritos", Logarchivo);
                        this.lblCCTV.Visible = true;
                        this.lblCCTV.Text = "Warnning";
                        this.lblCCTV.BackColor = Color.Yellow;
                        string[] Mensaje = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            Mensaje[i] = nombre[i].ToString() + ", " + CRegistro[i].ToString();
                        }
                        EnviarMail.Mail(Mensaje);
                        Conexion.Close();
                        return;
                    }
                }
                catch (SqlException e)
                {
                    EscribeLog.escribe(e.ToString(), Logarchivo);
                    Conexion.Close();
                }
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
            if (val == 0)
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
                    Cuenta = 10;
                }

            } else
            {
                System.Threading.Thread.Sleep(60000);
                val = 0;
            }

            
        }
        public void ARS()
        {
            Logarchivo = "ARS.txt";
            DateTime Val = DateTime.Now;
            string Val1 = Val.ToString("HH:mm");
            if (Val1.Contains("00:05"))
            {
                Conexion.Open();
                DateTime Ahora = DateTime.Now;
                DateTime Ahorahr = Ahora.AddMinutes(-10);
                string AhorahrString = Ahorahr.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                SqlCommand da = new SqlCommand("SELECT * FROM MEDELLIN_HIST..Seg_reg_min" +
                                                       " where fecha > '" + AhorahrString + "' and c_registros <= 1296 and nombre like 'xc-%'" +
                                                       " order by fecha desc  ", Conexion);
                SqlDataReader leer;
                int count = 0;
                string[] nombre = new string[400];
                string[] CRegistro = new string[400];
                leer = da.ExecuteReader();
                while (leer.Read() == true)
                {                  
                    nombre[count] = leer[1].ToString();
                    CRegistro[count] = leer[2].ToString();
                    count = 1 + count;
                }
                try
                {
                    if (count == 0)
                    {
                        EscribeLog.escribe(" Todos los datos están completos", Logarchivo);
                        this.label5.Visible = true;
                        this.label5.Text = "Running";
                        this.label5.BackColor = Color.Green;
                        Conexion.Close();
                    }
                    else
                    {
                        EscribeLog.escribe("Algunos carriles tienen problema escritos", Logarchivo);
                        this.label5.Visible = true;
                        this.label5.BackColor = Color.Yellow;
                        this.label5.Text = "Warnning";
                        string[] Mensaje = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            Mensaje[i] = nombre[i].ToString() + ", " + CRegistro[i].ToString();
                        }
                        EnviarMail.Mail(Mensaje);
                        Conexion.Close();
                        return;
                    }
                }
                catch (SqlException e)
                {
                    EscribeLog.escribe(e.ToString(), Logarchivo);
                    Conexion.Close();
                }
            }
        }

        public void FDT()
        {
            Logarchivo = "FDT.txt";
            DateTime Val = DateTime.Now;
            string Val1 = Val.ToString("HH:mm");
            if (Val1.Contains(":30"))
            {
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
                        //string Mensaje = "No se encontró datos de FDT a esta hora -> ";
                        //EnviarMail.Mail(Mensaje);
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
                catch (SqlException e)
                {
                    EscribeLog.escribe(e.ToString(), Logarchivo);
                }

            }




        }
        public void Logger()
        {
            Logarchivo = "Logger.txt";

            string Proceso = "Logger-final";
            Process[] Logger = Process.GetProcessesByName(Proceso);
            if (Logger.Length == 1)
            {
                EscribeLog.escribe(",El proceso " + Proceso + " esta corriendo", Logarchivo);

                this.label10.Visible = true;
                this.label10.Text = "Running";
                this.label10.BackColor = Color.Green;

            }
            else
            {
                if (Logger.Length == 0)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + " esta detenido", Logarchivo);
                    //string Mensaje = "La BDV Logger se encuentra detenida a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
                    this.label10.Visible = true;
                    this.label10.Text = "Stopped";
                    this.label10.BackColor = Color.Red;
                }
                if (Logger.Length > 1)
                {
                    EscribeLog.escribe(DateTime.Now + ",El proceso " + Proceso + " esta más de una vez", Logarchivo);
                    //string Mensaje = "La BDV Logger se encuentra duplicada a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
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
                EscribeLog.escribe(",El proceso " + Proceso + " esta corriendo", Logarchivo);

                this.label11.Visible = true;
                this.label11.Text = "Running";
                this.label11.BackColor = Color.Green;

            }
            else
            {
                if (Bdv_DAI.Length == 0)
                {
                    EscribeLog.escribe(DateTime.Now + ",El proceso " + Proceso + " esta detenido", Logarchivo);

                    //string Mensaje = "La BDV DAI se encuentra detenida a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
                    this.label11.Visible = true;
                    this.label11.Text = "Stopped";
                    this.label11.BackColor = Color.Red;
                }
                if (Bdv_DAI.Length > 1)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + ",esta más de una vez", Logarchivo);
                    //string Mensaje = "La BDV DAI se encuentra duplicada a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
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
                EscribeLog.escribe(",El proceso " + Proceso + " esta corriendo", Logarchivo);
                this.label12.Visible = true;
                this.label12.Text = "Running";
                this.label12.BackColor = Color.Green;

            }
            else
            {
                if (BdvETD.Length == 0)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + " esta detenido", Logarchivo);

                    //string Mensaje = "La BDV ETD se encuentra detenida a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
                    this.label12.Visible = true;
                    this.label12.Text = "Stopped";
                    this.label12.BackColor = Color.Red;
                }
                if (BdvETD.Length > 1)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + ",esta más de una vez", Logarchivo);
                    //string Mensaje = "La BDV ETD se encuentra duplicada a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
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
                EscribeLog.escribe(",El proceso " + Proceso + " esta corriendo", Logarchivo);
                this.label13.Visible = true;
                this.label13.Text = "Running";
                this.label13.BackColor = Color.Green;

            }
            else
            {
                if (DriverDAI.Length == 0)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + " esta detenido", Logarchivo);
                    //string Mensaje = "La Driver-DAI se encuentra detenida a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
                    this.label13.Visible = true;
                    this.label13.Text = "Stopped";
                    this.label13.BackColor = Color.Red;
                }
                if (DriverDAI.Length > 1)
                {
                    EscribeLog.escribe(",El proceso " + Proceso + ",esta más de una vez", Logarchivo);
                    //string Mensaje = "El Driver-DAI se encuentra duplicada a esta hora -> ";
                    //EnviarMail.Mail(Mensaje);
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
            // this.btnProcesos.Enabled = true;

        }

        private void DatosTrafico_Load(object sender, EventArgs e)
        {

        }
    }

}

