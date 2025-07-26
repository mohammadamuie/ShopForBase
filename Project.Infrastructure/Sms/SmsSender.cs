using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Models;
using System.Net.Http;
using System.Text.Encodings.Web;

namespace Project.Infrastructure.Sms
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsSettings _smsSettings;
        private readonly RestClient client;
        private readonly IHttpClientFactory _clientFactory;
        public SmsSender(IOptions<SmsSettings> smsSettings, IHttpClientFactory clientFactory)
        {
            _smsSettings = smsSettings.Value;
            client = new RestClient(_smsSettings.ApiUrl);
            _clientFactory = clientFactory;
        }

        public async Task<bool> SendWithPattern(SmsWithPattern smsWithPattern)
        {
            var request = new RestRequest
            {
                Method = Method.POST
            };
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter(
                "undefined",
                JsonConvert.SerializeObject(new
                {
                    op = "pattern",
                    user = _smsSettings.Username,
                    pass = _smsSettings.Password,
                    fromNum = _smsSettings.FromNumber,
                    toNum = smsWithPattern.To,
                    patternCode = smsWithPattern.PatternCode,
                    inputData = JsonConvert.SerializeObject(smsWithPattern.PatternData)
                }),
                ParameterType.RequestBody);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await client.ExecuteAsync(request, cancellationTokenSource.Token);

            return true;
        }

        public async Task SendWithPatternAsync(string phone, string patternCode, string data)
        {
            await CallApi(phone, patternCode, data);
        }

        private async Task CallApi(string phone, string patternCode, string inputData)
        {
            try
            {
                var number = phone.Insert(0, "98").Remove(2, 1);
                string from = "+983000505";
                string userName = "caropastry1";
                string pass = "1qaz2wsx!QAZ@WSX";
                string patternCodee = patternCode;
                string to = JsonConvert.SerializeObject(new string[] { phone });
                string input_data = inputData;

                string url = $@"http://188.0.240.110/patterns/pattern?username={userName}&password={UrlEncoder.Default.Encode(pass)}&from={from}&to={to}&input_data={UrlEncoder.Default.Encode(input_data)}&pattern_code={patternCodee}";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.Dispose();
            }
            catch
            {

            }
        }
        //private async Task CallApi(string phone, string patternCode, string inputData)
        //{
        //    try
        //    {
        //        string url = $@"https://RayganSMS.com/SendMessageWithUrl.ashx?Username=caropastry02&Password=mmbb5152&PhoneNumber=5000299557686&MessageBody={inputData}&RecNumber={phone}&Smsclass=1";
        //        var request = new HttpRequestMessage(HttpMethod.Get, url);
        //        var client = _clientFactory.CreateClient();
        //        var response = await client.GetAsync(url);
        //        response.Dispose();
        //    }
        //    catch
        //    {

        //    }
        //}
        public bool SimpleSend(SimpleSms simpleSms)
        {
            return true;
        }

        public bool SimpleSend(string to, string content)
        {
            return true;
        }

    }
}
