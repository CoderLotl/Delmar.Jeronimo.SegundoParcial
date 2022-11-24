using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTest
{
    [TestClass]
    public class DataAccessTest
    {
        string connectionString;
        DataAccess dataAccess;

        [TestInitialize]
        public void SetConnectionString()
        {
            connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            dataAccess = new DataAccess(connectionString);
        }

        [TestMethod]
        public void TestConnection_OK()
        {
            Assert.IsTrue(dataAccess.TestConnection());
        }
    }
}
