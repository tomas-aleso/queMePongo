﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using queMePongo.Repositories;
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

        [Column("id_tipoprenda")]
        public int tipoPrenda { get; set; }

        [Column("id_tela")]
        public int id_tela { get; set; }

        [Column("colorprincipal")]
        public String colorPrincipal { get; set; }

        [Column("colorsecundario")]
        public String colorSecundario { get; set; }
        
        [Column("calificacion")]
        public int calificacion { get; set; }

        [Column("cantcalif")]
        public int cantCalif { get; set; }

        public List<Evento> eventos = new List<Evento>();

        public Prenda() { }

        public String tela;

        [NotMapped]
        public Tela Tela { get; set; }

        [NotMapped]
        public TipoPrenda tipo;

        [NotMapped]
        public guardarropaXprendaRepository gxp { get; set; }

        public Prenda(TipoPrenda tipoP, Tela tel, String cp, String cs)
        {
            if (cp == cs) throw new ArgumentException("el color principal no puede ser igual que el secundario");
            tipo = tipoP;
            tipoPrenda = tipoP.id_tipoPrenda;
            tela = tel.descripcion;
            id_tela =tel.id_tela;
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