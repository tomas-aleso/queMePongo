using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queMePongo.Repositories
{
    [Table("guardarropaxprenda")]
    public class guardarropaXprendaRepository
    {
        [Key]
        [Column("id_guardarropaxprenda")]
        public int id_guardarropaXprenda { get; set; }

        [Column("id_prenda")]
        public int id_prenda { get; set; }

        [Column("id_guardarropa")]
        public int id_guardarropa { get; set; }
        public guardarropaXprendaRepository() { }
    }
}
