using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queMePongo.Repositories
{
    [Table("prendaxatuendo")]
    public class prendaXatuendoRepository
    {
        [Key]
        [Column("id_prendaxatuendo")]
        public int id_prendaXatuendo { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("id_prenda")]
        public int id_prenda { get; set; }
        public prendaXatuendoRepository() { }
    }
}
