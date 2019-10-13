using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;

namespace QueMePongo
{
    [Table("guardarropas")]
    public class Guardarropa
    {
        [Key]
        [Column("id_guardarropa")]
        public int id_guardarropa { get; set; }

        [Column("nombre")]
        public String nombreGuardarropas { get; set; }

        [Column("id_duenio")]
        public int duenio { get; set; }

        //public virtual ICollection<Usuario> Usuarios { get; set; }

        //public virtual ICollection<Prenda> Prendas { get; set; }

        public List<Prenda> prendas = new List<Prenda>();

        public List<int> usuariosCompartidos = new List<int>();

        public Guardarropa(Usuario user, String nombreGuardarropa)
        {
            usuariosCompartidos = null;
            nombreGuardarropas = nombreGuardarropa;
            duenio = user.id_usuario;
        }

        public Guardarropa() { }

        public void crearPrenda(TipoPrenda tipoDePrenda, Tela tela, String colorPrincipal, String colorSecundario, Usuario user)
        {
            if (cumpleRequisitos(tipoDePrenda, tela.descripcion, colorPrincipal, colorSecundario, user))
            {
                DB context = new DB();
                PrendaRepository pr = new PrendaRepository();
                Prenda value = new Prenda(tipoDePrenda, tela, colorPrincipal, colorSecundario);
                pr.Insert(value,context,id_guardarropa);
                prendas.Add(value);
                Console.WriteLine("Prenda creada");
            }
            else
            {
                Console.WriteLine("La prenda no cumple requisitos");
            }

        }

        private bool cumpleRequisitos(TipoPrenda tipoDePrenda, String tela, String colorPrincipal, String colorSecundario, Usuario usuario)
        {
            if (usuario.tipoUsuario.topePrendasPorGuardarropa() < 0)
            {
                return (tipoDePrenda.tiposDeTelaPosibles.Find(item => item == tela) != null && colorPrincipal != null && colorPrincipal != colorSecundario);
            }
            else
            {
                if (prendas.Count() < usuario.tipoUsuario.topePrendasPorGuardarropa())
                {
                    return (tipoDePrenda.tiposDeTelaPosibles.Find(item => item == tela) != null && colorPrincipal != null && colorPrincipal != colorSecundario);
                }
                else
                {
                    return false;
                }
            }

        }

        public void eliminarPrenda(Prenda prenda)
        {

            foreach (Prenda a in prendas)
            {
                if (prenda == a)
                {
                    DB context = new DB();
                    PrendaRepository gr = new PrendaRepository();
                    gr.Delete(a.id_prenda, context);
                    prendas.Remove(a);
                    Console.WriteLine("Prenda eliminada");
                    break;
                }
            }

        }

        public List<Atuendo> generarSugerencias(int temperatura, Evento even)
        {
            GenerarSugerencias g = new GenerarSugerencias();
            List<Atuendo> sugerencias = new List<Atuendo>();
            sugerencias = g.ejecutarGenerar(temperatura, prendas, even);
            return sugerencias;
        }
    }


}
