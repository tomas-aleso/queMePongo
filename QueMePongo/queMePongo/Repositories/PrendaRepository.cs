using System;
using QueMePongo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    public class PrendaRepository
    {
        public void Insert(Prenda prenda, DB context, int idGuardarropa)
        {
            context.prendas.Add(prenda);
            context.SaveChanges();
            guardarropaXprendaRepository gpr = new guardarropaXprendaRepository();
            gpr.id_guardarropa = idGuardarropa;
            gpr.id_prenda = prenda.id_prenda;
            context.guardarropaXprendaRepositories.Add(gpr);
            context.SaveChanges();
        }

        public void Update(Prenda prenda, DB context)
        {
            var s = context.prendas.Single(b => b.id_prenda == prenda.id_prenda);
            s.calificacion = prenda.calificacion;
            s.cantCalif = prenda.cantCalif;
            context.SaveChanges();
        }
        public void Delete(int prendaId, DB context)
        {
            Prenda g = new Prenda();
            g = context.prendas.Single(b => b.id_prenda == prendaId);
            List<guardarropaXprendaRepository> gpr = new List<guardarropaXprendaRepository>();
            gpr = context.guardarropaXprendaRepositories.Where(u => u.id_prenda == prendaId).ToList();
            foreach (guardarropaXprendaRepository a in gpr)
            {
                context.guardarropaXprendaRepositories.Remove(a);
            }
            context.prendas.Remove(g);
            context.SaveChanges();
        }

        public Prenda loguing(int prendaId, DB context)
        {
            Prenda g = new Prenda();
            g = context.prendas.Single(b => b.id_prenda == prendaId);
            var eventos = context.consultarEventos();
            foreach(Evento ev in eventos)
            {
                if(ev.id_atuendo!=-1)
                {
                    List<prendaXatuendoRepository> gur = new List<prendaXatuendoRepository>();
                    gur = context.prendaXatuendoRepositories.Where(u => u.id_atuendo == ev.id_atuendo).ToList();
                    foreach (prendaXatuendoRepository gu in gur)
                    {
                       if(gu.id_prenda== prendaId)
                       {
                            g.eventos.Add(ev);
                            break;
                       }
                    }
                }
            }
            TipoPrendaRepository tpr = new TipoPrendaRepository();
            g.tipo = tpr.loguing(g.tipoPrenda, context);
            g.tela = (context.telas.Single(b => b.id_tela == g.id_tela)).descripcion;
            return g;
        }
    }
}
