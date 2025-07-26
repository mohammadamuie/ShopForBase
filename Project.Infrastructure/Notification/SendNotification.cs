using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Project.Application.Models;
using Project.Application.Responses;
using RestSharp;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Notification
{
    public class SendNotification
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly string App_Id;
        private readonly string Token;

        public SendNotification(IHttpContextAccessor contextAccessor, HttpClient httpClient)
        {
            _contextAccessor = contextAccessor;
            App_Id = "353b0e71-5e25-4473-a4ef-95f5f37b4683";
            Token = "4d7288c6ca467b0f607dd32abcc2173f79c23092";
            _httpClient = httpClient;
        }

        public async Task SendNotificationAsync()
        {
            var requestUri = "https://app.najva.com/api/v2/notification/management/send-direct/";

            var requestBody = new
            {
                title = "title",
                body = "body",
                onclick_action = "<YOUR_ACTION>",
                url = "http://example.com",
                content = "some content",
                icon = "http://example.com/static/icon.png",
                image = "http://example.com/static/img.png",
                utm = new object(),
                json = "{\"key\":\"value\"}",
                phone_number = "",
                activity_package_name = "",
                light_up_screen = false,
                buttons = new[]
                {
                new { title = "title", url = "http://example.com", utm = new object() }
            },
                subscribers = new object[0]
            };

            var requestBodyJson = JsonConvert.SerializeObject(requestBody);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
            httpRequest.Headers.Add("Authorization", $"Token \"{Token}\"");
            httpRequest.Headers.Add("X-api-key", App_Id);
            httpRequest.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error here
                throw new Exception($"Failed to send notification. Status code: {response.StatusCode}");
            }

            // Handle success here
            // For example, you can read response content
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }

        public async Task SendCustomNotification(NotificationData input, DeviceFilters deviceFilters)
        {
            var client = new RestClient();
            var request = new RestRequest("https://app.najva.com/api/v2/notification/management/send-direct/");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Token " + Token);

            //create api body
            var modelbody = new SendFiterNotification();
            modelbody.app_ids = App_Id;
            modelbody.data = input;
            modelbody.filters = deviceFilters;
            var body = JsonConvert.SerializeObject(modelbody);
            request.AddBody(body, "application/json");
            await client.ExecutePostAsync(request);
        }


    }
}
