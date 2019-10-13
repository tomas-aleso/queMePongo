using System;
using QueMePongo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    public class GuardarropaRepository
    {
        public void Insert(Guardarropa guardarropa, DB context, int idUsuario)
        {
            context.guardarropas.Add(guardarropa);
            context.SaveChanges();
            guardarropaXusuarioRepository gur = new guardarropaXusuarioRepository();
            gur.id_guardarropa = guardarropa.id_guardarropa;
            gur.id_usuario = idUsuario;
            context.guardarropaXusuarioRepositories.Add(gur);
            context.SaveChanges();
        }

        public void Update(Guardarropa guardarropa, DB context)
        {

        }

        public bool existeGuardarropa(String nombreGuardarropa, DB context,int idUsuario)
        {
            List<guardarropaXusuarioRepository> gur= new List<guardarropaXusuarioRepository>();
            gur = context.guardarropaXusuarioRepositories.Where(b => b.id_usuario == idUsuario).ToList();
            foreach(guardarropaXusuarioRepository a in gur)
            {
                Guardarropa guardarrop = new Guardarropa();
                guardarrop= context.guardarropas.Single(b => b.id_guardarropa == a.id_guardarropa);
                if(guardarrop.nombreGuardarropas==nombreGuardarropa)
                {
                    return true;
                }

            }
            return false;
        }
        public void Delete(int guardarropaId,DB context, int idUsuario)
        {
            Guardarropa g = new Guardarropa();
            g = context.guardarropas.Single(b => b.id_guardarropa == guardarropaId);
            if(g.duenio==idUsuario)
            {
                List<guardarropaXusuarioRepository> gur = new List<guardarropaXusuarioRepository>();
                gur = context.guardarropaXusuarioRepositories.Where(u => u.id_guardarropa == guardarropaId).ToList();
                foreach (guardarropaXusuarioRepository a in gur)
                {
                        context.guardarropaXusuarioRepositories.Remove(a);
                }
                List<guardarropaXprendaRepository> gpr = new List<guardarropaXprendaRepository>();
                gpr = context.guardarropaXprendaRepositories.Where(u => u.id_guardarropa == guardarropaId).ToList();
                foreach (guardarropaXprendaRepository a in gpr)
                {
                    context.guardarropaXprendaRepositories.Remove(a);
                }
                context.guardarropas.Remove(g);
            }
            else
            {
                List<guardarropaXusuarioRepository> gur = new List<guardarropaXusuarioRepository>();
                gur = context.guardarropaXusuarioRepositories.Where(u => u.id_usuario == idUsuario).ToList();
                foreach (guardarropaXusuarioRepository a in gur)
                {
                    if (a.id_guardarropa == guardarropaId)
                    {
                        context.guardarropaXusuarioRepositories.Remove(a);
                        break;
                    }

                }
            }
            context.SaveChanges();
        }
        public Guardarropa loguing(int idGuardarropa, DB context)
        {
            Guardarropa g = new Guardarropa();
            List<guardarropaXusuarioRepository> gur = new List<guardarropaXusuarioRepository>();
            gur = context.guardarropaXusuarioRepositories.Where(u => u.id_guardarropa == idGuardarropa).ToList();
            g = context.guardarropas.Single(u => u.id_guardarropa == idGuardarropa);
            foreach(guardarropaXusuarioRepository gu in gur)
            {
                if(gu.id_usuario!=g.duenio)
                {
                    g.usuariosCompartidos.Add(gu.id_usuario);
                }
            }
            List<guardarropaXprendaRepository> gpr = new List<guardarropaXprendaRepository>();
            gpr = context.guardarropaXprendaRepositories.Where(u => u.id_guardarropa == idGuardarropa).ToList();
            foreach (guardarropaXprendaRepository p in gpr)
            {
                PrendaRepository per = new PrendaRepository();
                g.prendas.Add(per.loguing(p.id_prenda, context));
            }
            return g;
        }
    }
}