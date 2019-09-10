using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueMePongo;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

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
        public void usuarioSeIniciaGratuito()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("Juan");

            Assert.AreEqual(user.tipoUsuario.topePrendasPorGuardarropa(),200);
        }

        [TestMethod]
        public void usuarioPremium()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("Juan");
            sistema.upgradeUsuario(user);
            Assert.AreEqual(user.tipoUsuario.topePrendasPorGuardarropa(), -1);
        }

        public void guardarropaCompartido()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Usuario user2 = sistema.crearUsuario("Luis");
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");
            
            user.compartirGuardarropa(user2, guardarropas);

            Assert.AreEqual(user.guardarropas.Count, user2.guardarropas.Count);
        }

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
        [ExpectedException(typeof(ArgumentException), "los usuarios son de distinto tipo")]
        public void compartirSoloMismoTipo()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("Juan");
            Usuario user2 = sistema.crearUsuario("Luis");
            sistema.upgradeUsuario(user);
            Guardarropa guardarropas = user.crearGuardarropa("guardarropasPrueba");
            user.compartirGuardarropa(user2, guardarropas);
        }

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
        [TestMethod]
        public void creacionCorrectaDeEvento()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("juan");
            user.crearEvento(new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 6, 8, 0, 0), new DateTime(2018, 10, 6, 9, 0, 0), "buenos Aires", "cena", 0);
            Assert.AreEqual(user.eventos.Count, 1);
            
        }

    }
}
