using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public class SqlConnect
{
    public static  SqlConnection ConexionSQL()
    {
        SqlConnection Conexion;
        try
        {
            Conexion = new SqlConnection("Data Source=10.158.64.91;Initial Catalog=MEDELLIN_HIST;Persist Security Info=True;User ID=indra;Password=0f120400DdBblog;MultipleActiveResultSets=true");
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se conecto con la base de datos:" + ex.ToString());
            return null;
        }
        return Conexion;
    }
}
