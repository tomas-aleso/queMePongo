using System;
using QueMePongo;

namespace queMePongo.Repositories
{
    public class TipoPrendaRepository
    {
        public void Insert(TipoPrenda tipoPrenda, DB context)
        {
            context.tipoprendas.Add(tipoPrenda);
            context.SaveChanges();
            Console.WriteLine($"\nTipo de prenda {tipoPrenda} creado!");
        }

        public void Update(TipoPrenda tipoPrenda, DB context)
        {

        }

        public void Delete(int tipoprendaId)
        {

        }
    }
}
