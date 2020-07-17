using AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace web_API9.Models.Application.User
{
    public class UserWithIdentity : MongoIdentityUser
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        //[BsonId]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        //public string PasswordHash { get; set; }

        //public string Email { get; set; }

        public UserRole Role { get; set; }

        public UserWithIdentity(User input)
        {
            //this.Id = input.UserId;
            this.UserId = this.Id;

            this.FirstName = input.FirstName;
            this.LastName = input.LastName;
            this.FullName = String.Concat(input.FirstName, " ", input.LastName);
            this.PasswordHash = input.PasswordHash;
            this.Email = input.Email;
            this.Role = input.Role;
            this.UserName = String.Concat(input.FirstName, ".", input.LastName);
            

        }

    }
}
