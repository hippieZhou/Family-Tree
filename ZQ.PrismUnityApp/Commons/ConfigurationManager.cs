using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZQ.PrismUnityApp.Commons
{
    class ConfigurationManager
    {
        public static object locker = new object();

        private static ConfigurationManager _current;
        public static ConfigurationManager Current
        {
            get
            {
                if (_current == null)
                {
                    lock (locker)
                    {
                        if (_current == null)
                        {
                            _current = new ConfigurationManager();
                        }
                    }
                }
                return _current;
            }
        }

        ConfigurationManager() { }

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
