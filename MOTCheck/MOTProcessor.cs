using MOTCheck.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOTCheck
{
    public class MOTProcessor
    {
        public static async Task<MOTResponseModel> LoadMOTDetails(string registration)
        {
            string url = "";

            if (!string.IsNullOrEmpty(registration))
            {
                url = $"{ ConfigurationManager.AppSettings["APIEndpoint"] }?registration={ registration }";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<MOTResponseModel> mots = await response.Content.ReadAsAsync<List<MOTResponseModel>>();

                    return mots[0];
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



        }
    }
}
