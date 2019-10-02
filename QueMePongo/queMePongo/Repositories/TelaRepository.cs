using System;
using QueMePongo;
using System.Linq;

namespace queMePongo.Repositories
{
    public class TelaRepository
    {
        public int Insert(Tela tela, DB context)
        {
            if (context.telas.Any(c => c.descripcion == tela.descripcion))
            { }
            else
            {
                context.telas.Add(tela);
                context.SaveChanges();
            }
            return (context.telas.Single(b => b.descripcion == tela.descripcion)).id_tela;
        }
    }
}
