using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Habits.Domain.Models;

namespace Habits.Domain.Repositories
{
    public class HabitRepository :  BaseRepository, IHabitRepository
    {
        public HabitRepository() : base() { }
        public HabitRepository(IAmazonDynamoDB client) : base(client) { }

        public async Task<List<Habit>> GetItems(String teamId)
        {
            var request = new QueryRequest()
            {
                TableName = Constants.HabitTableName,
                KeyConditionExpression = "TeamId = :teamId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":teamId", new AttributeValue(){ S = teamId } }
                }
            };

            var result = await _dbClient.QueryAsync(request);

            if (result.Count > 0)
            {
                var items = new List<Habit>();
                foreach (var item in result.Items)
                {
                    items.Add(GetItem(item));
                }
                return items;
            }
            else return null;
        }

        public async Task<Habit> GetItem(string teamId, string habitId)
        {
            var request = new GetItemRequest()
            {
                TableName = Constants.HabitTableName,
                Key = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = teamId } },
                    { "HabitId", new AttributeValue(){ S = habitId } }
                }
            };

            var result = await _dbClient.GetItemAsync(request);

            if (result.Item != null)
                return GetItem(result.Item);
            else
                return null;
        }

        public async Task AddAsync(Habit item)
        {
            var request = new PutItemRequest()
            {
                TableName = Constants.HabitTableName,
                Item = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "HabitId", new AttributeValue(){ S = item.HabitId } },
                    { "Name", new AttributeValue(){ S = item.Name } },
                    { "StartDate", new AttributeValue(){ S = item.StartDate.ToString() } },
                    { "EndDate", new AttributeValue(){ S = item.EndDate.ToString() } },
                    { "Status", new AttributeValue(){ S = item.Status.ToString() } },
                    { "Notes", new AttributeValue(){ S = item.Notes } }
                }
            };

            await _dbClient.PutItemAsync(request);
        }

        public async Task UpdateAsync(Habit item)
        {
            var request = new UpdateItemRequest()
            {
                TableName = Constants.HabitTableName,
                Key = new Dictionary<string, AttributeValue>() {
                    { "TeamId", new AttributeValue(){ S = item.TeamId } },
                    { "HabitId", new AttributeValue(){ S = item.HabitId } }
                },
                UpdateExpression = "set #Name = :Name, StartDate = :StartDate, EndDate = :EndDate, #Status = :Status, Notes = :Notes",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
                    { ":Name", new AttributeValue(){ S = item.Name } },
                    { ":StartDate", new AttributeValue(){ S = item.StartDate.ToString() } },
                    { ":EndDate", new AttributeValue(){ S = item.EndDate.ToString()} },
                    { ":Status", new AttributeValue(){ S = item.Status.ToString() } },
                    { ":Notes", new AttributeValue(){ S = item.Notes } }
                },
                ExpressionAttributeNames = new Dictionary<string, string>() {
                    { "#Name", "Name" },
                    { "#Status", "Status" }
                }
            };

            await _dbClient.UpdateItemAsync(request);
        }

        public async Task DeleteAsync(string teamId, string habitId)
        {
            var request = new DeleteItemRequest()
            {
                TableName = Constants.HabitTableName,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "TeamId", new AttributeValue(){ S = teamId } },
                    { "HabitId", new AttributeValue(){ S = habitId } }
                }
            };

            await _dbClient.DeleteItemAsync(request);
        }

        private Habit GetItem(Dictionary<string, AttributeValue> item)
        {
            //CultureInfo ci = new CultureInfo("en-US");

            var habit = new Habit()
            {
                TeamId = item["TeamId"].S,
                HabitId = item["HabitId"].S,
                Name = item["Name"].S,
                StartDate = Convert.ToDateTime(item["StartDate"].S),
                EndDate = Convert.ToDateTime(item["EndDate"].S),
                Status = (Status)Enum.Parse(typeof(Status), item["Status"].S),
                Notes = item["Notes"].S
            };

            return habit;
        }
    }
}
