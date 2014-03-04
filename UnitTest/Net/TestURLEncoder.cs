using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Net;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    /// <summary>
    /// Descripción resumida de TestURLEncoder
    /// </summary>
    [TestClass]
    public class TestURLEncoder
    {
        public TestURLEncoder()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las Tests que proporciona
        ///información y funcionalidad para la ejecución de Tests actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestMethod]
        public void EncodeUpdate()
        {
            UrlEncoder urlEncoder = new UrlEncoder();

            Client client = new Client();
            client.Email = "max@musterman.com";
            client.Description = "description";
            string reply = urlEncoder.EncodeUpdateObject(client);

            Assert.AreEqual(expected, reply);
        }
    }
}
