using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Carvajal.configuracion
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            SqlConnection conexion = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\vicho\\source\\repos\\Carvajal\\Carvajal\\App_Data\\prueba4.mdf;Integrated Security=True");
            conexion.Open();
            return conexion;
        }
    }
}