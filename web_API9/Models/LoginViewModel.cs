using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace web_API9.Models
{ 
    public class LoginViewModel
    {
        

        
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        

    }
}
