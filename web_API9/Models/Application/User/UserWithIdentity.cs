using AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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

        public UserWithIdentity(string FirstName, string LastName, string Email, UserRole _Role)
        {
        this.UserId = this.Id;

        this.FirstName = FirstName;
        this.LastName = LastName;
        this.FullName = String.Concat(FirstName, " ", LastName);
        this.UserName = String.Concat(FirstName.ToLower(), ".", LastName.ToLower());
            //this.PasswordHash = PasswordHash;
        this.Email = Email;
        this.Role = _Role;

            string z = "";

            switch ((int)_Role)
            {
                case 0: z = "Admin"; break;
                case 1: z = "Schema_guard"; break;
                case 2: z = "Spectator"; break;
            }


            //var claimOne = new UserClaim(ClaimTypes.Name, String.Concat(FirstName.ToLower(), ".", LastName.ToLower()));
            //this.AddClaim(claimOne);

            var userClaims = new List<UserClaim>()
            {
                    new UserClaim("FirstName", FirstName),
                    new UserClaim("LastName", LastName),
                    new UserClaim("UserName", String.Concat(FirstName.ToLower(), ".", LastName.ToLower())),
                    new UserClaim("FullName", String.Concat(FirstName, " ", LastName)),
                    new UserClaim("Email", Email),
                    new UserClaim("Role", z) 
            };

            this.AddClaims(userClaims);
            this.AddRole(z);




        }

        public UserWithIdentity()
        {
            


        }

        //public UserWithIdentity(User input)
        //{
        //    //this.Id = input.UserId;
        //    this.UserId = this.Id;

        //    this.FirstName = input.FirstName;
        //    this.LastName = input.LastName;
        //    this.FullName = String.Concat(input.FirstName, " ", input.LastName);
        //    this.PasswordHash = input.PasswordHash;
        //    this.Email = input.Email;
        //    this.Role = input.Role;
        //    this.UserName = String.Concat(input.FirstName, ".", input.LastName);


        //}

    }
}
