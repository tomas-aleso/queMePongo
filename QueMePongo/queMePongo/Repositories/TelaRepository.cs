using System;
using QueMePongo;

namespace queMePongo.Repositories
{
    public class TelaRepository
    {
        public void Insert(Tela tela, DB context)
        {
            context.telas.Add(tela);
            context.SaveChanges();
            Console.WriteLine($"\nTela {tela.id_tela} - {tela.descripcion} creado!");
        }

        public void Update(Tela tela, DB context)
        {

        }

        public void Delete(int telaId)
        {

        }
    }
}
