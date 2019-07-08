﻿using System.Collections.Generic;
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
    public class TaskHandler
    {
        private ITaskService ITaskService { get; }

        public TaskHandler() {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            ITaskService = serviceProvider.GetService<ITaskService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITaskService, TaskService>();
            serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
        }

        public async Task<APIGatewayProxyResponse> Add(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body)) {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            HTask task = JsonConvert.DeserializeObject<HTask>(request.Body);
            await ITaskService.AddAsync(task);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was saved successfully"
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

            var team = JsonConvert.DeserializeObject<Team>(request.Body);
            var item = await ITaskService.GetItem(team.TeamId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Delete(APIGatewayProxyRequest request)
        {
            if (!validPayload(request.Body))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            var task = JsonConvert.DeserializeObject<HTask>(request.Body);
            await ITaskService.DeleteAsync(task);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was deleted successfully"
            };
        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
