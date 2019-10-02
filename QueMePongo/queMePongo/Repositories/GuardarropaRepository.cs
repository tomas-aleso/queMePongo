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
            List<Guardarropa> guard = new List<Guardarropa>();
            guard = context.guardarropas.Where(b => b.id_duenio == idUsuario).ToList();
            foreach (Guardarropa a in guard)
            {
                if (guardarropa.nombreGuardarropas == a.nombreGuardarropas)
                {
                    guardarropaXusuarioRepository gur = new guardarropaXusuarioRepository();
                    gur.id_guardarropa = a.id_guardarropa;
                    gur.id_usuario = idUsuario;
                    context.guardarropaXusuarioRepositories.Add(gur);
                    break;
                }

            }
            context.SaveChanges();
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
            if(g.id_duenio==idUsuario)
            {
                List<guardarropaXusuarioRepository> gur = new List<guardarropaXusuarioRepository>();
                gur = context.guardarropaXusuarioRepositories.Where(u => u.id_guardarropa == guardarropaId).ToList();
                foreach (guardarropaXusuarioRepository a in gur)
                {
                        context.guardarropaXusuarioRepositories.Remove(a);
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
    }
}