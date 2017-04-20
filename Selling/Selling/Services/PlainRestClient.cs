//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace Selling.Services
{
    public abstract class PlainRestClient
    {
        protected string BaseUrl { get; set; }

        protected PlainRestClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<string> GetData()
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(BaseUrl).ConfigureAwait(false);
            }
        }

        protected async Task<IEnumerable<T>> GetAsJson<T>()
            where T : class
        {
            var result = Enumerable.Empty<T>();
            //using (var httpClient = new HttpClient())
            //{
            //    //httpClient.DefaultRequestHeaders.Accept.Clear();
            //    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //   // BaseUrl = ;
            //    var response = await httpClient.GetAsync("https://restcountries.eu/rest/v1/all").ConfigureAwait(false);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        //var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //        //if (!string.IsNullOrWhiteSpace(json))
            //        //{
            //        //    result = await Task.Run(() =>
            //        //    {
            //        //        return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            //        //    }).ConfigureAwait(false);
            //        //}
            //    }
            //}
            //return result;
            if (CrossConnectivity.Current.IsConnected == true)
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage llamada = await httpClient.GetAsync("http://restcountries.eu/rest/v1/all").ConfigureAwait(false);

                    //var paisPrueba = new Pais() { name = "Nueva Entrada" };
                    //string parseo = JsonConvert.SerializeObject(T);
                    //Debug.WriteLine(parseo);
                    //await httpClient.PostAsync("/Inventarios", new StringContent(parseo), Encoding.UTF8, "application/json").ConfigureAwait(false);

                    //https://restcountries.eu/#api-endpoints
                    if (llamada.IsSuccessStatusCode)
                    {
                        //var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);
                        //result = await Task.Run(() =>
                        //{
                        //    return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        //}).ConfigureAwait(false);
                    }
                }
            }
            return result;
        }

        protected string FromUrl(string baseUrl, string resource)
        {
            return string.Join("/", baseUrl, resource);
        }
    }
}
