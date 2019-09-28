using System;
using QueMePongo;
namespace queMePongo.Repositories
{
    public class EventoRepository
    {
        public void Insert(Evento evento, DB context)
        {
            context.eventos.Add(evento);
            context.SaveChanges();
            Console.WriteLine($"\nEvento {evento} creado!");
        }

        public void Update(Evento evento, DB context)
        {

        }

        public void Delete(int eventooId)
        {

        }
    }
}
