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
    public class HabitHandler
    {
        private IHabitService IHabitService { get; }

        public HabitHandler()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            IHabitService = serviceProvider.GetService<IHabitService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHabitService, HabitService>();
            serviceCollection.AddScoped<IHabitRepository, HabitRepository>();
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

            var items = await IHabitService.GetItems(teamId);

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

            if (!request.PathParameters.TryGetValue("habitId", out string habitId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add habitId"
                };
            }

            var item = await IHabitService.GetItem(teamId, habitId);

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

            var habit = JsonConvert.DeserializeObject<Habit>(request.Body);
            habit.TeamId = teamId;
            await IHabitService.AddAsync(habit);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Habit was saved successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Update(APIGatewayProxyRequest request)
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

            var habit = JsonConvert.DeserializeObject<Habit>(request.Body);
            habit.TeamId = teamId;
            await IHabitService.UpdateAsync(habit);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Habit was updated successfully"
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

            if (!request.PathParameters.TryGetValue("habitId", out string habitId))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid query string, please add habitId"
                };
            }

            await IHabitService.DeleteAsync(teamId, habitId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Habit was deleted successfully"
            };
        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
