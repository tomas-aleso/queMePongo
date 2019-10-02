using System;
using QueMePongo;
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
                int idPrenda = context.tipoprendas.Single(b => b.descripcion == tipoPrenda.descripcion).id_tipoPrenda;
                foreach (String s in tipoPrenda.tiposDeTelaPosibles)
                {
                    Tela t = new Tela();
                    t.descripcion = s;
                    TelaRepository tr = new TelaRepository();
                    telaXtipoPrendaRepository ttpr = new telaXtipoPrendaRepository();
                    ttpr.id_tela= tr.Insert(t, context);
                    ttpr.id_tipoprenda = idPrenda;
                    context.telaXtipoPrendaRepositories.Add(ttpr);
                }
            }
        }
    }
}
