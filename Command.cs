#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DWGManager
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Result result = Result.Cancelled;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            MainForm mainForm = new MainForm(uidoc);
            var dialogResult = mainForm.ShowDialog();
            
            if (dialogResult.HasValue && dialogResult.Value)
            {
                result = Result.Succeeded;
            }

            return result;
        }
    }
}
