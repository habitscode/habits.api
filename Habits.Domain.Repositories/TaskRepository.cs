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

        public void Delete(HTask item)
        {
            throw new NotImplementedException();
        }

        public HTask Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(HTask item)
        {
            throw new NotImplementedException();
        }
    }
}
