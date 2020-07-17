using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_API9.Infrastructure;
using web_API9.Models.Application.Project;

namespace web_API9.Models.Application.Deployment
{
    public class DeploymentToDisplay
    {
        private readonly ProjectService _ProjectService;
        private readonly DatabaseService _DatabaseService;
        private readonly Userservice _Userservice;

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

        public string Nazwa_bd { get; set; }

        public string SchemaCreatedByUserId { get; set; }

        public string Nazwa_user { get; set; }

        public string AttachedToProjectId { get; set; }

        public string Nazwa_project { get; set; }


        public DeploymentToDisplay(Deployment document, ProjectService ProjectService, DatabaseService DatabaseService, Userservice Userservice)
        {
            _ProjectService = ProjectService;
            _DatabaseService = DatabaseService;
            _Userservice = Userservice;

            var _project = _ProjectService.Get(document.AttachedToProjectId);
            var project_string = _project.Name;
            var _db = _DatabaseService.Get(document.TargetDbId);
            var db_string = _db.Name;
            var _user = _Userservice.Get(document.SchemaCreatedByUserId);
            var user_string = _user.FullName;

            this.DeploymentId = document.DeploymentId;
            this.Name = document.Name;
            this.DeployMode = document.DeployMode;
            this.PlannedTimeOfDeployment = document.PlannedTimeOfDeployment;
            this.TimeOfDeployment = document.TimeOfDeployment;
            this.Details = document.Details;
            this.HasBeenDeployed = document.HasBeenDeployed;
            this.AttachedFeatureDescription = document.AttachedFeatureDescription;
            this.SchemaContent = document.SchemaContent;
            this.TargetDbId = document.TargetDbId;
            this.Nazwa_bd = db_string;
            this.SchemaCreatedByUserId = document.SchemaCreatedByUserId;
            this.Nazwa_user = user_string;
            this.AttachedToProjectId = document.AttachedToProjectId;
            this.Nazwa_project = project_string;
        }

    }
}
