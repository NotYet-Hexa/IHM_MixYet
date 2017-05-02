using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FacebookLogin
{
    public class Tools
    {
        public Tools()
        {

        }
        public string GetCurrentTime()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            if (DateTime.Now.IsDaylightSavingTime() == true)
            {
                now = now.AddHours(1);
            }
            string currentTime = (string.Format("Current Time: {0}", now));
            return currentTime;
        }

        public async Task SendData(string jsonData, string dest)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000");// okok 

            var content = new StringContent(jsonData);

            HttpResponseMessage response =  await client.PostAsync(dest, content);// on reçoit quoi - où
        }

        public async Task VoteAbility(string jsonData, string dest)
        {
            var client = new HttpClient();
            var content = new StringContent(jsonData);
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            HttpResponseMessage response = await client.PostAsync(dest, content);
            //test la réponse 
        }
        
        public string ObjectToJson(object objet)
        {
            return JsonConvert.SerializeObject(objet);
        }


    }

}

