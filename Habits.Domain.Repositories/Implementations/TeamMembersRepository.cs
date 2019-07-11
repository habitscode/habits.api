using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public class TeamMembersRepository : BaseRepository, ITeamMembersRepository
    {
        public TeamMembersRepository(IAmazonDynamoDB client) : base(client) { }

        public async Task AddAsync(TeamMember item)
        {
            var request = new PutItemRequest()
            {
                TableName = Constants.TeamMembersTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "MemberId", new AttributeValue(){ S = item.MemberId } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public async Task DeleteAsync(TeamMember item)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.TaskTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "MemberId", new AttributeValue(){ S = item.MemberId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }
    }
}
