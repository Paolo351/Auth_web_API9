using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace web_API9.Models.Application.User
{
    public class User 
    {
        

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public User(string FirstName, string LastName, string PasswordHash, string Email, UserRole Role)
        {
            this.UserId = "";// this.Id;

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.FullName = String.Concat(FirstName, " ", LastName);
            this.PasswordHash = PasswordHash;
            this.Email = Email;
            this.Role = Role;


        }

    }
}
