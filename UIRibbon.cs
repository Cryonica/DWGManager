using DWGManager.Utils;
using DWGManager.AppSettings;
using Miracad.Revit.DB;
using Miracad.Revit.UI;
using System.IO;

namespace BIM4.DWG
{
    public class UIRibbon
    {
        public static string Assembly_Path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), DWGManager.Utils.Utils.PluginDll);
        public static void LoadPlugin()
        {
            #region Добавляем кнопку в уже существующую панель
            //Ищем загруженный плагин 
            NLogU.Set("DWGMANAGER Start");
            var plugin = BIM4.App.Plugins.Get("AR");
            if (plugin != null)
            {
                //выбираем нужную панель, для добавления кнопки
                var panel1 = plugin.RibbonPanels[0];
                if (panel1 != null)
                {
                    //добавляем кнопку
                        panel1.AddItem(Miracad.Revit.UI.RibbonItem.AddPushButtonData(DWGManager.Utils.Utils.PluginID + "_DWGManager",
                                 "DWG Менеджер",
                                 "DWGManager.Command",
                                 AppStore.dwg_ico,
                                 "Выполняет удаление DWG-файлов",
                                 "",
                                 Assembly_Path));
                }
            }
            #endregion
        }
    }
}
