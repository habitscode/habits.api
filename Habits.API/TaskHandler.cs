using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Habits.Domain.Models;
using Habits.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Habits.API
{
    public class TaskHandler
    {
        private ITaskService TaskService { get; }


        public TaskHandler() {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            TaskService = serviceProvider.GetService<ITaskService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaskService, TaskService>();
        }

        public APIGatewayProxyResponse AddTask(APIGatewayProxyRequest request) {
            if (!validPayload(request.Body)) {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid payload, please use payload valid"
                };
            }

            HTask task = JsonConvert.DeserializeObject<HTask>(request.Body);
            TaskService.Add(task);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was saved successfully"
            };

        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
