using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    public class Atuendo
    {
        public List<Prenda> prendas = new List<Prenda>();

        public int id_atuendo { get; set; }

        public int puntuacion { get; set; }

        public bool Igual(Atuendo atuendo)
        {
            if (prendas.Count != atuendo.prendas.Count)
                return false;
            for (int i = 0; i < prendas.Count; i++)
                if (!prendas[i].Igual(atuendo.prendas[i]))
                    return false;
            return true;
        }

        public bool validarAtuendo(Evento even)
        {
            foreach (Prenda p in prendas)
            {
                if (p.validarFechas(even) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}