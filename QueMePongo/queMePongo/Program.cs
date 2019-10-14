using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using queMePongo.Repositories;

namespace QueMePongo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DB())
            {

                var UsuarioRepo = new UsuarioRepository();
                var GuardarropaRepo = new GuardarropaRepository();
                var TelaRepo = new TelaRepository();
                var TipoPrendaRepo = new TipoPrendaRepository();
                var AtuendoRepo = new AtuendoRepository();
                var EventoRepo = new EventoRepository();
                var PrendasRepo = new PrendaRepository();

                context.limpiarDB();

                var telaDePrueba = new Tela();
                telaDePrueba.descripcion = "Seda";
                TelaRepo.Insert(telaDePrueba, context);

                TipoPrenda tp1 = new TipoPrenda();
                tp1.descripcion = "jean";
                tp1.categoria = "torso";
                tp1.tiposDeTelaPosibles.Add("algodon");
                tp1.tiposDeTelaPosibles.Add("lana");
                tp1.nivelDeAbrigo = 8;
                tp1.capa = 1;

                TipoPrenda tp2 = new TipoPrenda();
                tp2.descripcion = "joguin";
                tp2.categoria = "torso";
                tp2.tiposDeTelaPosibles.Add("algodon");
                tp2.tiposDeTelaPosibles.Add("lana");
                tp2.nivelDeAbrigo = 10;
                tp2.capa = 0;

                TipoPrendaRepo.Insert(tp1, context);
                TipoPrendaRepo.Insert(tp2, context);

                var tela = context.telas.Single(u => u.id_tela == 17);

                var usuario1 = new Usuario("usuario1", new Gratuito(), "pass");
                UsuarioRepo.Insert(usuario1, context);
                var user = context.usuarios.Single(u => u.usuario == "usuario1");


                Guardarropa guardarropa = new Guardarropa(usuario1, "Guardarropa1");
                GuardarropaRepo.Insert(guardarropa, context,user.id_usuario);
                var guard = context.guardarropas.Single(u => u.nombreGuardarropas == "Guardarropa1");


                Prenda p = new Prenda(tp1, tela, "rojo", "amarillo");
                PrendasRepo.Insert(p, context, guard.id_guardarropa);


                var evento = new Evento("UTN", "EntregaTP", usuario1, new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), "evento1", 1);
                //Falta crear un atuendo y asignarle el id al atributo id_atuendo de evento
                EventoRepo.Insert(evento, context);

                Helper sist = new Helper();
                Usuario user2 =new Usuario();

                user2 = sist.loguing("usuario1", "pass");
                Console.WriteLine(user2.id_usuario);
                Console.WriteLine(user2.usuario);
                Console.WriteLine(user2.guardarropas[0].nombreGuardarropas);
                Console.WriteLine(user2.guardarropas[0].prendas[0].id_prenda);
                Console.WriteLine(user2.eventos[0].lugar);

                UsuarioRepo.Delete(user2,context);

                var usuarios = context.consultarUsuarios();
                var guardarropas = context.consultarGuardarropas();
                var eventos = context.consultarEventos();
                var prendas = context.consultarPrendas();
                var atuendos = context.consultarAtuendos();
                var tipoprendas = context.consultarTipoPrendas();
                var telas = context.consultarTelas();

                Console.WriteLine($"Existen {usuarios.Length} usuario(s).");
                Console.WriteLine($"Existen {guardarropas.Length} guardarropa(s).");
                Console.WriteLine($"Existen {eventos.Length} evento(s).");
                Console.WriteLine($"Existen {prendas.Length} prenda(s).");
                Console.WriteLine($"Existen {atuendos.Length} atuendo(s).");
                Console.WriteLine($"Existen {tipoprendas.Length} tipo(s) de prenda(s).");
                Console.WriteLine($"Existen {telas.Length} telas(s).");
            }
        }
    }
}