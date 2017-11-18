using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;

namespace NanoQuantumShellGame.Web
{
    public class QProcessor
    {
        private string _token = "";
        private HttpClient _client = new HttpClient();  //dont use "using" on HttpClient as its a shareable resource
        private string _baseUrl = "https://quantumexperience.ng.bluemix.net/api";

        public QUser User { get; set; }

        public QProcessor()
        {
        }

        public QProcessor(string token)
        {
            _token = token;
        }


        public QResult Login()
        {
            return PerformLogin(); ;
        }


        public QResult Login(string token)
        {
            _token = token;
            return PerformLogin(); ;
        }


        public QResult DeleteExperiment(string experimentID)
        {
            //DELETE https://quantumexperience.ng.bluemix.net/api/users/a3e5c196cb90688ba9a50dd7607999a6/codes/553c3398a4039e2b809cc6ec110e971e HTTP/1.1
            //Host: quantumexperience.ng.bluemix.net

            QResult result = new QResult();
            string baseUrl = string.Format("/users/{0}/codes/{1}", this.User.userid, experimentID);
            System.Diagnostics.Debug.WriteLine("Deleteing to URL: " + baseUrl);
            result = FetchAPIData(baseUrl, HttpMethod.Delete, null);
            if (result.Success)
            {
                Debug.WriteLine("Successfully deleted experiment.");
            }
            else
            {
                Debug.WriteLine("Failed to delete experiment. Response was " + result.Message);
            }
            return result;
        }



        private QResult PerformLogin()
        {
            if (_token == string.Empty) throw new Exception("A token is required.");

            QResult result = new QResult();

            HttpContent content = new StringContent("apiToken=" + _token,
                                    System.Text.Encoding.UTF8,
                                    "application/x-www-form-urlencoded");//CONTENT-TYPE header

            result = FetchAPIData("/users/loginWithToken", HttpMethod.Post, content);
            if (result.Success)
            {
                User = JsonConvert.DeserializeObject<QUser>(result.Message);
                Debug.WriteLine("Logged in and have UserID: " + User.userid);
            }
            else
            {
                User = null;
            }

            return result;

        }

        public QExecutionOutput GetOutputFromMessageData(string messageData)
        {
            QExecutionOutput x = JsonConvert.DeserializeObject<QExecutionOutput>(messageData);
            return x;
        }



        public QResult ExecuteCode(QCode code)
        {
            if (this.User == null) throw new Exception("Not logged in.");

            QResult result = new QResult();
            string url = string.Format("/codes/execute?shots={0}&seed={1}&deviceRunType={2}",
                    code.shots.ToString(),
                    code.seed.ToString(),
                    code.deviceRunType
                    );

            string data = string.Format("qasm={0}&codeType={1}&name={2}",
                    code.qasm,
                    code.codeType,
                    code.name
                    );

            var kvp = new List<KeyValuePair<string, string>>();
            kvp.Add(new KeyValuePair<string, string>("qasm", code.qasm));
            kvp.Add(new KeyValuePair<string, string>("codeType", code.codeType));
            kvp.Add(new KeyValuePair<string, string>("name", code.name));

            HttpContent content = new FormUrlEncodedContent(kvp);

            result = FetchAPIData(url, HttpMethod.Post, content);

            Debug.WriteLine("ExecuteCode received the following JSON from the API:");
            Debug.WriteLine(result.Message);


            return result;

        }


        private QResult FetchAPIData(string urlRelativePath,
                                    HttpMethod httpMethod,
                                    HttpContent contentToSend)
        {
            QResult result = new QResult();

            //add auth token if we have a user and we arent deleting
            if (User != null && httpMethod != HttpMethod.Delete)
            {
                urlRelativePath = urlRelativePath + "&access_token=" + User.id;
            }
            string url = _baseUrl + urlRelativePath;

            Debug.WriteLine("Performing " + httpMethod.ToString() + " to " + url);

            if (contentToSend != null)
            {
                Debug.WriteLine("Sending data " + contentToSend.ReadAsStringAsync().Result);
            }

            HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            request.Content = contentToSend;
            if (User != null) request.Headers.Add("X-Access-Token", User.id);
            using (HttpResponseMessage response = _client.SendAsync(request).Result)
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        result.Message = content.ReadAsStringAsync().Result;
                        result.Success = response.IsSuccessStatusCode;

                    }
                }
                else
                {
                    result.Message = response.ReasonPhrase;
                    result.Success = false;
                }
            return result;
        }





    }

}

