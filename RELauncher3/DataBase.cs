using KMCCC.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RELauncher3
{
    class DataBase
    {
        public string AutoSetMemory()
        {
            return (SystemTools.GetRunmemory() * 0.8).ToString("0");
        }
    }
}
