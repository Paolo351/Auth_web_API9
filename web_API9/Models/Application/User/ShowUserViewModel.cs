using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace web_API9.Models.Application.User
{
    public class ShowUserViewModel
    {
        

        public SelectList SUserlist { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }




    }
}
