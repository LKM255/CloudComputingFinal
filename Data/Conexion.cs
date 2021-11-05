using CloudComputingFinal.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingFinal.Data
{
    public class Conexion
    {
        SqlCommand comando;
        SqlConnection con;
        SqlDataReader reader;

        public Datos BuscarDNI(string dni)
        {
            //List<Datos> listdat = new List<Datos>();
            Datos dat = new Datos();

            con = new SqlConnection("Server=tcp:buscardnidbserver.database.windows.net,1433;Initial Catalog=BuscarDni_db;Persist Security Info=False;" +
                "User ID=jeanpierre;Password=Jean2551;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            string fd = "select * from Datos where DNI='" + dni + "'";
            comando = new SqlCommand(fd, con);
            con.Open();
            reader = comando.ExecuteReader();

            bool hay = reader.Read();
            if (hay)
            {
                dat.Nombre = reader[1].ToString();
                dat.Apellido = reader[2].ToString();
                //listdat.Add(dat);
            }
            else
            {
                dat.Nombre = "No hay resultado";
                dat.Apellido = "";
                //listdat.Add(dat);
            }
            return dat;
        }

        public  Datos Crear(string nombre, string apellido, string dni)
        {
            Datos dat = new Datos();
            con = new SqlConnection("Server=tcp:buscardnidbserver.database.windows.net,1433;Initial Catalog=BuscarDni_db;Persist Security Info=False;" +
               "User ID=jeanpierre;Password=Jean2551;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            string fd = "Insert into Datos(Nombre, Apellido, DNI) values('"+nombre+"','"+apellido+"','"+dni+"')";
            comando = new SqlCommand(fd, con);
            con.Open();
            comando.ExecuteNonQuery();
            con.Close();
            return dat;
        }
    }
}
