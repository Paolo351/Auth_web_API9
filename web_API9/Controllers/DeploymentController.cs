using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_API9.Infrastructure;
using web_API9.Models.Application.Database;
using web_API9.Models.Application.Deployment;
using web_API9.Models.Application.Project;
using web_API9.Models.Application.User;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_API9.Controllers
{
    [Route("Deployment")]
    [ApiController]
    public class DeploymentController : Controller
    {

        private readonly ProjectService _ProjectService;
        private readonly DatabaseService _DatabaseService;
        private readonly DeploymentService _DeploymentService;
        private readonly Userservice _Userservice;

        public DeploymentController(DeploymentService DeploymentService, ProjectService ProjectService, DatabaseService DatabaseService, Userservice Userservice)
        {

            _DeploymentService = DeploymentService;
            _ProjectService = ProjectService;
            _DatabaseService = DatabaseService;
            _Userservice = Userservice;

        }

       
        [HttpGet("AddDeployment")]
        public IActionResult AddDeployment
            (string Name, string DeployMode, DateTime PlannedTimeOfDeployment, DateTime TimeOfDeployment, string Details, Boolean HasBeenDeployed, 
            string AttachedFeatureDescription, string SchemaContent, string TargetDbId, string SchemaCreatedByUserId, string AttachedToProjectId)
        {
            var document = new Deployment(Name, DeployMode, PlannedTimeOfDeployment, TimeOfDeployment, Details, HasBeenDeployed,
            AttachedFeatureDescription, SchemaContent, TargetDbId, SchemaCreatedByUserId, AttachedToProjectId);

            var deployment_list = new List<Deployment>();

            deployment_list.Add(_DeploymentService.Create(document));

            var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);

            var document_list_toDisplay = new List<DeploymentToDisplay>();

            document_list_toDisplay.Add(document_toDisplay);

            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = document_list_toDisplay
            };

            return View(viewModel);
        }


       
        [HttpGet("DelDeployment")]
        public IActionResult DelDeployment(string DeploymentId)
        {
            var document = _DeploymentService.Get(DeploymentId);
            if (document == null)
                return NotFound();

            var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);

            _DeploymentService.Remove(document.DeploymentId);

            var deployment_list = new List<DeploymentToDisplay>();

            deployment_list.Add(document_toDisplay);

            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = deployment_list
            };

            return View(viewModel);
        }


        
        [HttpGet("ShowDeployment")]
        public IActionResult ShowDeployment()
        {
            var lista_projektow = new List<SelectListItem>();
            var project_list = new List<Project>(_ProjectService.Get());
            foreach (var document in project_list)
            {
                lista_projektow.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.ProjectId });                   
            }
            var slist_project = new SelectList(lista_projektow, "Value", "Text");
            
            var lista_baz = new List<SelectListItem>();
            var db_list = new List<Database>(_DatabaseService.Get());
            foreach (var document in db_list)
            {
                lista_baz.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.DatabaseId });
            }
            var slist_database = new SelectList(lista_baz, "Value", "Text");
           
            var lista_userow = new List<SelectListItem>();
            var user_list = new List<User>(_Userservice.Get());
            foreach (var document in user_list)
            {
                lista_userow.Add(new SelectListItem { Selected = false, Text = document.FullName, Value = document.UserId });
            }
            var slist_user = new SelectList(lista_userow, "Value", "Text");

            var lista_deploymentowow = new List<SelectListItem>();
            var deployment_list = new List<Deployment>(_DeploymentService.Get());
            foreach (var document in deployment_list)
            {
                lista_deploymentowow.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.DeploymentId });
            }
            var slist_deployment = new SelectList(lista_deploymentowow, "Value", "Text");

            var viewModel = new ShowDeploymentViewModel()
            {
                SProjectlist = slist_project,
                SDatabaselist = slist_database,
                SUserlist = slist_user,
                SDeploymentlist = slist_deployment
            };
            return View(viewModel);
        }


        
        [HttpGet("ShowAllDeployment")]
        public IActionResult ShowAllDeployment()
        {
            
            var deployment_list = new List<Deployment>(_DeploymentService.Get());

            var document_list_toDisplay = new List<DeploymentToDisplay>();
                        

            foreach (var document in deployment_list)
            {
                var document_toDisplay = new DeploymentToDisplay(document, _ProjectService, _DatabaseService, _Userservice);

                document_list_toDisplay.Add(document_toDisplay);
            }



            var viewModel = new ShowAllDeploymentViewModel()
            {
                DeploymentToDisplay_List = document_list_toDisplay
            };

            return View(viewModel);

        }

    }
}
