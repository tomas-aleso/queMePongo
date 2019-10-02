using System;
using QueMePongo;

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

        public void Delete(int atuendoId)
        {

        }
    }
}
