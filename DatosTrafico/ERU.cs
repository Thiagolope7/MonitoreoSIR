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

namespace DatosTrafico
{
	public partial class ERU : Form
	{
		SqlConnection Conexion;
		public ERU()
		{
			InitializeComponent();
			ConexionSQL();
			Datos_act();
			
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
		public void Datos_act()
		{
			string query = "SELECT fecha,nombre,c_registros FROM Seg_datos_logic";
			Conexion.Open();

			SqlCommand comando = new SqlCommand(query,Conexion);
		
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

					dataGridView1.Rows[renglon].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
					dataGridView1.Rows[renglon].DefaultCellStyle.ForeColor = System.Drawing.Color.White;

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
				else {
					normal++;
				}


				dataGridView1.Rows[renglon].Cells[1].Value = dr.GetInt32(dr.GetOrdinal("c_registros")).ToString();
			}
			Series series = this.chart1.Series.Add("Debajo");
			series.Points.Add(debajo);
			series = this.chart1.Series.Add("normal");
			series.Points.Add(normal);
			series = this.chart1.Series.Add("arriba");
			series.Points.Add(arriba);


			Conexion.Close();
			//DataSet ds = new DataSet();
			//da.Fill(ds);
			//while (da.)
			//dataGridView1.Item
		}

	}
}
