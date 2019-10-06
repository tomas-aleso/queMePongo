using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueMePongo;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    class UsuarioTest
    {
        [TestMethod]
        public void ABGuardarropa()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            user.crearGuardarropa("guardarropasPrueba");
            Assert.AreEqual(user.guardarropas.Count,1);
            user.eliminarGuardarropa("guardarropasPrueba");
            Assert.AreEqual(user.guardarropas.Count,0);
        }

        [TestMethod]
        public void compartirGuardarropa()
        {
            Helper sistema = new Helper();
            sistema.tipoDePrenda = sistema.levantarJSon();
            Usuario user = sistema.crearUsuario("Juan");
            Usuario user2 = sistema.crearUsuario("Pablo");
            Guardarropa g = user.crearGuardarropa("guardarropasPrueba");
            user.compartirGuardarropa(user2,g);
            Assert.AreEqual(user.guardarropas, user2.guardarropas);
        }

        [TestMethod]
        public void ABEvento()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("juan");
            user.crearEvento(new DateTime(2018, 10, 5, 7, 45, 0), new DateTime(2018, 10, 6, 8, 0, 0), new DateTime(2018, 10, 6, 9, 0, 0), "buenos Aires", "cena", 0);
            Assert.AreEqual(user.eventos.Count, 1);
            user.eliminarEvento("buenos Aires");
            Assert.AreEqual(user.eventos.Count, 0);
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
        public void usuarioSeIniciaGratuito()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("Juan");

            Assert.AreEqual(user.tipoUsuario.topePrendasPorGuardarropa(), 200);
        }

        [TestMethod]
        public void usuarioPremium()
        {
            Helper sistema = new Helper();
            Usuario user = sistema.crearUsuario("Juan");
            sistema.upgradeUsuario(user);
            Assert.AreEqual(user.tipoUsuario.topePrendasPorGuardarropa(), -1);
        }

    }
}
