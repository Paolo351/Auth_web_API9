using Microsoft.AspNetCore.Identity;
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
        private readonly IMongoCollection<UserWithIdentity> _Users;
        private readonly UserManager<UserWithIdentity> _userManager;

        public Userservice(IMongoBDO settings, UserManager<UserWithIdentity> userManager)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<UserWithIdentity>(settings.CollectionName_user);

            _userManager = userManager;
        }


        public List<UserWithIdentity> Get() =>
            _Users.Find(User => true).ToList();


        public UserWithIdentity Create(UserWithIdentity User, string PasswordHash)
        {
            //_Users.InsertOne(User);
            var result =  _userManager.CreateAsync(User, PasswordHash);

            if (result != null)
            {
                return User;

            } else return null;   
        }

        public UserWithIdentity Get(string id) =>
           _Users.Find<UserWithIdentity>(UserWithIdentity => UserWithIdentity.UserId == id).FirstOrDefault();

        public void Remove(string id) =>
            _Users.DeleteOne(UserWithIdentity => UserWithIdentity.UserId == id);

    }
}
