using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2;

namespace Habits.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IAmazonDynamoDB _dbClient;

        public BaseRepository()
        {
            _dbClient = new AmazonDynamoDBClient();
        }

        public BaseRepository(IAmazonDynamoDB client)
        {
            _dbClient = client;
        }
    }
}
