using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DatosTrafico
{
    public partial class ERU : Form
    {
        //private SqlConnection Conexion;
        private int a1 = 0;
        public static string Logarchivo = "ftp.txt";
        public ERU()
        {

            InitializeComponent();



            ThreadStart delegado = new ThreadStart(Datos_act);
            ThreadStart delegado2 = new ThreadStart(desc_ar);
            // creamos el delegado 
            Thread hilo = new Thread(delegado);
            Thread hilo1 = new Thread(delegado2);
            //creamos el hilo
            hilo.Start();



        }


        public void Datos_act()
        {
            int c1 = 1;



            Series series = new Series();

            string query = "SELECT fecha,nombre,c_registros FROM Seg_datos_logic";
            SqlConnection Conexion = SqlConnect.ConexionSQL();
            Conexion.Open();
            SqlCommand comando = new SqlCommand(query, Conexion);

            comando.CommandText = "SELECT fecha,nombre,c_registros FROM Seg_datos_logic";
            //comando.CommandType = CommandType.Text;
            //comando.
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                {
                    dataGridView1.Rows.Clear();
                }));
            }
            else
            {
                dataGridView1.Rows.Clear();
            }

            //DataGridViewCellFormattingEventArgs e ;

            //SqlDataAdapter da = new SqlDataAdapter("SELECT fecha,nombre,c_registros FROM Seg_datos_logic",Conexion);
            SqlDataReader dr = comando.ExecuteReader();
            int debajo = 0, arriba = 0, normal = 0, renglon = 0;


            while (dr.Read())
            {

                if (this.InvokeRequired)
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        renglon = dataGridView1.Rows.Add();
                    }));
                }
                else
                {
                    renglon = dataGridView1.Rows.Add();
                }

                dataGridView1.Rows[renglon].Cells[0].Value = dr.GetString(dr.GetOrdinal("nombre"));
                if (dr.GetInt32(dr.GetOrdinal("c_registros")) <= 54)
                {
                    debajo++;
                    //this.dataGridView1.Columns[e.ColumnIndex].Name == "c_registros";

                    //dataGridView1.Rows[renglon].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    //dataGridView1.Rows[renglon].DefaultCellStyle.ForeColor = System.Drawing.Color.White;

                    //e.CellStyle/Rows[renglon].Cells[0].cel
                }
                else
                if (dr.GetInt32(dr.GetOrdinal("c_registros")) >= 66)
                {
                    arriba++;
                    //this.dataGridView1.Columns[e.ColumnIndex].Name == "c_registros";

                    dataGridView1.Rows[renglon].DefaultCellStyle.BackColor = System.Drawing.Color.Blue;
                    dataGridView1.Rows[renglon].DefaultCellStyle.ForeColor = System.Drawing.Color.White;

                    //e.CellStyle/Rows[renglon].Cells[0].cel
                }
                else
                {
                    normal++;
                }


                dataGridView1.Rows[renglon].Cells[1].Value = dr.GetInt32(dr.GetOrdinal("c_registros")).ToString();
            }
            //Series series = this.chart1.Series.Add("Debajo");
            if (c1 > 1)
            {
                Invoke((MethodInvoker)(() =>
                {

                    series = this.chart1.Series.Add("< 54");
                    series.Points.Add(debajo);
                    series = this.chart1.Series.Add("> 66");
                    series.Points.Add(arriba);
                    Conexion.Close();
                    System.Threading.Thread.Sleep(3600000);
                    chart1.Series.Clear();
                }));

            }
            else
            {
                //	series = this.chart1.Series.Add("< 54");
                //	series.Points.Add(debajo);
                //	series = this.chart1.Series.Add("> 66");
                //	series.Points.Add(arriba);
                //	Conexion.Close();
                System.Threading.Thread.Sleep(2000);
                c1 = 2;
                //chart1.Series.Clear();

                //Invoke((MethodInvoker)(() =>
                //{chart1.Series.Clear();
                //}));
            }
            c1 = 2;





        }




        public void desc_ar()

        {


            int hora = 3600000;
            string query = "SELECT dir_ip FROM medellin_conf..c_com_equip_ip", archivo;
            SqlConnection Conexion = SqlConnect.ConexionSQL();
            Conexion.Open();
            SqlCommand comando = new SqlCommand(query, Conexion);

            comando.CommandText = "SELECT dir_ip FROM medellin_conf..c_com_equip_ip";
            string result = "";
            SqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {

                archivo = dr.GetString(dr.GetOrdinal("dir_ip"));
                EscribeLog.escribe("comprobando archivos en el dir " + archivo.ToString(), Logarchivo);
                string[] ip = archivo.Split('.');

                result = listaftp(archivo, ip[3]);
                EscribeLog.escribe(result, Logarchivo);
            }
            Conexion.Close();
            hora = hora * 24;
            EscribeLog.escribe("se finaliza la tarea de descargar", Logarchivo);
            DateTime horaaa =DateTime.Now;
            Ult_des_ERU.Text = horaaa.ToString();

        }

        public String listaftp(string archivo, string dir)
        {
            string nombre = " ", result = "";


            String Rp = "ftp://" + archivo + "/datos/", usr = "etdi", pa = "etdi";
            EscribeLog.escribe("leyendo lista de archivos remotos", Logarchivo);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Rp);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(usr, pa);
            FtpWebResponse response;
            try
            {
                EscribeLog.escribe("intentado establecer conexion a la dir: " + archivo, Logarchivo);
                response = (FtpWebResponse)request.GetResponse();

            }
            catch (System.Net.WebException e)
            {
                EscribeLog.escribe("error al intentar conectar con la dir: " + archivo, Logarchivo);
                return e.ToString();
            }

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string names = reader.ReadToEnd();
            string path = @"C:\ftp\" + dir;

            //foreach ()
            //DirectoryInfo di = new DirectoryInfo(@"C:\ftp\");
            if (Directory.Exists(path))
            {

            }
            else
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo di = new DirectoryInfo(path);
            string a1 = ".DAT";

            foreach (string b in names.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList())
            {

                //int comp = string.Compare(b, a1);
                if (b.EndsWith(a1))
                {
                    var fi = di.GetFiles(b);
                    if (fi.LongLength == 0)
                    {
                        //listView1.Items.Add(b.ToString() + " El archivo no se encuentra descargado ");
                        string desc = Rp + b;
                        nombre = desc;
                        EscribeLog.escribe("el arcivo no se encuentra local, se inicia descarga ", Logarchivo);
                        result = descargarFic(desc, usr, pa, path);
                        EscribeLog.escribe(result, Logarchivo);
                    }
                    else
                    {
                        result = " El archivo " + Rp + b + " ya se encuentra descargado ";
                        EscribeLog.escribe(result, Logarchivo);
                    }

                }

            }
            reader.Close();
            response.Close();
            return "Descarga finalizada";
        }
        static string descargarFic(string ficFTP, string user, string pass, string dirLocal)
        {
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(ficFTP));

            // Los datos del usuario (credenciales)
            NetworkCredential cr = new NetworkCredential(user, pass);
            dirFtp.Credentials = cr;
            // El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
            dirFtp.Method = WebRequestMethods.Ftp.DownloadFile;

            // Obtener el resultado del comando
            //StreamReader reader =new StreamReader(dirFtp.GetResponse().GetResponseStream());
            FtpWebResponse response;
            try
            {
                EscribeLog.escribe("se intenta descargar el archivo " + ficFTP, Logarchivo);
                response = (FtpWebResponse)dirFtp.GetResponse();

            }
            catch (System.Net.WebException e)
            {
                EscribeLog.escribe("error al descargar archivo " + ficFTP, Logarchivo);
                return e.ToString();
            }
            Stream responseStream = response.GetResponseStream();


            // Guardarlo localmente 

            string ficLocal = Path.Combine(dirLocal, Path.GetFileName(ficFTP));
            EscribeLog.escribe("creando dir local  " + ficLocal, Logarchivo);
            FileStream ws = new FileStream(ficLocal, FileMode.Create);
            int Lh = 1024;
            Byte[] buffer = new Byte[Lh];
            int bytesRead = responseStream.Read(buffer, 0, Lh);
            int bytesRead1 = bytesRead;
            EscribeLog.escribe("Se inicia la descarga", Logarchivo);
            while (bytesRead > 0)
            {
                //bytesRead1 =bytesRead1-bytesRead;
                //escribe(" se descargan "+ bytesRead1+" bytes");
                ws.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Lh);
            }
            EscribeLog.escribe("Se finaliza la descarga", Logarchivo);
            //StreamWriter sw = new StreamWriter(ficLocal, false, Encoding.UTF8);
            //sw.Write(res);
            ws.Close();
            response.Close();
            return "se descargo el archivo" + ficFTP.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (a1 == 0)
            {
                //desc_ar();
                //ThreadStart delegado = new ThreadStart(Datos_act);
                ThreadStart delegado2 = new ThreadStart(desc_ar);
                // creamos el delegado 

                Thread hilo1 = new Thread(delegado2);
                hilo1.Start();
            }
            a1++;
        }
    }
}
