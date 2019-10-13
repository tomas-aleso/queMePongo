using System;
using QueMePongo;
using System.Collections.Generic;
using System.Linq;

namespace queMePongo.Repositories
{
    public class TipoPrendaRepository
    {
        public void Insert(TipoPrenda tipoPrenda, DB context)
        {
            if(context.tipoprendas.Any(c => c.descripcion == tipoPrenda.descripcion))
            {}
            else
            {
                context.tipoprendas.Add(tipoPrenda);
                context.SaveChanges();
                int idPrenda = tipoPrenda.id_tipoPrenda;
                foreach (String s in tipoPrenda.tiposDeTelaPosibles)
                {
                    Tela t = new Tela();
                    t.descripcion = s;
                    TelaRepository tr = new TelaRepository();
                    telaXtipoPrendaRepository ttpr = new telaXtipoPrendaRepository();
                    ttpr.id_tela= tr.Insert(t, context);
                    ttpr.id_tipoprenda = idPrenda;
                    context.telaXtipoPrendaRepositories.Add(ttpr);
                    context.SaveChanges();
                }
            }
        }

        public TipoPrenda loguing(int tpid, DB context)
        {
            TipoPrenda tp = new TipoPrenda();
            tp = context.tipoprendas.Single(u => u.id_tipoPrenda == tpid);
            List<telaXtipoPrendaRepository> ttpr = new List<telaXtipoPrendaRepository>();
            ttpr = context.telaXtipoPrendaRepositories.Where(u => u.id_tipoprenda == tpid).ToList();
            foreach (telaXtipoPrendaRepository a in ttpr)
            {
                tp.tiposDeTelaPosibles.Add((context.telas.Single(u => u.id_tela == a.id_tela)).descripcion);
            }
            return tp;
        }
    }
}
