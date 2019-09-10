using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    public abstract class TipoUsuario
    {
        public abstract Guardarropa crearGuardarropa(String nombreGuardarropa, Usuario user);
        public abstract int topePrendasPorGuardarropa();
    }
}