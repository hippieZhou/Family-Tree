using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ZQ.PrismUnityApp.Commons
{
    class ConfigurationManager
    {
        #region 一种单例模式

        //public static object locker = new object();
        //private static ConfigurationManager _current;
        //public static ConfigurationManager Current
        //{
        //    get
        //    {
        //        if (_current == null)
        //        {
        //            lock (locker)
        //            {
        //                if (_current == null)
        //                {
        //                    _current = new ConfigurationManager();
        //                }
        //            }
        //        }
        //        return _current;
        //    }
        //}

        //ConfigurationManager() { }

        #endregion

        #region 另一种单例模式

        public ConfigurationManager() { }
        private static readonly Lazy<ConfigurationManager> lazy = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
        public static ConfigurationManager Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        #endregion


        public List<ModuleInfo> GetModuleInfos()
        {
            try
            {
                return new List<ModuleInfo>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetModuleInfos Error!");
                return null;
            }
        }
    }
}
