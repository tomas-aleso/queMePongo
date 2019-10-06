using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueMePongo;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    class GenerarTest
    {
        [TestMethod]
        public void menosDe3PrendasPorCategoria()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
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

            List<Atuendo> Juan = user.ObtenerSugerencias("BuenosAires");

            Assert.IsTrue(Juan.TrueForAll(a => (a.prendas.FindAll(p => p.tipo.categoria == "torso").Count < 3) && (a.prendas.FindAll(p => p.tipo.categoria == "piernas").Count < 3) && (a.prendas.FindAll(p => p.tipo.categoria == "calzado").Count < 2)));

        }

    }
}
