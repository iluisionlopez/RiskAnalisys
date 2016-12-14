using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using CSRToolWebApp.DataContracts.Base;
using System;

namespace CSRToolWebApp.Common
{
    public interface IRestService
    {
        string Get();
        string Get(List<KeyValuePair<string, string>> parameters);
        string Post(string jsonBody);
        string Operation();
    }

    public class RestService : IRestService
    {
        private static string _scheme = HttpContext.Current.Request.Url.Scheme;

#if !DEBUG
        private static string _host = "devcsrtool.scania.com";                 //Note to Luis - using the dev service to bypass WAM
#else
        private static string _host = HttpContext.Current.Request.Url.Host;  //Use in debug and change to service url when in place
#endif
        private readonly string _endPoint = string.Format("{0}://{1}/CSRToolService.svc/rest/",
                                                _scheme,
                                                _host);
        private string _operation;
        private WebClient _restClient;
        public RestService(string operation)
        {
            _operation = _endPoint + operation;
            _restClient = new WebClient();

            _restClient.Encoding = Encoding.UTF8;
            _restClient.Headers.Add("Content-Type", "application/json");
            //_restClient.Headers.Add("xdsuser", SessionHandler.LoggedInUser.UserId);
            _restClient.Headers.Add("Internal", "true");
            //_restClient.Headers.Add("Authorization", "Basic U0VSVklDRVNXSVAyOjNlNEhhQzN1UGVmOGVkcnVwcg==");
            _restClient.Headers.Add("Cache-Control", "no-cache");
        }

        public string Get()
        {
            return Get(null);
        }

        public string Get(List<KeyValuePair<string, string>> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (!_operation.EndsWith("/"))
                    _operation += "/";

                foreach (var parameter in parameters)
                {

                    if (parameters.First().Equals(parameter))
                        _operation += "?" + parameter.Key + '=' + parameter.Value;
                    else
                        _operation += "&" + parameter.Key + '=' + parameter.Value;
                }
            }

            Logging.LogInfo("GET " + _operation);
            return _restClient.DownloadString(_operation);
        }

        public string Post(string jsonBody)
        {
            Logging.LogInfo("POST " + _operation);
            return _restClient.UploadString(_operation, jsonBody);
        }
        
        public string Operation()
        {
            return _operation;
        }
    }
}