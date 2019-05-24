using System;
using System.Data.SqlClient;

public class SqlConnect
{
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
}
