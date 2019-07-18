using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Habits.Domain.Models;
using Habits.Domain.Repositories;
using Habits.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Habits.API
{
    public class TeamHandler
    {
        private ITeamService ITeamService { get; }

        public TeamHandler()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            ITeamService = serviceProvider.GetService<ITeamService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITeamService, TeamService>();
            serviceCollection.AddScoped<ITeamRepository, TeamRepository>();
        }

        public async Task<APIGatewayProxyResponse> GetAll(APIGatewayProxyRequest request) {
            var items = await ITeamService.GetAll();
            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(items, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request)
        {
            if (!validPathParameters(request.PathParameters, out string teamId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            var item = await ITeamService.GetItem(teamId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Add(APIGatewayProxyRequest request)
        {
            if (!validPayload(request.Body, out Team team, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            await ITeamService.AddAsync(team);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was saved successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Update(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body, out Team team, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            await ITeamService.UpdateAsync(team);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was updated successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Delete(APIGatewayProxyRequest request) {
            if (!validPathParameters(request.PathParameters, out string teamId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            await ITeamService.DeleteAsync(teamId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was deleted successfully"
            };
        }

        #region Validations
        private bool validPayload(string body, out Team team, out string error)
        {
            try
            {
                team = JsonConvert.DeserializeObject<Team>(body);
                error = string.Empty;
                return true;
            }
            catch (JsonException ex)
            {
                error = "Invalid payload, please use payload valid, error: " + ex.Message;
                team = null;
                return false;
            }
        }

        private bool validPathParameters(IDictionary<string, string> pathParameters, out string teamId, out string error)
        {
            if (pathParameters == null || !pathParameters.TryGetValue("teamId", out teamId))
            {
                error = "Invalid query string, please add teamId";
                teamId = string.Empty;
                return false;
            }

            error = string.Empty;
            return true;
        }
        #endregion
    }
}