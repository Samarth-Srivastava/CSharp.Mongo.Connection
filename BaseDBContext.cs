using MongoDB.Driver;


namespace CSharp.Mongo.Connection{
    public class BaseDBConetxt{

        protected readonly IMongoClient _mongoClient;
        public IMongoDatabase _mongoDatabase;
        public BaseDBConetxt(IMongoClient mongoClient, string databaseName)
        {
            this._mongoClient = mongoClient;
            this._mongoDatabase = mongoClient.GetDatabase(databaseName);
        }
    }

}