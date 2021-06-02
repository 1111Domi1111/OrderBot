using WebApplication1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CocktailsBot.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace CocktailsBot.Clients
{
    class ApiClient
    {
        private HttpClient _client;
        private static string _adress;
        private static string _apikey;

        public ApiClient()
        {
            _adress = Constants.adress;
            _apikey = Constants.apikey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<List<DBRepositoryDiscount>> GetListDiscount()
        {
            var responce = await _client.GetAsync($"/Order/discount");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<DBRepositoryDiscount>>(content);
            return result;
                
    
        }
        public async Task<ListDish> GetListDishes()
        {
            var responce = await _client.GetAsync($"/Order/dishes");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListDish> (content);
            return result;

        }
        public async Task<ListDish> GetListLastDishes()
        {
            var responce = await _client.GetAsync($"/Order/list-dishes");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListDish>(content);
            return result;

        }
        public async Task<ListCoctails> GetListLastCoctails()
        {
            var responce = await _client.GetAsync($"/Order/last-coctails");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListCoctails>(content);
            return result;
        }
        public async Task<ListCoctails> GetListPopularCoctails()
        {
            var responce = await _client.GetAsync($"/Order/popular-coctails");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListCoctails>(content);
            return result;
        }
        public async Task<bool> MakeOrder(string id)
        {
           
            var myContent = JsonConvert.SerializeObject(id);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responce = await _client.PutAsync($"/Order/make-order", byteContent);

           
            
            return true;
        }
      
        public async Task<bool> AddDishOrCoctail(string id, string dishOrCoctail, int num, bool tf)
        {
      var mod=new ModelUp()
            {
                Id = id,
                DishOrCoctail = dishOrCoctail,
                Num = num,
                Tf = tf
            };
            var myContent = JsonConvert.SerializeObject(mod);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responce = await _client.PutAsync($"/Order/add-or-delete-dish-or-coctail", byteContent);

            var content = responce.Content.ReadAsStringAsync().Result;
            var result1 = JsonConvert.DeserializeObject<bool>(content);
            return result1;
        }
        public async Task<ListCoctails> GetListCoctails()
        {
            var responce = await _client.GetAsync($"/Order/coctails");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListCoctails>(content);
            return result;

        }


        public async Task<ListDish> GetListDishesByCategory(string Tags)
        {
            var responce = await _client.GetAsync($"/Order/list-dishes-by-category?tags={Tags}");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListDish>(content);
            return result;
        }
        public async Task<ListCoctails> GetListCoctailsByCategory(string Tags)
        {
            var responce = await _client.GetAsync($"/Order/coctails-by-category?category={Tags}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<ListCoctails>(content);
            return result;
        }
        public async Task<ListCoctails> GetCategoryCoctails()
        {
            var responce = await _client.GetAsync($"/Order/Category-coctails");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListCoctails>(content);
            return result;
        }
        public async Task<OrderStatus> GetOrderStatus(string Id)
        {
            var responce = await _client.GetAsync($"/Order/order-get-status?IdOrder={Id}");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<OrderStatus>(content);
            return result;
        }
        public async Task<OrderDBRepository> GetOrder(string id)
        {
            var responce = await _client.GetAsync($"/Order/get-order?IdOrder={id}");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<OrderDBRepository>(content);
            return result;
        }
        public async Task<ListDish> GetListCategoryDishes()
        {
            var responce = await _client.GetAsync($"/Order/list-category-dishes");

            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<ListDish>(content);
            return result;
        }
        public async Task<Dishes> GetDish(string Id)
        {
            var responce = await _client.GetAsync($"/Order/dish-by-id?id={Id}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Dishes>(content);

            return result;
        }
        public async Task<Drinks> GetCoctail(string Id)
        {
            var responce = await _client.GetAsync($"/Order/coctail-by-id?id={Id}");

                var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Drinks>(content);

            return result;
        }

       
    }
}
