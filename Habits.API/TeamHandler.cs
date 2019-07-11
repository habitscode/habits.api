﻿using System;
using System.Collections.Generic;
using System.Net;
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
            if (!validPayload(request.Body)) {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

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
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var team = JsonConvert.DeserializeObject<Team>(request.Body);
            await ITeamService.AddAsync(team);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was saved successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Update(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var team = JsonConvert.DeserializeObject<Team>(request.Body);
            await ITeamService.UpdateAsync(team);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was updated successfully"
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

            string teamId;

            if (request.PathParameters == null || !request.PathParameters.TryGetValue("teamId", out teamId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add teamId"
                };
            }

            await ITeamService.DeleteAsync(teamId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team was deleted successfully"
            };
        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
