using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace web_API9.Models.Application.Deployment
{
    public class ShowDeploymentViewModel
    {
        public SelectList SProjectlist { get; set; }

        public SelectList SDatabaselist { get; set; }

        public SelectList SUserlist { get; set; }

        public SelectList SDeploymentlist { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DeploymentId { get; set; }

        public string Name { get; set; }

        public string DeployMode { get; set; }

        public DateTime PlannedTimeOfDeployment { get; set; }

        public DateTime TimeOfDeployment { get; set; }

        public string Details { get; set; }

        public Boolean HasBeenDeployed { get; set; }

        public string AttachedFeatureDescription { get; set; }

        public string SchemaContent { get; set; }

        public string TargetDbId { get; set; }

        public string SchemaCreatedByUserId { get; set; }

        public string AttachedToProjectId { get; set; }

    }
}
