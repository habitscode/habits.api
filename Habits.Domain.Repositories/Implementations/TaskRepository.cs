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

        public async Task<List<HTask>> GetItems(String habitId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.TaskTableName,
                KeyConditionExpression = "HabitId = :habitId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":habitId", new AttributeValue(){ S = habitId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
            {
                var items = new List<HTask>();
                foreach (var item in result.Items)
                {
                    items.Add(GetItem(item));
                }
                return items;
            }
            else return null;
        }

        public async Task<HTask> GetItem(string habitId, string taskId)
        {
            var request = new GetItemRequest() {
                TableName = Constants.TaskTableName,
                Key = new Dictionary<string, AttributeValue>() {
                    { "HabitId", new AttributeValue(){ S = habitId } },
                    { "TaskId", new AttributeValue(){ S = taskId } }
                }
            };

            var result = await _dbClient.GetItemAsync(request);

            if (result.Item != null)
                return GetItem(result.Item);
            else
                return null;
        }

        public async Task AddAsync(HTask item)
        {
            var request = new PutItemRequest() {
                TableName = Constants.TaskTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "HabitId", new AttributeValue(){ S = item.HabitId } },
                    { "TaskId", new AttributeValue(){ S = item.TaskId } },
                    { "What", new AttributeValue(){ S = item.What } },
                    { "When", new AttributeValue(){ S = item.When.ToString() } },
                    { "Where", new AttributeValue(){ S = item.Where } },
                    { "Status", new AttributeValue(){ S = item.Status.ToString() } },
                    { "Notes", new AttributeValue(){ S = item.Notes.ToString() } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public async Task UpdateAsync(HTask item)
        {
            var request = new UpdateItemRequest()
            {
                TableName = Constants.TaskTableName,
                Key = new Dictionary<string, AttributeValue>() {
                    { "HabitId", new AttributeValue(){ S = item.HabitId } },
                    { "TaskId", new AttributeValue(){ S = item.TaskId } }
                },
                UpdateExpression = "set What = :What, #Where = :Where, #When = :When, #Status = :Status, Notes = :Notes",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":What", new AttributeValue(){ S = item.What } },
                    { ":Where", new AttributeValue(){ S = item.Where } },
                    { ":When", new AttributeValue(){ S = item.When.ToString() } },
                    { ":Status", new AttributeValue(){ S = item.Status.ToString() } },
                    { ":Notes", new AttributeValue(){ S = item.Notes } }
                },
                ExpressionAttributeNames = new Dictionary<string, string>() {
                    { "#Where", "Where" },
                    { "#When", "When" },
                    { "#Status", "Status" }
                }
            };

            await _dbClient.UpdateItemAsync(request);
        }

        public async Task DeleteAsync(string habitId, string taskId)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.TaskTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "HabitId", new AttributeValue(){ S = habitId } },
                    { "TaskId", new AttributeValue(){ S = taskId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }

        private HTask GetItem(Dictionary<string, AttributeValue> item)
        {
            var task = new HTask()
            {
                HabitId = item["HabitId"].S,
                TaskId = item["TaskId"].S,
                Status = (Status)Enum.Parse(typeof(Status), item["Status"].S),
                What = item["What"].S,
                Where = item["Where"].S,
                When = Convert.ToDateTime(item["When"].S),
                Notes = item["Notes"].S
            };

            return task;
        }
    }
}
