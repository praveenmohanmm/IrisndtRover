using System;
using System.Collections.Generic;
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

        /// <summary>
        /// call get points service hosted in azure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the entire history of inouts 
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoverEntity>> GetAllDatas()
        {

            try
            {
                List<RoverEntity> alldatas = new List<RoverEntity>();
                HttpResponseMessage response = await _client.GetAsync("https://savescreenshot.azurewebsites.net/api/GetHistory?code=bZh8ZBMaW3srEshbFdKdm5w1cm8az43Ar2pFXcGeD/YaWFZi1eybBA==");
                if (response.IsSuccessStatusCode)
                {

                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var res = response.Content.ReadAsStringAsync().Result;
                    res = JToken.Parse(res).ToString();
                    serSettings.StringEscapeHandling = StringEscapeHandling.Default;
                    alldatas = JsonConvert.DeserializeObject<List<RoverEntity>>(res, serSettings);
                    return alldatas;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// save screen shots and all other in-outs to azure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SaveData(RoverEntity input)
        {

            try
            {
                RoverFinalPoints finalData = new RoverFinalPoints();

                JObject oJsonObject = new JObject();
                oJsonObject.Add("input", input.input);
                oJsonObject.Add("output", input.output);
                oJsonObject.Add("image", input.image);


                var content = new StringContent(oJsonObject.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("https://savescreenshot.azurewebsites.net/api/SaveToStorage?code=fRSf6ebJvgZj7JjUaabFGLT8uABeC/VrsTX8jU97P325wIfA0tbdyw==", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                    
                }

                return false;

            }

            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return false;
            }


        }

    }
}
