using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Net;
using System.IO;

namespace DatosTrafico
{
	public partial class ERU : Form
	{
		//private SqlConnection Conexion;
		private int a1 =0;
		public ERU()
		{
			
			InitializeComponent();
			
				
				
			
			CheckForIllegalCrossThreadCalls = false;

			ThreadStart delegado = new ThreadStart(Datos_act);
			ThreadStart delegado2 = new ThreadStart(desc_ar);
			// creamos el delegado 
			Thread hilo = new Thread(delegado);
			Thread hilo1 = new Thread(delegado2);
			//creamos el hilo
			hilo.Start();
			//a1++;
			//if (a1 == 1)
			//{
			//	hilo1.Start();
			//}
			
			
		}

		public SqlConnection ConexionSQL()
		{
			SqlConnection Conexion;
			try
			{
				Conexion = new SqlConnection("Data Source=10.158.64.91;Initial Catalog=MEDELLIN_HIST;Persist Security Info=True;User ID=indra;Password=0f120400DdBblog");
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se conecto con la base de datos:" + ex.ToString());
				return null;
			}
			return Conexion;
		}
		public void Datos_act()
		{
			
			while (true)
			{
				
				Series series = new Series( );

				



				string query = "SELECT fecha,nombre,c_registros FROM Seg_datos_logic";
				SqlConnection Conexion=ConexionSQL();
				Conexion.Open();
				SqlCommand comando = new SqlCommand(query, Conexion);

				comando.CommandText = "SELECT fecha,nombre,c_registros FROM Seg_datos_logic";
				//comando.CommandType = CommandType.Text;
				//comando.
				dataGridView1.Rows.Clear();
				//DataGridViewCellFormattingEventArgs e ;

				//SqlDataAdapter da = new SqlDataAdapter("SELECT fecha,nombre,c_registros FROM Seg_datos_logic",Conexion);
				SqlDataReader dr = comando.ExecuteReader();
				int debajo = 0, arriba = 0, normal = 0;


				while (dr.Read())
				{
					int renglon = dataGridView1.Rows.Add();
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
				series = this.chart1.Series.Add("Debajo");
				series.Points.Add(debajo);
				series = this.chart1.Series.Add("arriba");
				series.Points.Add(arriba);


				Conexion.Close();
				System.Threading.Thread.Sleep(3600000);
				this.chart1.Series.Clear();
				

				//DataSet ds = new DataSet();
				//da.Fill(ds);
				//while (da.)
				//dataGridView1.Item
			}
			}
		public void desc_ar()
		{
			
			
			int hora = 3600000;
			string query = "SELECT dir_ip FROM medellin_conf..c_com_equip_ip",archivo;
			SqlConnection Conexion = ConexionSQL();
			Conexion.Open();
			SqlCommand comando = new SqlCommand(query, Conexion);

			comando.CommandText = "SELECT dir_ip FROM medellin_conf..c_com_equip_ip";
			string result="";
			SqlDataReader dr = comando.ExecuteReader();
			while (dr.Read())
			{
				
				archivo = dr.GetString(dr.GetOrdinal("dir_ip"));
				escribe("comprobando archivos en el dir " + archivo.ToString());
				string[] ip = archivo.Split('.');
				
				result=listaftp(archivo, ip[3]);
				escribe(result);
				}
			Conexion.Close();
			hora = hora * 24;
			escribe("se finaliza la tarea de descargar");
			//System.Threading.Thread.Sleep(hora);
		}
		public static void escribe(string traza)
		{
			string log = "C:/Traza/";
			if (Directory.Exists(log))
			{

			}
			else
			{
				Directory.CreateDirectory(log);
			}
			FileStream ArchivoTxT = new FileStream(log + "ftp.txt", FileMode.Append, FileAccess.Write);
			StreamWriter Escribir = new StreamWriter(ArchivoTxT);
			Escribir.Write(DateTime.Now + " " + traza);
			Escribir.WriteLine();
			Escribir.Flush();
			Escribir.Close();

		}
		public String listaftp(string archivo, string dir)
		{
			string nombre = " ", result="";


			String Rp = "ftp://"+archivo+"/datos/", usr = "etdi", pa = "etdi";
			escribe("leyendo lista de archivos remotos");
			FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Rp);
			request.Method = WebRequestMethods.Ftp.ListDirectory;

			request.Credentials = new NetworkCredential(usr, pa);
			FtpWebResponse response;
			try
			{
				escribe("intentado establecer conexion a la dir: "+archivo);
				response = (FtpWebResponse)request.GetResponse();

			}
			catch (System.Net.WebException e)
			{
				escribe("error al intentar conectar con la dir: "+archivo);
				return e.ToString();
			}

			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);

			string names = reader.ReadToEnd();
			string path = @"C:\ftp\" + dir ;

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
						escribe("el arcivo no se encuentra local, se inicia descarga ");
						result =descargarFic(desc, usr, pa, path);
						escribe(result);
					}
					else
					{
						result= " El archivo " + Rp + b+ " ya se encuentra descargado ";
						escribe(result);
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
				escribe("se intenta descargar el archivo "+ficFTP);
				response = (FtpWebResponse)dirFtp.GetResponse();

			}
			catch(System.Net.WebException e)
			{
				escribe("error al descargar archivo " + ficFTP);
				return e.ToString();
			}
			Stream responseStream = response.GetResponseStream();


			// Guardarlo localmente 

			string ficLocal = Path.Combine(dirLocal, Path.GetFileName(ficFTP));
			escribe("creando dir local  " + ficLocal);
			FileStream ws = new FileStream(ficLocal, FileMode.Create);
			int Lh = 1024;
			Byte[] buffer = new Byte[Lh];
			int bytesRead = responseStream.Read(buffer, 0, Lh);
			int bytesRead1 = bytesRead;
			while (bytesRead > 0)
			{
				bytesRead1 =bytesRead1-bytesRead;
				escribe(" se descargan "+ bytesRead1+" bytes");
				ws.Write(buffer, 0, bytesRead);
				bytesRead = responseStream.Read(buffer, 0, Lh);
			}
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
			desc_ar();
		}
	}
}
