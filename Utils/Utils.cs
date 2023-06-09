using DWGManager.AppSettings;
using Miracad.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWGManager.Utils
{
    internal class BaseUtils
    {


        #region Определение данных информации
        //Название приложения в сплит-кнопке выбора
        public static string PluginTitle = McLanguage.GetEnvironmentButton(McLanguage.Get(App0.PluginTitle));
        //Страница магазина
        public static string PluginAppStore = Utils.GetAppStorePage();
        #endregion
    }
    internal class Utils
    {
        internal static string PluginID = App0.PluginID;
        internal const string dll_ID = ".dll";
        internal const string lic_ID = ".lic";
        internal static string PluginTitle = App0.PluginTitle;
        internal static string PluginDll = PluginID + Utils.dll_ID;
        internal static string PluginLic = PluginID + Utils.lic_ID;
        internal static string GetAppStorePage()
        {
            Autodesk.Revit.ApplicationServices.LanguageType lt = McDocument.ActiveUIControlledApplication.ControlledApplication.Language;
            string result = "";
            try
            {
                if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Russian) result = AppStore.RUS;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.English_USA) result = AppStore.ENU;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.German) result = AppStore.DEU;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.French) result = AppStore.FRA;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Czech) result = AppStore.CSY;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Polish) result = AppStore.PLK;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Italian) result = AppStore.ITA;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Spanish) result = AppStore.ESP;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Japanese) result = AppStore.JPN;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Korean) result = AppStore.KOR;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Brazilian_Portuguese) result = AppStore.PTB;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Chinese_Simplified) result = AppStore.CHS;
                else if (lt == Autodesk.Revit.ApplicationServices.LanguageType.Chinese_Traditional) result = AppStore.CHT;
            }
            catch { }

            if (result == "")
                return AppStore.ENU;
            else
                return result;
        }
    }
}
