using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueMePongo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    class GuardarropaTest
    {
        [TestMethod]
        public void ABPrendas()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");

            guardarropas.crearPrenda(sistema.tipoDePrenda[5], "algodon", "Blanco", "Verde", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[6], "algodon", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[7], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[8], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[9], "lana", "Negro", "", user);

            Assert.AreEqual(guardarropas.prendas.Count, 5);

            guardarropas.eliminarPrenda(guardarropas.prendas.First());
            Assert.AreEqual(guardarropas.prendas.Count, 4);
        }

        [TestMethod]
        public void guardarropaCompartidoGeneraCorrectamente()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Usuario user2 = sistema.crearUsuario("Luis");
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");

            guardarropas.crearPrenda(sistema.tipoDePrenda[0], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[1], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[2], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[3], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[4], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[5], "algodon", "Blanco", "Verde", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[6], "algodon", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[7], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[8], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[9], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[10], "jean", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[11], "poliester", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[12], "poliester", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[13], "cuero", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[14], "cuero", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[15], "", "Negro", "", user);

            user.compartirGuardarropa(user2, guardarropas);
            List<Atuendo> Juan = user.ObtenerSugerencias("BuenosAires");
            List<Atuendo> Luis = user2.ObtenerSugerencias("BuenosAires");


            Assert.AreEqual(Juan.Count, Luis.Count);

        }

        [TestMethod]
        public void prendasNoSeCompartenEntreGuardarropas()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");
            Guardarropa guardarropas2 = user.crearGuardarropa("guardarropasPrueba2");

            guardarropas.crearPrenda(sistema.tipoDePrenda[0], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[1], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[2], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[3], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[4], "", "nada", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[5], "algodon", "Blanco", "Verde", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[6], "algodon", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[7], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[8], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[9], "lana", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[10], "jean", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[11], "poliester", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[12], "poliester", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[13], "cuero", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[14], "cuero", "Negro", "", user);
            guardarropas.crearPrenda(sistema.tipoDePrenda[15], "", "Negro", "", user);

            guardarropas2.crearPrenda(sistema.tipoDePrenda[7], "lana", "Negro", "", user);

            List<Atuendo> atuendos = user.ObtenerSugerencias(Evento even); // hay que crear evento? no se puede generar sin evento?

            Assert.IsTrue(atuendos.Exists(a => a.prendas.Exists(p => p.Igual(guardarropas2.prendas[0]))));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "el color principal no puede ser igual que el secundario")]
        public void prenda2Colores()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");
            guardarropas.crearPrenda(sistema.tipoDePrenda[5], "algodon", "Blanco", "Blanco", user);
        }
    }
}
