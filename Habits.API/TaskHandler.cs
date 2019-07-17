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

        public async Task<APIGatewayProxyResponse> GetAll(APIGatewayProxyRequest request)
        {
            if (!validPathParameters(request.PathParameters, out string habitId, out string error)) {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            var items = await ITaskService.GetItems(habitId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(items, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request)
        {
            if (!validPathParameters(request.PathParameters, out string habitId, out string taskId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            var item = await ITaskService.GetItem(habitId, taskId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item, JsonSerializerConfig.settings),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public async Task<APIGatewayProxyResponse> Add(APIGatewayProxyRequest request) {
            if (!validPathParameters(request.PathParameters, out string habitId, out string error) || 
                !validPayload(request.Body, out HTask task, out error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }
            
            task.HabitId = habitId;
            await ITaskService.AddAsync(task);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was saved successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Update(APIGatewayProxyRequest request)
        {
            if (!validPathParameters(request.PathParameters, out string habitId, out string error) ||
                !validPayload(request.Body, out HTask task, out error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            } 

            task.HabitId = habitId;
            await ITaskService.UpdateAsync(task);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was updated successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Delete(APIGatewayProxyRequest request)
        {
            if (!validPathParameters(request.PathParameters, out string habitId, out string taskId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            await ITaskService.DeleteAsync(habitId, taskId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Task was deleted successfully"
            };
        }

        #region Validations
        private bool validPayload(string body, out HTask task, out string error)
        {
            try
            {
                task = JsonConvert.DeserializeObject<HTask>(body);
                error = string.Empty;
                return true;
            }
            catch (JsonException ex)
            {
                error = "Invalid payload, please use payload valid, error: " + ex.Message;
                task = null;
                return false;
            }
        }

        private bool validPathParameters(IDictionary<string, string> pathParameters, out string habitId, out string error)
        {
            if (pathParameters == null || !pathParameters.TryGetValue("habitId", out habitId))
            {
                error = "Invalid query string, please add habitId";
                habitId = string.Empty;
                return false;
            }

            error = string.Empty;
            return true;
        }

        private bool validPathParameters(IDictionary<string, string> pathParameters, out string habitId, out string taskId, out string error)
        {
            if (pathParameters == null || !pathParameters.TryGetValue("habitId", out habitId))
            {
                error = "Invalid query string, please add habitId";
                habitId = taskId = string.Empty;
                return false;
            }

            if (!pathParameters.TryGetValue("taskId", out taskId))
            {
                error = "Invalid query string, please add taskId";
                return false;
            }

            error = string.Empty;
            return true;
        }
        #endregion
    }
}
