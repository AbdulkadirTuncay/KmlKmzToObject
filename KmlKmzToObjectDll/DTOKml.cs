using System;

namespace KmlKmzToObjectDll
{
    [Serializable]
    public class DtoKml
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string WktString { get; set; }
        public string ObjectTypeName { get; set; }
        public string KmlName { get; set; }

    }
}
