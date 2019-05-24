using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DatosTrafico
{
    public class EscribeLog
    {

        public static void escribe(string traza,string archivo)
        {
            string log = "C:/Traza/";
            if (Directory.Exists(log))
            {

            }
            else
            {
                Directory.CreateDirectory(log);
            }
            FileStream ArchivoTxT = new FileStream(log + archivo, FileMode.Append, FileAccess.Write);
            StreamWriter Escribir = new StreamWriter(ArchivoTxT);
            Escribir.Write(DateTime.Now + " " + traza);
            Escribir.WriteLine();
            Escribir.Flush();
            Escribir.Close();

        }
    }
}
