using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_lab8._3
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            View view = doc.ActiveView;
            string filepath = Path.Combine(desktop_path, view.Name);

            var IEOoptions = new ImageExportOptions();
            IEOoptions.ZoomType = ZoomFitType.FitToPage;
            IEOoptions.PixelSize = 1280;
            IEOoptions.ImageResolution = ImageResolution.DPI_600;
            IEOoptions.FitDirection = FitDirectionType.Horizontal;
            IEOoptions.ExportRange = ExportRange.CurrentView;
            IEOoptions.HLRandWFViewsFileType = ImageFileType.PNG;
            IEOoptions.FilePath = filepath;
            IEOoptions.ShadowViewsFileType = ImageFileType.PNG;

            using (var ts = new Transaction(doc, "Export PNG"))
            {
                ts.Start();
                doc.ExportImage(IEOoptions);
                ts.Commit();
            }
            return Result.Succeeded;
        }
    }
}
