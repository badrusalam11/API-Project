using API.Models;
using API.ViewModels;
using Client.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;
        

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<RegisterVM>> GetRegisterData()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "GetRegisterData"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<RegisterVM> GetRegisterDataNIK(string NIK)
        {
            RegisterVM entity = new RegisterVM();

            using (var response = await httpClient.GetAsync(request + "GetRegisterData/"+ NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);

            }
            return entity;
        }

        public Object Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            //var result = httpClient.PostAsync(address.link + request + "Register/", content).Result;
            //return result.StatusCode;

            Object entities = new Object();

            using (var response = httpClient.PostAsync(request + "Register/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;

        }

        public Object UpdateRegisterData(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            //var result = httpClient.PutAsync(request + "UpdateRegisterData/", content).Result;
            //return result.StatusCode;
            Object entity = new Object();

            using (var response = httpClient.PutAsync(request + "UpdateRegisterData/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode DeleteRegisterData(string NIK)
        {
            var result = httpClient.DeleteAsync(request + "DeleteRegisterData/" + NIK).Result;
            return result.StatusCode;
        }


    }

}
