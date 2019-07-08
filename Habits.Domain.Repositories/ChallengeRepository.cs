using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public class ChallengeRepository :  BaseRepository, IChallengeRepository
    {
        public ChallengeRepository() : base() { }
        public ChallengeRepository(IAmazonDynamoDB client) : base(client) { }

        public async Task AddAsync(Challenge item)
        {
            var request = new PutItemRequest()
            {
                TableName = Constants.ChallengeTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "ChallengeId", new AttributeValue(){ S = item.ChallengeId } },
                    { "Name", new AttributeValue(){ S = item.Name } },
                    { "StartDate", new AttributeValue(){ S = item.StartDate.ToString() } },
                    { "EndDate", new AttributeValue(){ S = item.EndDate.ToString() } },
                    { "Status", new AttributeValue(){ S = item.Status.ToString() } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public async Task DeleteAsync(Challenge item)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.ChallengeTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "ChallengeId", new AttributeValue(){ S = item.ChallengeId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }

        public async Task<Challenge> GetItem(String challengeId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.ChallengeTableName,
                KeyConditionExpression = "ChallengeId = :challengeId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":challengeId", new AttributeValue(){ S = challengeId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
                return GetItem(result.Items[0]);
            else
                return null;
        }

        private Challenge GetItem(Dictionary<string, AttributeValue> item)
        {
            var challenge = new Challenge()
            {
                TeamId = item["TeamId"].S,
                ChallengeId = item["ChallengeId"].S,
                Name = item["Name"].S,
                StartDate = Convert.ToDateTime(item["StartDate"].S),
                EndDate = Convert.ToDateTime(item["EndDate"].S),
                Status = (Status)Enum.Parse(typeof(Status), item["Status"].S)
            };

            return challenge;
        }

        public async Task<List<Challenge>> GetItems(String teamId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.ChallengeTableName,
                KeyConditionExpression = "TeamId = :teamId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":teamId", new AttributeValue(){ S = teamId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
            {
                var items = new List<Challenge>();
                foreach (var item in result.Items)
                {
                    items.Add(GetItem(item));
                }
                return items;
            }
            else return null;
        }

        public Task UpdateAsync(Challenge item)
        {
            throw new NotImplementedException();
        }
    }
}
