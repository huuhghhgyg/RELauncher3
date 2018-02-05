using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RELauncher3.Update
{
    class checkUpdate
    {
        protected static int _ver = 1;//软件版本 !!!

        public static int Ver//只读访问器
        {
            get { return _ver; }
        }

        private static string _NewSoftVersion;
        private static string _ProgramURL;
        private static string _ConfigURL;
        private static string _RecoveryURL;

        public static string NewSoftVersion
        {
            get { return _NewSoftVersion; }
            set { _NewSoftVersion = value; }
        }

        public static string ProgramURL
        {
            get { return _ProgramURL; }
            set { _ProgramURL = value; }
        }

        public static string ConfigURL
        {
            get { return _ConfigURL; }
            set { _ConfigURL = value; }
        }

        public static string RecoveryURL
        {
            get { return _RecoveryURL; }
            set { _RecoveryURL = value; }
        }

        public void checkVer(string url)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            XmlNode config = doc.SelectSingleNode("Config");
            // 得到根节点Config
            XmlNodeList configList = config.ChildNodes;
            NewSoftVersion = configList.Item(0).InnerText;
            ProgramURL = configList.Item(1).InnerText;
            ConfigURL = configList.Item(2).InnerText;
            RecoveryURL = configList.Item(3).InnerText;
        }
    }
}
