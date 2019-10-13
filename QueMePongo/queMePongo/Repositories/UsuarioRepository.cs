using System;
using QueMePongo;
using System.Collections.Generic;
using System.Linq;

namespace queMePongo.Repositories
{
    public class UsuarioRepository
    {
        public void Insert(Usuario usuario, DB context)
        {
            context.usuarios.Add(usuario);
            context.SaveChanges();
        }

        public void Update(Usuario usuario, DB context)
        {
            var s = context.usuarios.Single(b => b.id_usuario == usuario.id_usuario);
            s.tipoDeUsuario = usuario.tipoDeUsuario;
            context.SaveChanges();
        }

        public void Delete(Usuario usuario, DB context)
        {
            var usuarioParaBorrar = context.usuarios.Single(u => u.id_usuario == usuario.id_usuario);
            foreach(Evento even in usuarioParaBorrar.eventos)
            {
                usuarioParaBorrar.eliminarEvento(even.lugar);
            }
            foreach (Guardarropa guar in usuarioParaBorrar.guardarropas)
            {
                usuarioParaBorrar.eliminarGuardarropa(guar.nombreGuardarropas);
            }
            context.usuarios.Remove(usuarioParaBorrar);
            context.SaveChanges();
        }

        public void agregarGaurdarropaCompartido(int guardarropaCompartido, String usuario, DB context)
        {
            var user = context.usuarios.Single(u => u.usuario == usuario);
            guardarropaXusuarioRepository gur = new guardarropaXusuarioRepository();
            gur.id_guardarropa = guardarropaCompartido;
            gur.id_usuario = user.id_usuario;
            context.guardarropaXusuarioRepositories.Add(gur);
            context.SaveChanges();
        }

        public int tipoUsuario(String usuario, DB context)
        {
            var user = context.usuarios.Single(u => u.usuario == usuario);
            return user.tipoDeUsuario;
        }

        public Usuario verificarLoguing(String nomusuario,String contrasenia, DB context)
        {
            var user = context.usuarios.Single(u => u.usuario == nomusuario);
            if(user==null)
            {
                return null;
            }
            else
            {
                if(user.contrasenia==contrasenia)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Guardarropa> loguingGuardarropas(int idUser, DB context)
        {
            List<Guardarropa> g = new List<Guardarropa>();
            List<guardarropaXusuarioRepository> gur = new List<guardarropaXusuarioRepository>();
            gur = context.guardarropaXusuarioRepositories.Where(u => u.id_usuario == idUser).ToList();
            GuardarropaRepository gr = new GuardarropaRepository();
            foreach (guardarropaXusuarioRepository a in gur)
            {
               g.Add(gr.loguing(a.id_guardarropa,context));
            }
            return g;
        }

        public List<Evento> loguingEvento(int idUser, DB context)
        {
            List<Evento> gur = new List<Evento>();
            gur = context.eventos.Where(u => u.id_usuario == idUser).ToList();
            foreach (Evento a in gur)
            {
                if(a.id_atuendo!=-1)
                {
                    AtuendoRepository at = new AtuendoRepository();
                    a.atuendo = at.loguing(a.id_atuendo, context);
                }
            }
            return gur;
        }
    }
}
