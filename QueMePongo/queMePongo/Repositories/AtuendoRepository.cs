using System;
using QueMePongo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    public class AtuendoRepository
    {
        public void Insert(Atuendo atuendo,Evento even, DB context)
        {
            context.atuendos.Add(atuendo);
            context.SaveChanges();
            sugerenciaXeventoRepository gur = new sugerenciaXeventoRepository();
            gur.id_atuendo = atuendo.id_atuendo;
            gur.id_evento = even.id_evento;
            context.sugerenciaXeventoRepositories.Add(gur);
            foreach(Prenda p in atuendo.prendas)
            {
                prendaXatuendoRepository par = new prendaXatuendoRepository();
                par.id_atuendo = atuendo.id_atuendo;
                par.id_prenda = p.id_prenda;
            }
            context.SaveChanges();
        }

        public void Delete(int atuendoId, DB context)
        {
            Atuendo g = new Atuendo();
            g = context.atuendos.Single(b => b.id_atuendo == atuendoId);
            List<prendaXatuendoRepository> gur = new List<prendaXatuendoRepository>();
            gur = context.prendaXatuendoRepositories.Where(u => u.id_atuendo == atuendoId).ToList();
            foreach(prendaXatuendoRepository gu in gur)
            {
                context.prendaXatuendoRepositories.Remove(gu);
            }
            context.atuendos.Remove(g);
            context.SaveChanges();
        }

        public Atuendo loguing(int atuendoId, DB context)
        {
            Atuendo g = new Atuendo();
            List<prendaXatuendoRepository> gur = new List<prendaXatuendoRepository>();
            gur = context.prendaXatuendoRepositories.Where(u => u.id_atuendo == atuendoId).ToList();
            foreach(prendaXatuendoRepository p in gur)
            {
                PrendaRepository per = new PrendaRepository();
                g.prendas.Add(per.loguing(p.id_prenda,context));
            }
            return g;
        }
    }
}
