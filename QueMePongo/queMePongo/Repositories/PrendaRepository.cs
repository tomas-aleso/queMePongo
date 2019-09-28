using System;
using QueMePongo;

namespace queMePongo.Repositories
{
    public class PrendaRepository
    {
        public void Insert(Prenda prenda, DB context)
        {
            context.prendas.Add(prenda);
            context.SaveChanges();
            Console.WriteLine($"\nPrenda {prenda.id_prenda} creada!");
        }

        public void Update(Prenda prenda, DB context)
        {

        }

        public void Delete(int prendaId)
        {

        }
    }
}
