using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;

namespace QueMePongo
{
    public class Helper
    {
        List<Usuario> usuarios = new List<Usuario>();
        public List<TipoPrenda> tipoDePrenda = new List<TipoPrenda>();
        public int capacidadMaxGratuito = 200;
        public int capacidadMaxPremium = -1;
        Gratuito gratuito = new Gratuito();
        Premium premium = new Premium();


        public Usuario loguing(String nombreUser,String contrasenia)
        {
            DB context = new DB();
            UsuarioRepository usr = new UsuarioRepository();
            Usuario u=usr.verificarLoguing(nombreUser, contrasenia, context);
            if(u==null)
            {
                Console.WriteLine("Error en el logueo (credenciales equivocadas o su usuario no existe)");
                return null;
            }
            else
            {
                u.guardarropas = usr.loguingGuardarropas(u.id_usuario, context);
                u.eventos = usr.loguingEvento(u.id_usuario, context);
                if(u.tipoDeUsuario==0)
                {
                    u.tipoUsuario = new Gratuito();
                }
                else
                {
                    u.tipoUsuario = new Premium();
                }
                Scheduler sched = Scheduler.getInstance();
                sched.run();
                foreach (Evento ev in u.eventos)
                {
                    ev.user = u;
                    String nombre = null;
                    nombre= u.usuario + ev.descripcion;
                    sched.crearSchedulerEvento(nombre, ev.tipoEvento, ev.fechaNotificacion, ev);
                }
                return u;
            }
        }

        public void eliminarUsuario(String usuario)
        {
            DB context = new DB();
            UsuarioRepository usr = new UsuarioRepository();
            foreach (Usuario a in usuarios)
            {
                if (usuario == a.usuario)
                {
                    usr.Delete(a, context);
                    usuarios.Remove(a);
                    Console.WriteLine("Usuario eliminado");
                    break;
                }
            }
        }

        public Usuario crearUsuario(String nombre)
        {
            DB context = new DB();
            UsuarioRepository usr = new UsuarioRepository();
            Usuario value = new Usuario(nombre, gratuito, "contrasenia");
            usr.Insert(value, context);
            usuarios.Add(value);
            Console.WriteLine("Usuario creado");
            return value;
        }

        public void upgradeUsuario(Usuario usr)
        {
            usr.modificarTipo(premium);
        }

        public void downgradeUsuario(Usuario usr)
        {
            usr.modificarTipo(gratuito);
        }

        public List<TipoPrenda> levantarJSon()
        {
            try
            {
                var json = System.IO.File.ReadAllText(@"../../datos.json");
                List<TipoPrenda> listaPrendas = JsonConvert.DeserializeObject<JObject>(json).Value<JArray>("tipoDePrenda").ToObject<List<TipoPrenda>>();
                TipoPrendaRepository tpr = new TipoPrendaRepository();
                DB context = new DB();
                foreach (TipoPrenda p in listaPrendas)
                {
                    tpr.Insert(p, context);
                }
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