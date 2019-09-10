using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    public class Prenda
    {
        public int id_prenda { get; set; }

        public TipoPrenda tipo;

        public int tipoPrenda { get; set; }

        public int tela_id { get; set; }

        public String tela;

        public String colorPrincipal { get; set; }

        public String colorSecundario { get; set; }

        public List<Evento> eventos = new List<Evento>();

        public int calificacion;
        public int cantCalif;

        public Prenda(TipoPrenda tipoP, String tel, String cp, String cs)
        {
            if (cp == cs) throw new ArgumentException("el color principal no puede ser igual que el secundario");
            tipo = tipoP;
            tela = tel;
            colorPrincipal = cp;
            colorSecundario = cs;
            calificacion = 0;
            cantCalif = 0;
        }

        public void calificar(int calif)
        {
            calificacion += calif;
            cantCalif++;
        }

        public float getCalif()
        {
            return (float)calificacion / (float)cantCalif;
        }

        public bool Igual(Prenda prenda)
        {
            return tipo.Equals(prenda.tipo) && tela == prenda.tela && colorPrincipal == prenda.colorPrincipal && colorSecundario == prenda.colorSecundario;
        }

        public bool validarFechas(Evento even)
        {
            for (int i = 0; i < eventos.Count; i++)
            {
                if (even.fechaFinPrendas < eventos[i].fechaInicioPrendas)
                {
                    return true;
                }
                else
                {
                    if (even.fechaInicioPrendas > eventos[i].fechaFinPrendas)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}