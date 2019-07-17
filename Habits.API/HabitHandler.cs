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
            if (!validPathParameters(request.PathParameters, out string teamId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
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
            if (!validPathParameters(request.PathParameters, out string teamId, out string habitId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
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
            if (!validPathParameters(request.PathParameters, out string teamId, out string error) ||
                !validPayload(request.Body, out Habit habit, out error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

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
            if (!validPathParameters(request.PathParameters, out string teamId, out string error) ||
                !validPayload(request.Body, out Habit habit, out error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            habit.TeamId = teamId;
            await IHabitService.UpdateAsync(habit);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Habit was updated successfully"
            };
        }

        public async Task<APIGatewayProxyResponse> Delete(APIGatewayProxyRequest request) {
            if (!validPathParameters(request.PathParameters, out string teamId, out string habitId, out string error))
            {
                return new APIGatewayProxyResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = error
                };
            }

            await IHabitService.DeleteAsync(teamId, habitId);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Habit was deleted successfully"
            };
        }

        #region Validations
        private bool validPayload(string body, out Habit habit, out string error)
        {
            try
            {
                habit = JsonConvert.DeserializeObject<Habit>(body);
                error = string.Empty;
                return true;
            }
            catch (JsonException ex)
            {
                error = "Invalid payload, please use payload valid, error: " + ex.Message;
                habit = null;
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

        private bool validPathParameters(IDictionary<string, string> pathParameters, out string teamId, out string habitId, out string error)
        {
            if (pathParameters == null || !pathParameters.TryGetValue("teamId", out teamId))
            {
                error = "Invalid query string, please add teamId";
                teamId = habitId = string.Empty;
                return false;
            }

            if (!pathParameters.TryGetValue("habitId", out habitId))
            {
                error = "Invalid query string, please add habitId";
                return false;
            }

            error = string.Empty;
            return true;
        }
        #endregion
    }
}