﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;

namespace QueMePongo
{
    [Table("usuarios")]
    public class Usuario
    {

        [Key]
        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("usuario")]
        public string usuario { get; set; }

        [Column("contrasenia")]
        public string contrasenia { get; set; }

        [Column("tipodeusuario")]
        public int tipoDeUsuario { get; set; }

        [NotMapped]
        public List<Evento> eventosDeUsuario { get; set; }

        public List<Guardarropa> guardarropas = new List<Guardarropa>();

        public List<Evento> eventos = new List<Evento>();

        public virtual ICollection<Guardarropa> Guardarropas { get; set; }

        [NotMapped]
        public TipoUsuario tipoUsuario { get; set; }

        public Usuario(String user, TipoUsuario tu, String pass)
        {
            usuario = user;
            tipoUsuario = tu;
            tipoDeUsuario = tu.Tipo;
            contrasenia = pass;

        }

        public void modificarTipo(TipoUsuario tu)
        {
            this.tipoUsuario = tu;
            tipoDeUsuario = tu.Tipo;
        }

        public Usuario() { }

        public Guardarropa crearGuardarropa(String nombreGuardarropa)
        {
            DB context = new DB();
            GuardarropaRepository gr = new GuardarropaRepository();
            if(gr.existeGuardarropa(nombreGuardarropa,context,id_usuario))
            {
                Console.WriteLine("El guardarropas ya existe");
                return null;
            }
            else
            {
                Guardarropa value = tipoUsuario.crearGuardarropa(nombreGuardarropa, this);
                guardarropas.Add(value);
                gr.Insert(value, context, id_usuario);
                Console.WriteLine("Guardarropas creado");
                return value;
            }
        }

        public void eliminarGuardarropa(String nombreGuardarropa)
        {

            foreach (Guardarropa a in guardarropas)
            {

                if (nombreGuardarropa == a.nombreGuardarropas)
                {
                    DB context = new DB();
                    GuardarropaRepository gr = new GuardarropaRepository();
                    gr.Delete(a.id_guardarropa, context, id_usuario);
                    guardarropas.Remove(a);
                    Console.WriteLine("Guardarropas eliminado");
                    break;
                }
            }

        }

        public void compartirGuardarropa(Usuario usuario, Guardarropa guardarropaACompartir)
        {
            if (Object.ReferenceEquals(this.tipoUsuario.GetType(), usuario.tipoUsuario.GetType()))
            {
                usuario.agregarGaurdarropaCompartido(guardarropaACompartir);
            }
            else
            {
                throw new ArgumentException("los usuarios son de distinto tipo");
            }
        }

        public void agregarGaurdarropaCompartido(Guardarropa guardarropaCompartido)
        {
            guardarropaCompartido.usuariosCompartidos.Add(this);
            guardarropas.Add(guardarropaCompartido);
            DB context = new DB();
            guardarropaXusuarioRepository gur = new guardarropaXusuarioRepository();
            gur.id_guardarropa = guardarropaCompartido.id_guardarropa;
            gur.id_usuario = id_usuario;
            context.guardarropaXusuarioRepositories.Add(gur);
        }

        public List<Atuendo> ObtenerSugerencias(Evento even)
        {
            Apis api = new Apis();
            int temperatura = 20;//api.solicitarClima(even.lugar);
            List<Atuendo> sugerencias = new List<Atuendo>();
            foreach (Guardarropa guardarropa in guardarropas)
            {
                foreach (Atuendo atuendo in guardarropa.generarSugerencias(temperatura, even))
                {
                    sugerencias.Add(atuendo);
                }

            }
            sugerencias = sugerencias.OrderBy(s1 => s1.getPuntuacion()).ToList();
            mostrarAtuendos(sugerencias);
            return sugerencias;
        }

        public void mostrarAtuendos(List<Atuendo> atuendos)
        {

            for (int i = 0; i < atuendos.Count; i++)
            {
                Console.WriteLine("Sugerencia Numero: " + i);
                Console.WriteLine("-------------------------------------------------");
                foreach (Prenda s in atuendos[i].prendas)
                {
                    if (s != null)
                    {
                        Console.WriteLine("descripcion:" + s.tipo.descripcion);
                        Console.WriteLine("categoria:" + s.tipo.categoria);
                        Console.WriteLine("capa:" + s.tipo.capa);
                        Console.WriteLine("tela:" + s.tela);
                        Console.WriteLine("color Principal:" + s.colorPrincipal);
                        Console.WriteLine("color Secundario:" + s.colorSecundario);
                    }
                    Console.WriteLine("-------------------------------------------------");

                }
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("");
            }
        }

        public void crearEvento(DateTime fechaIni, DateTime fechaFinP, DateTime fechaIniP, String lugar, String descripcion, int tipoEvento)
        {
            Evento even = new Evento(lugar, descripcion, this, fechaIni, fechaIniP, fechaFinP, usuario, tipoEvento);
            eventos.Add(even);
            Console.WriteLine("Se ha creado el evento");
        }

        public void eliminarEvento(String lugar)
        {
            foreach (Evento a in eventos)
            {

                if (lugar == a.lugar)
                {
                    eventos.Remove(a);
                    Console.WriteLine("Evento eliminado");
                    break;
                }
            }
        }

        public void elegirAtuendo(Evento even)
        {
            List<Atuendo> atuendos = new List<Atuendo>();
            atuendos = this.ObtenerSugerencias(even);


            /*
            Console.WriteLine("Indique el numero de sugerencia que quiere seleccionar:");

            String sugerenciaElegida = Console.ReadLine();

            int opcion = int.Parse(sugerenciaElegida);

            if (atuendos[opcion].validarAtuendo(even))
            {
                foreach (Prenda p in atuendos[opcion].prendas)
                {
                    p.eventos.Add(even);
                    Console.WriteLine("Ha elegido su atuendo Correctamente");
                    calificarAtuendo(atuendos[opcion]);
                }
            }
            else
            {
                Console.WriteLine("El atuendo que eligio ya esta en uso en ese periodo de tiempo, elija otro");
                this.elegirAtuendo(even);
            }*/

        }

        public void calificarAtuendo(Atuendo atuendo) // no se verifica datos ingresados ya que proximamente se hara con una interfaz
        {
            Console.WriteLine("Desea calificar el atuendo y/n");
            String str = Console.ReadLine();
            if (str == "y") {
                Console.WriteLine("Ingrese puntuacion del 1 al 5");
                String puntuacion = Console.ReadLine();

                int punt = int.Parse(puntuacion);
                atuendo.prendas.ForEach(p => p.calificar(punt));
            }
        }

    }

}