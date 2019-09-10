using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DB())
            {
                var cantUsuarios = context.usuarios.ToArray();
                Console.WriteLine($"Existen {cantUsuarios.Length} usuario(s).");
                Console.ReadLine();
            }
        }
    }
}