using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace KmlKmzToObjectDll
{
   public class ReadXmlDoc
    {
        private const string PointStr = "POINT ({0})";
        private const string LinestringStr = "LINESTRING ({0})";
        private const string PoliygonStr = "POLYGON (({0}))";
        private const string MultiLineStringStr = "MULTILINESTRING ({0})";
        private const string MultiPolygonStr = "MULTIPOLYGON ({0})";
        private const string GeometrycollectionStr = "GEOMETRYCOLLECTION ({0})";
        private string DirectoryPath { get; }
        private string KmlDocPath { get; }
        private string Point => PointStr;
        private string Linestring => LinestringStr;
        private string Poliygon => PoliygonStr;
        private string MultiLineString => MultiLineStringStr;
        private string MultiPolygon => MultiPolygonStr;
        private string Geometrycollection => GeometrycollectionStr;
        private List<DtoKml> KmlData { get; set; }
        private List<DtoKml> ReadXmlDocument(string filePath)
        {
            KmlData = new List<DtoKml>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            XmlNodeList xmlnode = xmlDocument.GetElementsByTagName("Placemark");
            for (int i = 0; i < xmlnode.Count; i++)
            {
                DtoKml addingItem = new DtoKml();

                var b = xmlnode[i].ChildNodes;
                foreach (var node1 in b)
                {
                    XmlElement element = (XmlElement)node1;
                    switch (element.Name)
                    {
                        case "name":
                            addingItem.Name = element.InnerText;
                            break;
                        case "description":
                            addingItem.Description = element.InnerText;
                            break;
                        case "Point":
                            addingItem.WktString = GetWkt(element, element.Name);
                            addingItem.ObjectTypeName = element.Name.ToUpper(CultureInfo.CurrentCulture);
                            break;
                        case "Polygon":
                            addingItem.WktString = GetWkt(element, element.Name);
                            addingItem.ObjectTypeName = element.Name.ToUpper(CultureInfo.CurrentCulture);
                            break;
                        case "LineString":
                            addingItem.WktString = GetWkt(element, element.Name);
                            addingItem.ObjectTypeName = element.Name.ToUpper(CultureInfo.CurrentCulture);
                            break;
                    }
                }
                KmlData.Add(addingItem);

            }
            return KmlData;
        }

        public ReadXmlDoc()
        {
            DirectoryPath =$"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)}\\Extract".Substring(6);
            KmlDocPath = $"{DirectoryPath}\\doc.kml";
        }


        /// <summary>
        /// read Google Kml & Kmz Xml DOcument
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isKmlFile"></param>
        /// <returns></returns>
        public List<DtoKml> ReadGoogleDocument(string filePath,bool isKmlFile=true)
        {
            List < DtoKml > result;
            if (isKmlFile)
            {
                result= ReadXmlDocument(filePath);
            }
            else
            {
                ExtractKmzToKml(filePath,DirectoryPath);
                result = ReadXmlDocument(KmlDocPath);
                DeleteKmzDirectory(DirectoryPath);
            }
            return result;
        }

        /// <summary>
        /// extract kmz files to path
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="pathDirectory"></param>
        public void ExtractKmzToKml(string zipPath, string pathDirectory)
        {
            DeleteKmzDirectory(DirectoryPath);
            CreateKmzDirectory(DirectoryPath);
            ZipFile.ExtractToDirectory(zipPath, pathDirectory);
        }
       
        /// <summary>
        /// Delete extracted kmz folder and files
        /// </summary>
        public bool DeleteKmzDirectory(string pathDirectory)
        {
            if (Directory.Exists(pathDirectory))
            {
                DirectoryInfo di = new DirectoryInfo(pathDirectory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(pathDirectory);
            }
            return !Directory.Exists(pathDirectory);
        }
       
        /// <summary>
        /// creating directory for extract kmz files
        /// </summary>
        public bool CreateKmzDirectory(string pathDirectory)
        {
            Directory.CreateDirectory(pathDirectory);
            return Directory.Exists(pathDirectory);
        }

        /// <summary>
        /// get files wkt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        private string GetWkt(XmlElement data, string caption)
        {
            var str = (XmlElement)(data.GetElementsByTagName("coordinates")[0]);
            var txt = str.InnerText.Replace(",0", ",").Replace(" ", "").Trim().Split(',');

            string coor = "";
            int a = 1;
            for (int i = 0; i < txt.Length; i++)
            {
                if (!string.IsNullOrEmpty(txt[i]))
                {
                    if (i == txt.Length - 2)
                    {
                        coor += txt[i];
                    }
                    else if (a == 1)
                    {
                        coor += txt[i] + " ";
                    }
                    else if (a == 2)
                    {
                        coor += txt[i] + ",";
                        a = 0;
                    }
                }
                a++;
            }

            string result = "";
            switch (caption)
            {
                case "Point":
                    result = string.Format(Point, coor);
                    break;
                case "Polygon":
                    result = string.Format(Poliygon, coor);
                    break;
                case "LineString":
                    result = string.Format(Linestring, coor);
                    break;
                case "MultiLineString":
                    result = string.Format(MultiLineString, coor);
                    break;
                case "Geometrycollection":
                    result = string.Format(Geometrycollection, coor);
                    break;
            }

            return result;
        }

    }
}
