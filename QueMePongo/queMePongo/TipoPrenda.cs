using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("tiposDePrenda")]
    public class TipoPrenda
    {
        [Key]
        [Column("tipoPrenda_id")]
        public int tipoPrenda_id { get; set; }//TODO: Avisar que falta aclarar que esto es la PK en el DER
                                              //TODO: Avisar que se tiene que agregar este atributo

        [Column("descripcion")]
        public String descripcion { get; set; }

        [Column("categoria")]
        public String categoria { get; set; }

        public List<String> tiposDeTelaPosibles = new List<String>();//TODO: Avisar que esto lo tenemos que partir en el DER y crear 2 tablas mas "tipoDeTela" y "tipoDeTela x tipoDePrenda"

        [Column("nivelDeAbrigo")]
        public int nivelDeAbrigo { get; set; }

        [Column("capa")]
        public int capa { get; set; }//TODO: Avisar que falta este atributo en el DER
    }
}