using System;
using QueMePongo;

namespace queMePongo.Repositories
{
    public class GuardarropaRepository
    {
        public void Insert(Guardarropa guardarropa, DB context)
        {
            context.guardarropas.Add(guardarropa);
            context.SaveChanges();
            Console.WriteLine($"\nGuardarropa {guardarropa} creado!");
        }

        public void Update(Guardarropa guardarropa, DB context)
        {

        }

        public void Delete(int guardarropaId)
        {

        }
    }
}