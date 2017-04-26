using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Selling.Model;

namespace Selling.Services
{
    public class PlainRestClient
    {

        public async Task<List<Company>> GetAllCompanys()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://apirestproyectoxam.azurewebsites.net/tables/Company";
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var result = await client.GetAsync(url);

                string data = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<Company>>(data);
                }
                else
                    return new List<Company>();
            }
        }

        public async Task<Company> CreateCompany(Company company)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://apirestproyectoxam.azurewebsites.net/tables/Company";
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                string content = JsonConvert.SerializeObject(company);

                StringContent body = new StringContent(content, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(url, body);

                string data = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Company>(data);
                }
                else
                    return null;

            }
        }

    }
}