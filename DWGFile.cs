using Autodesk.Revit.DB;

namespace DWGManager
{
    internal class DWGFile
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string ElementId { get; set; }
        public string ViewName { get; set; }
        internal Element Element { get; set; }

    }
}
