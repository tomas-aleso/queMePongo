using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("eventos")]
    public class Evento
    {
        [Key]
        [Column("id_evento")]
        public int id_evento { get; set; }//TODO: Avisar que se tiene que agregar este atributo

        [Column("fechanotificacion")]
        public DateTime fechaNotificacion { get; set; }

        [Column("fechadeinicio")]
        public DateTime fechaInicioPrendas { get; set; }

        [Column("fechafinal")]
        public DateTime fechaFinPrendas { get; set; }

        [NotMapped]
        public bool yaSeEjecuto { get; set; }

        [Column("lugar")]
        public String lugar { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("descripcion")]
        public String descripcion { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [NotMapped]
        public Usuario user { get; set; }
        Atuendo atuendo;

        public Evento(String lug, String descript, Usuario u, DateTime fechaIni, DateTime fechaIniPrendas, DateTime fechaFinPrenda, String nombre, int tipoEvento)
        {
            lugar = lug;
            descripcion = descript;
            user = u;
            fechaNotificacion = fechaIni;
            fechaInicioPrendas = fechaIniPrendas;
            fechaFinPrendas = fechaFinPrenda;
            Scheduler sched = Scheduler.getInstance();
            sched.run();
            nombre = nombre + descript;
            sched.crearSchedulerEvento(nombre, tipoEvento, fechaIni, this);
        }

        public Evento() { }

        public void ejecutarEvento()
        {
            user.elegirAtuendo(this);
        }
    }
}