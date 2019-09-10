using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    public class Helper
    {
        List<Usuario> usuarios = new List<Usuario>();
        public List<TipoPrenda> tipoDePrenda = new List<TipoPrenda>();
        public int capacidadMaxGratuito = 200;
        public int capacidadMaxPremium = -1;

        public void eliminarUsuario(String usuario)
        {

            foreach (Usuario a in usuarios)
            {
                if (usuario == a.usuario)
                {
                    usuarios.Remove(a);
                    Console.WriteLine("Usuario eliminado");
                    break;
                }
            }
        }

        public Usuario crearUsuario(String nombre)
        {
            Gratuito user = new Gratuito();
            Usuario value = new Usuario(nombre, user);
            usuarios.Add(value);
            Console.WriteLine("Usuario creado");
            return value;
        }

        public List<TipoPrenda> levantarJSon()
        {
            try
            {
                var json = System.IO.File.ReadAllText(@"../../datos.json");
                List<TipoPrenda> listaPrendas = JsonConvert.DeserializeObject<JObject>(json).Value<JArray>("tipoDePrenda").ToObject<List<TipoPrenda>>();
                return listaPrendas;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR AL ABRIR Y CARGAR JSON");
                return null;
            }

        }
    }
}