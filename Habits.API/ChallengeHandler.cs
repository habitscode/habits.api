using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Habits.Domain.Models;
using Habits.Domain.Repositories;
using Habits.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Habits.API
{
    public class ChallengeHandler
    {
        private IChallengeService IChallengeService { get; }

        public ChallengeHandler()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            IChallengeService = serviceProvider.GetService<IChallengeService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IChallengeService, ChallengeService>();
            serviceCollection.AddScoped<IChallengeRepository, ChallengeRepository>();
        }

        public async Task<APIGatewayProxyResponse> Add(APIGatewayProxyRequest request)
        {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var challenge = JsonConvert.DeserializeObject<Challenge>(request.Body);
            await IChallengeService.AddAsync(challenge);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Challenge was saved successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var challenge = JsonConvert.DeserializeObject<Challenge>(request.Body);
            var item = await IChallengeService.GetItem(challenge.ChallengeId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Delete(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var challenge = JsonConvert.DeserializeObject<Challenge>(request.Body);
            await IChallengeService.DeleteAsync(challenge);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Challenge was deleted successfully"
            };
        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
