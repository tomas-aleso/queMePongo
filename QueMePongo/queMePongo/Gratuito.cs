using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class Gratuito : TipoUsuario
    {


        public override Guardarropa crearGuardarropa(String nombreGuardarropa, Usuario user)
        {
            Guardarropa value = new Guardarropa(user, nombreGuardarropa);
            return value;

        }

        public override int topePrendasPorGuardarropa()
        {
            Helper sist = new Helper();
            return sist.capacidadMaxGratuito;
        }
    }
}