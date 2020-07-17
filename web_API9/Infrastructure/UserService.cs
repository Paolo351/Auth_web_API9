using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models.Application.Database;
using web_API9.Models.Application.Deployment;
using web_API9.Models.Application.Project;
using web_API9.Models.Application.User;


namespace web_API9.Infrastructure
{ 
    public class Userservice
    {
        private readonly IMongoCollection<User> _Users;

        public Userservice(IMongoBDO settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<User>(settings.CollectionName_user);
        }


        public List<User> Get() =>
            _Users.Find(User => true).ToList();


        public User Create(User User)
        {
            _Users.InsertOne(User);
            return User;
        }

        public User Get(string id) =>
           _Users.Find<User>(User => User.UserId == id).FirstOrDefault();

        public void Remove(string id) =>
            _Users.DeleteOne(User => User.UserId == id);

    }
}
