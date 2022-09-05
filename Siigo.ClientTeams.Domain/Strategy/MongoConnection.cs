using MongoDB.Driver;
using Siigo.ClientTeams.Domain.Models;
using MongoDB.Bson;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Siigo.ClientTeams.Domain.Strategy
{
    public class MongoConnection : ConnectionBase, IDatabaseStrategy
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("MongoDBAtlasConnectionString");

        public MongoConnection(MessageBody messageBody) : base(messageBody)
        {
        }

        public IEnumerable<Fact> BuildFacts()
        {

            var client = new MongoClient(connectionString);
            var odatabase = client.GetDatabase(messageBody.Database);
            var ocollection = odatabase.GetCollection<BsonDocument>(messageBody.Collection);
            FilterDefinition<BsonDocument> ofilter = BuildFilters(messageBody);
            var document = ocollection.Find(ofilter).FirstOrDefault();

            if (document == null)
                return facts;

            var jsonString = BsonTypeMapper.MapToDotNetValue(document);
            object jsonObject = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(jsonString));


            return GetFacts(jsonObject);
        }

        private static FilterDefinition<BsonDocument> BuildFilters(MessageBody data)
        {
            FilterDefinition<BsonDocument> filters = data?.Filters != null && data?.Filters.Length > 0 ? null : Builders<BsonDocument>.Filter.Empty;

            foreach (var filter in data?.Filters)
            {
                if (filters == null)
                {
                    filters = Builders<BsonDocument>.Filter.Eq(filter.Field, filter.Value);
                }
                else
                {
                    filters = Builders<BsonDocument>.Filter.And(filters, Builders<BsonDocument>.Filter.Eq(filter.Field, filter.Value));
                }
            }

            return filters;
        }

    }

}
