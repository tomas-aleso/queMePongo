using System;
using QueMePongo;
using System.Linq;

namespace queMePongo.Repositories
{
    public class UsuarioRepository
    {
        public void Insert(Usuario usuario, DB context)
        {
            context.usuarios.Add(usuario);
            context.SaveChanges();
            Console.WriteLine($"\nUsuario {usuario.usuario} con ID {usuario.id_usuario} creado!");
        }

        public void Update(Usuario usuario, DB context)
        {

        }

        public void Delete(int usuarioId, DB context)
        {
            var usuarioParaBorrar = context.usuarios.Single(u => u.id_usuario == usuarioId);
            context.usuarios.Remove(usuarioParaBorrar);
            context.SaveChanges();
            Console.WriteLine($"\nUsuario {usuarioId} eliminado!");
        }

    }
}
