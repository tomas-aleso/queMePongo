using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueMePongo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DB())
            {
                var usuarios = context.usuarios.ToArray();
                var guardarropas = context.guardarropas.ToArray();
                var eventos = context.eventos.ToArray();
                var prendas = context.prendas.ToArray();
                var atuendos = context.atuendos.ToArray();
                //var tipoprendas = context.tipoprendas.ToArray();
                var telas = context.telas.ToArray();

                Console.WriteLine($"Existen {usuarios.Length} usuario(s).");
                Console.WriteLine($"Existen {guardarropas.Length} guardarropa(s).");
                Console.WriteLine($"Existen {eventos.Length} evento(s).");
                Console.WriteLine($"Existen {prendas.Length} prenda(s).");
                Console.WriteLine($"Existen {atuendos.Length} atuendo(s).");
                //Console.WriteLine($"Existen {tipoprendas.Length} tipo(s) de prenda(s).");
                Console.WriteLine($"Existen {telas.Length} telas(s).");

                //Creo un usuario
                var usuario1 = new Usuario("usuario1", new Gratuito());
                usuario1.contrasenia = "contraseniaSegura";
                context.usuarios.Add(usuario1);
                context.SaveChanges();
                Console.WriteLine($"\nUsuario {usuario1.usuario} creado!");
 
                //Agrego un usuario con guardarropa
                var usuario2ConGuardarropa = new Usuario("usuario2", new Gratuito());
                usuario2ConGuardarropa.contrasenia = "otraContrasenia";
                Guardarropa guardarropa = new Guardarropa(usuario2ConGuardarropa, "Guardarropa1");
                usuario2ConGuardarropa.guardarropas = (List<Guardarropa>)new List<Guardarropa> { guardarropa };
                context.usuarios.Add(usuario2ConGuardarropa);
                context.SaveChanges();
                Console.WriteLine($"\nUsuario {usuario2ConGuardarropa.usuario} creado!");

                //Agrego el guardarropa al usuario2 (Aca nose si esta andando bien o mal, cuando liste los guardarropas de este usuario, me deberían aparecer 1 o 2 guardarropas?
                var nuevoGuardarropa = new Guardarropa(usuario2ConGuardarropa, "Guardarropa2");
                context.guardarropas.Add(nuevoGuardarropa);
                context.SaveChanges();
                Console.WriteLine($"\nGuardarropa {nuevoGuardarropa.nombreGuardarropas} creado!");
    
                //Consulto los guardarropas de ese usuario
                var usuarioConsultaGuardarropa = context.usuarios.Single(x => x.usuario == "usuario2");

                Console.WriteLine($"\nGuardarropas del usuario {usuarioConsultaGuardarropa.usuario}:");
                foreach (Guardarropa g in usuarioConsultaGuardarropa.guardarropas)
                {
                    Console.WriteLine($"{g.id_guardarropa} - {g.nombreGuardarropas}");
                }

                /*//Creo una prenda TODO: No anda (ERROR: insert or update on table "prendas" violates foreign key constraint "prenda_tipoprenda_prenda_id")
                var prenda = new Prenda(new TipoPrenda(), "Algodon", "Blanco", "Negro");
                prenda.tipoPrenda = 1;
                prenda.id_tela = 1;
                context.prendas.Add(prenda);
                context.SaveChanges();
                Console.WriteLine($"\nPrenda {prenda.id_prenda} creada!");*/

                //Creo un atuendo
                var atuendo = new Atuendo();
                context.atuendos.Add(atuendo);
                context.SaveChanges();
                Console.WriteLine($"\nAtuendo {atuendo.id_atuendo} creado!");

                //Creo un evento
                var evento = new Evento("UTN", "EntregaTP", usuario1, new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 5, 7, 45, 0), "evento1", 1);
                evento.id_atuendo = atuendo.id_atuendo;
                context.eventos.Add(evento);
                context.SaveChanges();
                Console.WriteLine($"\nEvento {evento.id_evento} creado!");

                var tela = new Tela("Algodon");
                context.telas.Add(tela);
                context.SaveChanges();
                Console.WriteLine($"\nTela {tela.id_tela}: {tela.descripcion} creada!");

                var usuarioParaBorrar = context.usuarios.Single(x => x.usuario == "usuario1");
                context.usuarios.Remove(usuarioParaBorrar);
                context.SaveChanges();

                usuarioParaBorrar = context.usuarios.Single(x => x.usuario == "usuario2");
                context.usuarios.Remove(usuarioParaBorrar);
                context.SaveChanges();

            }
        }
    }
}