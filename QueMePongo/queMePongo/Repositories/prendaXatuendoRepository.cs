using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queMePongo.Repositories
{
    [Table("prendaXatuendo")]
    public class prendaXatuendoRepository
    {
        [Key]
        [Column("id_prendaXatuendo")]
        public int id_prendaXatuendo { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("id_prenda")]
        public int id_prenda { get; set; }
        public prendaXatuendoRepository() { }
    }
}
