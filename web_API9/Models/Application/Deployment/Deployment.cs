using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Models.Application.Project;

namespace web_API9.Models.Application.Deployment
{
    public class Deployment
    {

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

        

        public Deployment(string name_input, string deployMode_input, DateTime plannedTimeOfDeployment_input, DateTime timeOfDeployment_input, string details_input, Boolean hasBeenDeployed_input,
            string attachedFeatureDescription_input, string schemaContent_input, string targetDbId_input, string schemaCreatedByUserId_input, string attachedToProjectId_input)
        {
            this.DeploymentId = "";
            this.Name = name_input;
            this.DeployMode = deployMode_input;
            this.PlannedTimeOfDeployment = plannedTimeOfDeployment_input;
            this.TimeOfDeployment = timeOfDeployment_input;
            this.Details = details_input;
            this.HasBeenDeployed = hasBeenDeployed_input;
            this.AttachedFeatureDescription = attachedFeatureDescription_input;
            this.SchemaContent = schemaContent_input;
            this.TargetDbId = targetDbId_input;
            this.SchemaCreatedByUserId = schemaCreatedByUserId_input;
            this.AttachedToProjectId = attachedToProjectId_input;
            
        }

        
    }
}
