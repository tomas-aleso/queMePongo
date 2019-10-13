using System;
using QueMePongo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    public class EventoRepository
    {
        public void Insert(Evento evento, DB context)
        {
            context.eventos.Add(evento);
            context.SaveChanges();
        }

        public void Update(Evento evento, DB context)
        {
            var s= context.eventos.Single(b => b.id_evento == evento.id_evento);
            s.id_atuendo =evento.id_atuendo;
            context.SaveChanges();
        }

        public void Delete(int eventooId, DB context)
        {
            Evento g = new Evento();
            g = context.eventos.Single(b => b.id_evento == eventooId);
            context.eventos.Remove(g);
            context.SaveChanges();
        }

        public void InsertSugerencias(Evento evento,List<Atuendo> atuendos, DB context)
        {
            foreach(Atuendo a in atuendos)
            {
                sugerenciaXeventoRepository ser = new sugerenciaXeventoRepository();
                ser.id_atuendo = a.id_atuendo;
                ser.id_evento = evento.id_evento;
                context.sugerenciaXeventoRepositories.Add(ser);
                context.SaveChanges();
            }

        }
    }
}
