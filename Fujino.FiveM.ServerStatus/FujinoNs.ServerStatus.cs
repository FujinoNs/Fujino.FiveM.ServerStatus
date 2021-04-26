using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace FujinoNs.ServerStatus
{
    public class ServerStatus
    {
        bool ipServ = false;
        string responseFromServer;
        public ServerStatus(string ip)
        {
            try
            {
                if (File.Exists("Newtonsoft.Json.dll"))
                {
                    WebRequest request = WebRequest.Create("http://" + ip + "/players.json");
                    request.Credentials = CredentialCache.DefaultCredentials;
                    ipServ = isExist("http://" + ip + "/players.json");
                    if (ipServ == true)
                    {
                        WebResponse response = request.GetResponse();
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(dataStream);
                            responseFromServer = reader.ReadToEnd();
                        }
                        response.Close();
                    }
                }
                else
                {
                    Console.WriteLine("\n\n[Fujino.FiveM.ServerStatus.dll] - [Error: Please install Packages: Newtonsoft.Json Version: 13.0.1]");
                    Console.WriteLine("[Fujino.FiveM.ServerStatus.dll] - [Please install in Package Manager: Install-Package Newtonsoft.Json -Version 13.0.1]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Fujino.FiveM.ServerStatus.dll] - [Error:" + ex.Message +"]");
            }
        }

        private bool isExist(string con)
        {
            WebRequest webRequest = HttpWebRequest.Create(con);
            webRequest.Method = "HEAD";
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Status()
        {
            if (ipServ == false)
            {
                return false;
            }
            return true;
        }

        public int PlayerCount()
        {
            Player[] PlayerOnline = JsonConvert.DeserializeObject<Player[]>(responseFromServer);
            var Count = PlayerOnline.Count();
            return Count;
        }
    }
}
