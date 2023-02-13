using Contracts.IService;
using Entities.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service :IServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public Service(IHttpContextAccessor httpContextAccessor,IHttpClientFactory httpClientFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public ErrorDto ModelState(ModelStateDictionary ModelState)
        {
            return new ErrorDto
            {
                ErrorMessage = ModelState.Keys.FirstOrDefault(),
                Description = ModelState.Values.Select(src => src.Errors.Select(src => src.ErrorMessage).FirstOrDefault()).FirstOrDefault(),
                StatusCode = 400

            };
        }
        /// <summary>
        ///checks user 
        /// </summary>
        public Guid Usercheck()
        {
            return new Guid(_httpContextAccessor.HttpContext.User?.FindFirstValue("userid"));
        }
        public void GetUserId(Guid userId)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://localhost:44307");

            HttpResponseMessage response = client.GetAsync($"/api/interservice/{userId}").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException("No user found with this id");
            }
        }

    }
}
