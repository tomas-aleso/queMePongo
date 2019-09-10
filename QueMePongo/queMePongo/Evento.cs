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

        public DateTime fechaNotificacion { get; set; }

        public DateTime fechaInicioPrendas { get; set; }

        public DateTime fechaFinPrendas { get; set; }

        public bool yaSeEjecuto { get; set; }

        public String lugar { get; set; }

        public int id_atuendo { get; set; }

        public String descripcion { get; set; }

        public int id_usuario { get; set; }

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

        public void ejecutarEvento()
        {
            user.elegirAtuendo(this);
        }
    }
}