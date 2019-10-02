using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queMePongo.Repositories
{
    [Table("guardarropaxusuario")]
    public class guardarropaXusuarioRepository
    {
        [Key]
        [Column("guardarropaXusuario_id")]
        public int guardarropaXusuario_id { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("id_guardarropa")]
        public int id_guardarropa { get; set; }
        public guardarropaXusuarioRepository() { }
    }
}
