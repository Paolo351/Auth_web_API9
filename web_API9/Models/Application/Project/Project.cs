using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_API9.Models.Application.Project
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public Project(string Name_wej)
        {
            this.ProjectId = "";

            this.Name = Name_wej;
            

        }

    }
}
