using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApplication1.ConexionBD
{
    public class BaseDeDatos
    {
        public String parametrosConexionDB;
        private String esquema;


        public BaseDeDatos()
        {
            /*parametrosConexionDB = "Server=" + System.Configuration.ConfigurationManager.AppSettings["server"] + ";"
                + "Database=" + System.Configuration.ConfigurationManager.AppSettings["database"] + ";"
                + "User ID=" + System.Configuration.ConfigurationManager.AppSettings["id"] + ";"
                + "Password=" + System.Configuration.ConfigurationManager.AppSettings["password"];*/

            //esquema = System.Configuration.ConfigurationManager.AppSettings["esquema"];
            esquema = "C_HASHTAG";

            parametrosConexionDB = "Server=localhost\\SQLSERVER2012;Database=GD1C2016;USER ID=gd;Password=gd2016";

        }

        public SqlCommand obtenerStoredProcedure(String nombre)
        {
            SqlConnection conexion = new SqlConnection(parametrosConexionDB);
            conexion.Open();
            SqlCommand sp = new SqlCommand(esquema + "." + nombre, conexion);
            sp.CommandType = CommandType.StoredProcedure;
            return sp;
        }



    }
 }
