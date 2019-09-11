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
                var tipoprendas = context.tipoprendas.ToArray();
                var telas = context.telas.ToArray();

                Console.WriteLine($"Existen {usuarios.Length} usuario(s).");
                Console.WriteLine($"Existen {guardarropas.Length} guardarropa(s).");
                Console.WriteLine($"Existen {eventos.Length} evento(s).");
                Console.WriteLine($"Existen {prendas.Length} prenda(s).");
                Console.WriteLine($"Existen {atuendos.Length} atuendo(s).");
                Console.WriteLine($"Existen {tipoprendas.Length} tipo(s) de prenda(s).");
                Console.WriteLine($"Existen {telas.Length} telas(s).");

                //Creo un usuario
                /*var ale = new Usuario("achani", new Gratuito());
                ale.contrasenia = "asd123";

                //Lo guardo en la base de datos
                context.usuarios.Add(ale);
                context.SaveChanges();

                Console.WriteLine($"Usuario {ale.usuario} creado!");
                usuarios = context.usuarios.ToArray();
                Console.WriteLine($"Ahora hay {usuarios.Length} usuario(s).");*/

                Console.ReadLine();

            }
        }
    }
}