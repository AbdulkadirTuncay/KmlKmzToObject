using System;
using KmlKmzToObjectDll;

namespace KmlKmzToObject
{
    class Program
    {
        static void Main()
        {
            ReadXmlDoc();
        }
        private static void ReadXmlDoc()
        {
            ReadXmlDoc doc = new ReadXmlDoc();

            var path1 = "C:\\Users\\abtuncay\\Desktop\\Yerlerim.kml";
            var path2 = "C:\\Users\\abtuncay\\Desktop\\Yerlerim.kmz";
            var a = doc.ReadGoogleDocument(path1);
            var b = doc.ReadGoogleDocument(path2, false);


            foreach (var item in a)
            {
                Console.WriteLine($"{item.Name} - {item.Description}");
                Console.WriteLine($"{item.ObjectTypeName} - {item.WktString}");
            }
            foreach (var item in b)
            {
                Console.WriteLine($"{item.Name} - {item.Description}");
                Console.WriteLine($"{item.ObjectTypeName} - {item.WktString}");
            }
            Console.ReadLine();
        }

    }
}
