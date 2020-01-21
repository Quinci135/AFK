using System;
using System.IO;
using Newtonsoft.Json;

namespace AFK
{
    public class AFKConfigFile
    {
        public bool afkwarp = true;
        public bool afkkick = false;
        public int afkwarptime = 300;
        public int afkkicktime = 700;
        public int afkspam = 20;
        public string awayMessage = "{player} is away from his/her keyboard and has been warped To The AFK Zone!";
        public string returnMessage = "{player} is back from AFK! YAY!!!";
        public string[] whitelistByName = { "Rofle-Bot" };

        public static AFKConfigFile Read(string path)
        {
            if (!File.Exists(path))
                return new AFKConfigFile();
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Read(fs);
            }
        }

        public static AFKConfigFile Read(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                var cf = JsonConvert.DeserializeObject<AFKConfigFile>(sr.ReadToEnd());
                if (ConfigRead != null)
                    ConfigRead(cf);
                return cf;
            }
        }

        public void Write(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                Write(fs);
            }
        }

        public void Write(Stream stream)
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(str);
            }
        }

        public static Action<AFKConfigFile> ConfigRead;
    }
}