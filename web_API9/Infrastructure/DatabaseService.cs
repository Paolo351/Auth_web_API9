using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models.Application.Database;



namespace web_API9.Infrastructure
{
    public class DatabaseService
    {
        private readonly IMongoCollection<Database> _Databases;

        
        public DatabaseService(IMongoBDO settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Databases = database.GetCollection<Database>(settings.CollectionName_db);
        }

        public List<Database> Get() =>
            _Databases.Find(database => true).ToList();

        
        public Database Get(string id) =>
           _Databases.Find<Database>(database => database.DatabaseId == id).FirstOrDefault();

        public Database Create(Database database)
        {
            _Databases.InsertOne(database);
            return database;
        }

        public void Update(string id, Database databaseIn) =>
            _Databases.ReplaceOne(student => student.DatabaseId == id, databaseIn);

        public void Remove(Database databaseIn) =>
            _Databases.DeleteOne(database => database.DatabaseId == databaseIn.DatabaseId);

        public void Remove(string id) =>
            _Databases.DeleteOne(database => database.DatabaseId == id);
    }
}
