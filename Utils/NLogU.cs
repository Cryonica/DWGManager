using System;
using System.IO;
using NLog;
using Miracad.Revit.DB;
using Autodesk.Revit.DB;
using System.Windows;

namespace DWGManager.Utils
{
    internal class LogN
    {
        internal static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    }
    internal class NLogU
    {

        static bool IsOk = false;

        internal static void SetLogger(string str)
        {
            try
            {
                NLogU.Logger().Error("{0}:{1}", NLogU.Get(), str);
            }
            catch (FileNotFoundException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        internal static void SetLoggerFun(string str)
        {
            try
            {
                NLogU.Logger().Error("{0}:{1}", NLogU.GetFun(), str);
            }
            catch (FileNotFoundException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// Получить логгер
        /// </summary>
        /// <returns></returns>
        internal static NLog.Logger Logger()
        {
            LoggerConfig();
            return LogN.logger;
        }

        /// <summary>
        /// Здесь хранится ID функции в которой происходит запись в логгер
        /// </summary>
        static string log_id = "";
        static string Current = "";
        static string Current_ElementId = "";
        static string Current_Document = "";
        #region Для функций 
        static string log_idf = "";
        static string Currentf = "";
        static string Current_ElementIdf = "";
        static string Current_Documentf = "";
        public static bool IsDebug = false;
        #endregion
        static void LoggerConfig()
        {
            if (!IsOk)
            {
                IsOk = true;
                var config = new NLog.Config.LoggingConfiguration();
                string filepath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Utils.PluginDll).Replace(".dll", ".log");
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = filepath };
                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Debug, LogLevel.Error, logfile);
                NLog.LogManager.Configuration = config;
            }
        }

        /// <summary>
        /// Устанавливаем ID метода двумя буквами
        /// </summary>
        /// <param name="method_id"></param>
        internal static void Start(string method_id)
        {
            log_id = method_id;
        }

        internal static void StartFun(string method_id)
        {
            log_idf = method_id;
        }

        /// <summary>
        /// Устанавливаем номер индекса строки (три цифры) чтобы легко найти в логгере строку с ошибкой
        /// </summary>
        /// <param name="index"></param>
        internal static void Set(string index)
        {
            try
            {
                if (Current_ElementId != "")
                    Current = string.Format("0x{0}{1} Document={2} ElementId={3}", log_id, index, Current_Document, Current_ElementId);
                else
                    Current = string.Format("0x{0}{1}", log_id, index);
            }
            catch { }
            if (IsDebug)
                Miracad.Revit.UI.Windows.MsgBox(Current);
        }

        internal static void Set(string index, Element elem)
        {
            try
            {
                Current = string.Format("0x{0}{1} Document={2} ElementId={3}", log_id, index, elem.Document.Title, elem.ToIdLink());
                Current_ElementId = elem.ToIdLink();
                Current_Document = elem.Document.Title;
            }
            catch { }
            if (IsDebug)
                Miracad.Revit.UI.Windows.MsgBox(Current);
        }

        internal static void Set(string index, McElement elem)
        {
            try
            {
                Current = string.Format("0x{0}{1} Document={2} ElementId={3}", log_id, index, elem.Document.Title, elem.Id);
                Current_ElementId = elem.Id;
                Current_Document = elem.Document.Title;
            }
            catch { }
            if (IsDebug)
                Miracad.Revit.UI.Windows.MsgBox(Current);
        }

        internal static void Set(string index, string comment)
        {
            try
            {
                Current = string.Format("0x{0}{1} comment: {2}", log_id, index, comment);
            }
            catch { }
            if (IsDebug)
                Miracad.Revit.UI.Windows.MsgBox(Current);
        }



        /// <summary>
        /// Сохраняем ID элемента, у которого ошибка произошла
        /// </summary>
        /// <param name="elem"></param>
        internal static void Set(Element elem)
        {
            try
            {
                Current_ElementId = elem.ToIdLink();
                Current_Document = elem.Document.Title;
            }
            catch { }
        }

        internal static void ClearElementId()
        {
            try
            {
                Current_ElementId = "";
                Current_Document = "";
            }
            catch { }
        }




        internal static void SetFun(string indexf)
        {
            try
            {
                if (Current_ElementIdf != "")
                    Currentf = string.Format("0x{0}{1} Document={2} ElementId={3}", log_idf, indexf, Current_Documentf, Current_ElementIdf);
                else
                    Currentf = string.Format("0x{0}{1}", log_idf, indexf);
            }
            catch { }
        }

        internal static void SetFun(string indexf, Element elemf)
        {
            try
            {
                Currentf = string.Format("0x{0}{1} Document={2} ElementId={3}", log_idf, indexf, elemf.Document.Title, elemf.ToIdLink());
                Current_ElementIdf = elemf.ToIdLink();
                Current_Documentf = elemf.Document.Title;
            }
            catch { }
        }

        internal static void SetFun(string indexf, McElement elemf)
        {
            try
            {
                Currentf = string.Format("0x{0}{1} Document={2} ElementId={3}", log_idf, indexf, elemf.Document.Title, elemf.Id);
                Current_ElementIdf = elemf.Id;
                Current_Documentf = elemf.Document.Title;
            }
            catch { }
        }

        /// <summary>
        /// Сохраняем ID элемента, у которого ошибка произошла
        /// </summary>
        /// <param name="elem"></param>
        internal static void SetFun(Element elem)
        {
            try
            {
                Current_ElementIdf = elem.ToIdLink();
                Current_Documentf = elem.Document.Title;
            }
            catch { }
        }

        internal static void ClearElementIdFun()
        {
            try
            {
                Current_ElementIdf = "";
                Current_Documentf = "";
            }
            catch { }
        }




        /// <summary>
        /// Возвращаем строку индекса строки
        /// </summary>
        /// <returns></returns>
        internal static string Get()
        {
            return Current;
        }

        internal static string GetFun()
        {
            return Currentf;
        }
    }
}
