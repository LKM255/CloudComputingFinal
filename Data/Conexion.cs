using CloudComputingFinal.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                dat.Nomber = reader[1].ToString();
                dat.Apellido = reader[2].ToString();
                //listdat.Add(dat);
            }
            else
            {
                dat.Nomber = "No hay resultado";
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

        private readonly string URL = "https://webapidni20211112175143.azurewebsites.net/api/Person";

        public async Task<Datos> GetPerson(string dni)
        {
            Datos dt = new Datos();
            using(var httpcliente = new HttpClient())
            {
                string url = URL + "/"+dni;
                HttpResponseMessage response = await httpcliente.GetAsync(url);
                string airresponse = await response.Content.ReadAsStringAsync();
                dt = JsonConvert.DeserializeObject<Datos>(airresponse);
                if(dt.DNI == null)
                {
                    dt.Nomber = "Datos no encontrados";
                    dt.Apellido = "";
                }
            }
            return dt;
        }
        public async Task CrearPersona(string nomber, string apellido, string dni)
        {
            using (var httpcliente = new HttpClient())
            {
                Person per = new Person();
                per.Nomber = nomber;
                per.Apellido = apellido;
                per.DNI = dni;
                string url = URL;
                StringContent content = new StringContent(JsonConvert.SerializeObject(per), Encoding.UTF8,"application/json");
                HttpResponseMessage response = await httpcliente.PostAsync(url,content);
                string airresponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
