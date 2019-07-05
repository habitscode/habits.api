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
            Console.WriteLine("Entré acá");
            _dbClient = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USWest2);
        }

        public BaseRepository(IAmazonDynamoDB client)
        {
            Console.WriteLine("O entré por acá");
            _dbClient = client;
        }
    }
}
