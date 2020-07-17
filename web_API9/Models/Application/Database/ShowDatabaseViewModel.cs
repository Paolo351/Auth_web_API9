using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace web_API9.Models.Application.Database
{ 
    public class ShowDatabaseViewModel
    {
        

        public SelectList SDatabaselist { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DatabaseId { get; set; }

        public string Name { get; set; }

        public string Engine { get; set; }




    }
}
