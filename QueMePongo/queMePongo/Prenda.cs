using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("prendas")]
    public class Prenda
    {
        [Key]
        [Column("id_prenda")]
        public int id_prenda { get; set; }

        public TipoPrenda tipo;

        public Guardarropa guardarropa;

        public Atuendo atuendo;

        [Column("id_tipoprenda")]
        public int tipoPrenda { get; set; }

        [Column("id_tela")]
        public int id_tela { get; set; }

        public String tela;

        [Column("estahabilitada")]
        public bool estaHabilitada;

        [Column("colorprincipal")]
        public String colorPrincipal { get; set; }

        [Column("colorsecundario")]
        public String colorSecundario { get; set; }

        public List<Evento> eventos = new List<Evento>();

        public Prenda() { }

        public Prenda(TipoPrenda tipoP, String tel, String cp, String cs)
        {
            if (cp == cs) throw new ArgumentException("el color principal no puede ser igual que el secundario");
            tipo = tipoP;
            tela = tel;
            colorPrincipal = cp;
            colorSecundario = cs;
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