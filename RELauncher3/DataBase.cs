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

        public string GetMinecraftVersionJson(int SiteNumber)//有参
        {
            switch (SiteNumber)
            {
                case 0:
                    return "https://launchermeta.mojang.com/mc/game/version_manifest.json";
                case 1:
                    return "http://bmclapi.bangbang93.com/mc/game/version_manifest.json";
                default:
                    return "https://s3.amazonaws.com/Minecraft.Download/versions/versions.json";
            }
        }

        public string GetMinecraftVersionJson()//无参
        {
            return "https://launchermeta.mojang.com/mc/game/version_manifest.json";
        }
    }
}
