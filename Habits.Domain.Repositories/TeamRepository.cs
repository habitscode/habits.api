using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository() : base() { }
        public TeamRepository(IAmazonDynamoDB client) : base(client) { }

        public async Task<List<Team>> GetAll()
        {
            var request = new ScanRequest()
            {
                TableName = Constants.TeamTableName
            };

            var result = await _dbClient.ScanAsync(request);

            return GetItems(result.Items);
        }

        public async Task<Team> GetItem(String teamId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.TeamTableName,
                KeyConditionExpression = "TeamId = :teamId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":teamId", new AttributeValue(){ S = teamId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
                return GetItem(result.Items[0]);
            else
                return null;
        }

        public async Task AddAsync(Team item)
        {
            var request = new PutItemRequest()
            {
                TableName = Constants.TeamTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "Name", new AttributeValue(){ S = item.Name } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public Task UpdateAsync(Team item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(String teamId)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.TeamTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "TeamId", new AttributeValue(){ S = teamId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }

        private Team GetItem(Dictionary<string, AttributeValue> item)
        {
            var team = new Team()
            {
                TeamId = item["TeamId"].S,
                Name = item["Name"].S
            };

            return team;
        }

        private List<Team> GetItems(List<Dictionary<string, AttributeValue>> list)
        {
            if (list.Count > 0)
            {
                var items = new List<Team>();
                foreach (var item in list)
                {
                    items.Add(GetItem(item));
                }
                return items;
            }
            else return null;
        }
    }
}
