using System;
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

        public async Task<APIGatewayProxyResponse> GetAll(APIGatewayProxyRequest request)
        {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            if (request.PathParameters == null || !request.PathParameters.TryGetValue("teamId", out string teamId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add teamId"
                };
            }

            var items = await IChallengeService.GetItems(teamId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(items, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request)
        {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            if (request.PathParameters == null || !request.PathParameters.TryGetValue("teamId", out string teamId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add teamId"
                };
            }

            if (!request.PathParameters.TryGetValue("challengeId", out string challengeId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add challengeId"
                };
            }

            var item = await IChallengeService.GetItem(teamId, challengeId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
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

            if (request.PathParameters == null || !request.PathParameters.TryGetValue("teamId", out string teamId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add teamId"
                };
            }

            var challenge = JsonConvert.DeserializeObject<Challenge>(request.Body);
            challenge.TeamId = teamId;
            await IChallengeService.AddAsync(challenge);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Challenge was saved successfully"
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

            if (request.PathParameters == null || !request.PathParameters.TryGetValue("teamId", out string teamId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add teamId"
                };
            }

            if (!request.PathParameters.TryGetValue("challengeId", out string challengeId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add challengeId"
                };
            }

            await IChallengeService.DeleteAsync(teamId, challengeId);

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
