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

                var usuarios = context.consultarUsuarios();
                var guardarropas = context.consultarGuardarropas();
                var eventos = context.consultarEventos();
                var prendas = context.consultarPrendas();
                var atuendos = context.consultarAtuendos();
                //var tipoprendas = context.consultarTipoPrendas();
                var telas = context.consultarTelas();

                /*Console.WriteLine($"Existen {usuarios.Length} usuario(s).");
                Console.WriteLine($"Existen {guardarropas.Length} guardarropa(s).");
                Console.WriteLine($"Existen {eventos.Length} evento(s).");
                Console.WriteLine($"Existen {prendas.Length} prenda(s).");
                Console.WriteLine($"Existen {atuendos.Length} atuendo(s).");
                Console.WriteLine($"Existen {tipoprendas.Length} tipo(s) de prenda(s).");
                Console.WriteLine($"Existen {telas.Length} telas(s).");*/

                //Creo un usuario
                var usuario1 = new Usuario("usuario1", new Gratuito(), "pass");
                UsuarioRepo.Insert(usuario1, context);

                //Agrego un usuario con guardarropa
                var usuario2ConGuardarropa = new Usuario("usuario2", new Gratuito(), "pass");
                Guardarropa guardarropa = new Guardarropa(usuario2ConGuardarropa, "Guardarropa1");
                usuario2ConGuardarropa.guardarropas = (List<Guardarropa>)new List<Guardarropa> { guardarropa };
                UsuarioRepo.Insert(usuario2ConGuardarropa, context);

                //Agrego el guardarropa al usuario2 (Aca nose si esta andando bien o mal, cuando liste los guardarropas de este usuario, me deberían aparecer 1 o 2 guardarropas?
                var nuevoGuardarropa = new Guardarropa(usuario2ConGuardarropa, "Guardarropa2");
                GuardarropaRepo.Insert(nuevoGuardarropa, context);
    
                //Consulto los guardarropas de ese usuario
                var usuarioConsultaGuardarropa = context.usuarios.Single(x => x.usuario == "usuario2");

                Console.WriteLine($"\nGuardarropas del usuario {usuarioConsultaGuardarropa.usuario}:");
                foreach (Guardarropa g in usuarioConsultaGuardarropa.guardarropas)
                {
                    Console.WriteLine($"{g.id_guardarropa} - {g.nombreGuardarropas}");
                }

                var tela = new Tela("Algodon");
                TelaRepo.Insert(tela, context);

                var tipoPrenda = new TipoPrenda();
                TipoPrendaRepo.Insert(tipoPrenda, context);

                //Creo una prenda TODO: No anda (ERROR: insert or update on table "prendas" violates foreign key constraint "prenda_tipoprenda_prenda_id")
                /*var prenda = new Prenda(new TipoPrenda(), "Algodon", "Blanco", "Negro");
                prenda.tipoPrenda = tipoPrenda.id_tipoPrenda;
                //prenda.tipoPrenda = 1;
                prenda.id_tela = tela.id_tela;
                context.prendas.Add(prenda);
                context.SaveChanges();
                Console.WriteLine($"\nPrenda {prenda.id_prenda} creada!");*/

                //Creo un atuendo
                var atuendo = new Atuendo();
                AtuendoRepo.Insert(atuendo, context);

                //Creo un evento
                var evento = new Evento("UTN", "EntregaTP", usuario1, new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), "evento1", 1);
                evento.id_atuendo = atuendo.id_atuendo;
                EventoRepo.Insert(evento, context);

                UsuarioRepo.Delete(usuario1.id_usuario, context);

            }
        }
    }
}