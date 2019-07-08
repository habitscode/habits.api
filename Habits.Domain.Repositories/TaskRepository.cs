using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository() : base() { }
        public TaskRepository(IAmazonDynamoDB client) : base(client) { }

        public async Task AddAsync(HTask item)
        {
            var request = new PutItemRequest() {
                TableName = Constants.TaskTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "ChallengeId", new AttributeValue(){ S = item.ChallengeId } },
                    { "TaskId", new AttributeValue(){ S = item.TaskId } },
                    { "What", new AttributeValue(){ S = item.What } },
                    { "When", new AttributeValue(){ S = item.When.ToString() } },
                    { "Where", new AttributeValue(){ S = item.Where } },
                    { "Status", new AttributeValue(){ S = item.Status.ToString() } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public async Task DeleteAsync(HTask item)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.TaskTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "ChallengeId", new AttributeValue(){ S = item.ChallengeId } },
                    { "TaskId", new AttributeValue(){ S = item.TaskId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }

        private HTask GetItem(Dictionary<string, AttributeValue> item)
        {
            var task = new HTask()
            {
                ChallengeId = item["ChallengeId"].S,
                TaskId = item["TaskId"].S,
                Status = (Status)Enum.Parse(typeof(Status), item["Status"].S),
                What = item["What"].S,
                Where = item["Where"].S,
                When = Convert.ToDateTime(item["When"].S)
            };

            return task;
        }

        public async Task<HTask> GetItem(String taskId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.TaskTableName,
                KeyConditionExpression = "TaskId = :taskId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":taskId", new AttributeValue(){ S = taskId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
                return GetItem(result.Items[0]);
            else
                return null;
        }

        public async Task<List<HTask>> GetItems(String challengeId) {
            var request = new QueryRequest()
            {
                TableName = Constants.TaskTableName,
                KeyConditionExpression = "ChallengeId = :challengeId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":challengeId", new AttributeValue(){ S = challengeId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0) {
                var items = new List<HTask>();
                foreach (var item in result.Items) {
                    items.Add(GetItem(item));
                }
                return items;
            } else return null;
        }



        public Task UpdateAsync(HTask item)
        {
            throw new NotImplementedException();
        }
    }
}
