using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZQ.MvvmlightApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        //public Action Validate; 简化版
        public delegate void Validate(object sender);
        public event Validate ValiateEvent;
    }
}
