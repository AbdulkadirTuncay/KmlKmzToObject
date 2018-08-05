using System.IO;
using KmlKmzToObjectDll;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KmlKmzToObjectTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestReadKmlDocument()
        {
            var directoryPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}".Substring(6);
            var path1 = $"{directoryPath}\\Yerlerim.kml";

            ReadXmlDoc readXmlDoc=new ReadXmlDoc();
            Assert.AreEqual(6, readXmlDoc.ReadGoogleDocument(path1).Count);
        }
        [TestMethod]
        public void TestReadKmzDocument()
        {
            var directoryPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}".Substring(6);
            var path2 = $"{directoryPath}\\Yerlerim.kmz";

            ReadXmlDoc readXmlDoc = new ReadXmlDoc();
            Assert.AreEqual(6, readXmlDoc.ReadGoogleDocument(path2, false).Count);
        }

        [TestMethod]
        public void TestCreateKmzDirectory()
        {
            var directoryPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}\\Extract".Substring(6);

            ReadXmlDoc readXmlDoc = new ReadXmlDoc();
            Assert.AreEqual(true,readXmlDoc.CreateKmzDirectory(directoryPath));
        }
        [TestMethod]
        public void TestDeleteKmzDirectory()
        {
            var directoryPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}\\Extract".Substring(6);

            ReadXmlDoc readXmlDoc = new ReadXmlDoc();
            Assert.AreEqual(true, readXmlDoc.DeleteKmzDirectory(directoryPath));
        }
    }
}
