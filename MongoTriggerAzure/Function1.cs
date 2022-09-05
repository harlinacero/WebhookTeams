using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
//using MongoTriggerAzure.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using Siigo.ClientTeams.Domain.Strategy;
using Siigo.ClientTeams.Domain.Models;
//using MongoTriggerAzure.Strategy;

namespace MongoTriggerAzure
{
    public static class Function1
    {
        [FunctionName("TriggerMongo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                string requestBody = String.Empty;
                using (StreamReader streamReader = new(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }
                var data = JsonConvert.DeserializeObject<MessageBody>(requestBody);

                if (data.Database != null && data.Collection != null)
                {
                    data.BuildMessage();
                }

                var response = await SendMessageAsync(data);


                log.LogDebug("Permiss added");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Error trigger" + ex.Message);
            }
        }



        private static MessageBody BuildMessage(this MessageBody messageBody)
        {
            ContextDatabase contextDatabase = new();
            switch (messageBody.Provider)
            {
                case "Mongo":
                    contextDatabase.SetStrategy(new MongoConnection(messageBody));
                    break;

                case "SQLServer":
                    contextDatabase.SetStrategy(new CustomSqlConnection(messageBody));
                    break;

                default:
                    contextDatabase.SetStrategy(new MongoConnection(messageBody));
                    break;
            }
           
            var facts = contextDatabase.GetFacts();

            messageBody.Sections = messageBody.Sections.Any() ? messageBody.Sections : new Section[] {
                new Section()
            };

            messageBody.Sections.FirstOrDefault().Facts = facts.ToArray();

            return messageBody;
        }

        private static async Task<HttpStatusCode> SendMessageAsync(MessageBody messageBody)
        {
            string text = JsonConvert.SerializeObject(messageBody);
            //var url = Environment.GetEnvironmentVariable("urlTeamsChannel");
            var url = messageBody.UrlTeams;

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), url);
            request.Content = new StringContent(text);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await httpClient.SendAsync(request);
            return response.StatusCode;
        }
    }

}
