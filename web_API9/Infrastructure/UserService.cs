using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using web_API9.Models.Application.User;


namespace web_API9.Infrastructure
{ 
    public class Userservice
    { 
        private readonly IMongoCollection<UserWithIdentity> _Users;
        

        public Userservice(IMongoBDO settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<UserWithIdentity>(settings.CollectionName_user);

            
        }


        public List<UserWithIdentity> Get() =>
            _Users.Find(User => true).ToList();


        //public UserWithIdentity Create(UserWithIdentity User)
        //{
        //    _Users.InsertOne(User);
            
        //     return User;   
        //}

        public UserWithIdentity Get(string id) =>
           _Users.Find<UserWithIdentity>(UserWithIdentity => UserWithIdentity.UserId == id).FirstOrDefault();

        public void Remove(string id) =>
            _Users.DeleteOne(UserWithIdentity => UserWithIdentity.UserId == id);

    }
}
