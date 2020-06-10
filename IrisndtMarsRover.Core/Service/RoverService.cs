using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IrisndtMarsRover.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace IrisndtMarsRover.Core.Service
{

    public class RoverService
    {
        HttpClient _client;

        public RoverService()
        {
            
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Content", "application/json");
        }

        public async Task<RoverFinalPoints> GetFinalPoints(RoverInput input)
        {
            
            try
            {
                RoverFinalPoints finalData = new RoverFinalPoints();

                JObject oJsonObject = new JObject();
                oJsonObject.Add("commands", input.commands);
                oJsonObject.Add("startXPos", input.startXPos);
                oJsonObject.Add("startYPos", input.startYPos);
                oJsonObject.Add("max", input.max);
                oJsonObject.Add("startDirection", (int)input.startDirection);

                var content = new StringContent(oJsonObject.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("https://getpath.azurewebsites.net/api/GetRoverPositions?code=bZShJRLsump/xpZuafRYvLqOFTIdDvghauBVRjIYaqALstSRYSPqqQ==", content);
                if (response.IsSuccessStatusCode)
                {
                    
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var res =  response.Content.ReadAsStringAsync().Result;
                    res = JToken.Parse(res).ToString();
                    serSettings.StringEscapeHandling =  StringEscapeHandling.Default;
                    finalData = JsonConvert.DeserializeObject<RoverFinalPoints>(res, serSettings);
                }

            
                return finalData;

        
            }
            
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return null;
            }

            
        }


        //public async Task SaveScreenshot(TodoItem item, bool isNewItem = false)
        //{
        //    Uri uri = new Uri(string.Format(Constants.TodoItemsUrl, string.Empty));


        //    string json = JsonConvert.SerializeObject(item);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;
        //    if (isNewItem)
        //    {
        //        response = await client.PostAsync(uri, content);
        //    }


        //    if (response.IsSuccessStatusCode)
        //    {
        //        Debug.WriteLine(@"\tTodoItem successfully saved.");
        //    }

        //}
    }
}
