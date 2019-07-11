using System;
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
    class TeamMemberHandler
    {
        private ITeamMembersService ITeamMembersService { get; }

        public TeamMemberHandler()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            ITeamMembersService = serviceProvider.GetService<ITeamMembersService>();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITeamMembersService, TeamMembersService>();
            serviceCollection.AddScoped<ITeamMembersRepository, TeamMembersRepository>();
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

            var team = JsonConvert.DeserializeObject<TeamMember>(request.Body);
            await ITeamMembersService.AddAsync(team);

            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = "Team member was saved successfully"
            };
        }

        private bool validPayload(string body)
        {
            return true;
        }
    }
}
