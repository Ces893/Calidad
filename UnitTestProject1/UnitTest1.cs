using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Especialista es = new Especialista();
            es.FirstName = "TestNombre1";
            es.LastName = "Apellido1";
            es.CodigoColegiatura = "A0001";
            es.Especialidad = "Psicologo";
            es.Info = "Info1";
            es.Status true;
        }
    }
}
