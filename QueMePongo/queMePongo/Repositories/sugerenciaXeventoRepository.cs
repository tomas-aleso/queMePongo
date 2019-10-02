using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace queMePongo.Repositories
{
    [Table("sugerenciasxevento")]
    public class sugerenciaXeventoRepository
    {
        [Key]
        [Column("id_sugerenciaxevento")]
        public int id_sugerenciaxevento { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("id_evento")]
        public int id_evento { get; set; }
        public sugerenciaXeventoRepository() { }
    }
}
