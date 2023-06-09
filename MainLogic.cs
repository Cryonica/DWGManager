using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using DWGManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWGManager
{
    internal class MainLogic
    {
        
        internal static List<DWGFile> ReadDWG()
        {
           
            DWGFile dwgFile1 = new DWGFile
            {
                FileName = "FileName",
                FileType = "link",
                //ElementId = 100,
                ViewName = "View1"
            };
            DWGFile dwgFile2 = new DWGFile
            {
                FileName = "FileName2",
                FileType = "link",
                //ElementId = 101,
                ViewName = "View1"
            };
            DWGFile dwgFile3 = new DWGFile
            {
                FileName = "FileName3",
                FileType = "",
                //ElementId = 102,
                ViewName = "View2"
            };
            DWGFile dwgFile4 = new DWGFile
            {
                FileName = "FileName4",
                FileType = "",
                //ElementId = 103,
                ViewName = "View3"
            };
            var dwgFiles = new List<DWGFile>
            {
                dwgFile1, dwgFile2, dwgFile3, dwgFile4
            };
            return dwgFiles;

        }
        internal static List<DWGFile> ReadDWG(UIDocument uidoc)
        {
            Document doc = uidoc.Document;
            var dwgFiles = new List<DWGFile>();

            // Создание коллекции для хранения найденных импортированных и внедренных DWG файлов
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> importedAndLinkedDWGFiles = collector.OfClass(typeof(ImportInstance))
                .WhereElementIsNotElementType()
                .ToElements();

            // Фильтрация импортированных и внедренных DWG файлов
            
            foreach (Element element in importedAndLinkedDWGFiles)
            {
                ImportInstance importInstance = element as ImportInstance;
                if (importInstance != null/* && importInstance.GetExternalFileReference().GetLinkedFileStatus() == LinkedFileStatus.NotFound*/)
                {
                    if (element.ViewSpecific)
                    {
                        string viewName = null;
                        try
                        {
                            Element viewElement = doc.GetElement(
                              element.OwnerViewId);
                            viewName = viewElement.Name;
                        }
                        catch (Autodesk.Revit.Exceptions
                          .ArgumentNullException) // just in case
                        {
                            viewName = String.Concat(
                              "Invalid View ID: ",
                              element.OwnerViewId.ToString());
                        }
                    }
                    else
                    {
                        CADLinkType cadLinkType = doc.GetElement(importInstance.GetTypeId()) as CADLinkType;
                        if (cadLinkType != null)
                        {
                            var fileName = cadLinkType.Name;
                            var elId = element.Id;
                            string vieName = string.Empty;
                            string fileType = string.Empty;
                            try
                            {
                                ExternalFileReference  efr =cadLinkType.GetExternalFileReference();
                                if (efr != null)
                                {
                                    //string linkStatus = efr.GetLinkedFileStatus().ToString();
                                    fileType = "Связь";
                                }
                            }
                            catch
                            {
                                fileType = "Импорт";
                            }


                            Level level = doc.GetElement(element.LevelId) as Level;

                            DWGFile dWGFile = new DWGFile
                            {
                                FileName = fileName,
                                FileType = fileType,
                                ElementId = elId.IntegerValue.ToString(),
                                ViewName = level.Name,
                                Element = element

                            };
                            dwgFiles.Add(dWGFile);



                        }
                        
                        

                    }
                    
                }
            }

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("Transaction Name");
            //    tx.Commit();
            //}



            
            
            return dwgFiles;

        }
        internal static void DeleteDwg(UIDocument uidoc, DWGFile dWGFile)
        {
            Document doc = uidoc.Document;
            using (Transaction tx = new Transaction(doc))
            {
                NLogU.Set("Start transaction for delete dwg");
                tx.Start($"Delete {dWGFile.FileName}");
                doc.Delete(dWGFile.Element.Id);

                tx.Commit();
                NLogU.Set("End transaction for delete dwg");
            }

        }

    }
}
