using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymillWrapper.Net;
using PaymillWrapper.Models;

namespace UnitTest.Net
{
    /// <summary>
    /// Descripción resumida de TestFilter
    /// </summary>
    [TestClass]
    public class TestFilter
    {
        public TestFilter()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la ejecución de pruebas actual.
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

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EncodeFilter()
        {
            Filter filter = new Filter();
            filter.Add("count", 1);
            filter.Add("offset", 2);
            filter.Add("interval", "month");
            filter.Add("amount", 495);
            filter.Add("created_at", 1352930695);
            filter.Add("trial_period_days", 5);

            string expected = "count=1&offset=2&interval=month&amount=495&created_at=1352930695&trial_period_days=5";
            string reply = filter.ToString();

            Assert.AreEqual(expected, reply);

        }

    }
}
