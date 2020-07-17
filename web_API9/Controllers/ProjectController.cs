using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_API9.Models.Application.Project;
using web_API9.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using web_API9.Models.Application.Deployment;

namespace web_API9.Controllers
{
    [Route("Project")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ProjectService _ProjectService;
        private readonly DeploymentService _DeploymentService;
        private readonly DatabaseService _DatabaseService;
        private readonly Userservice _Userservice;

        public ProjectController(ProjectService ProjectService, DeploymentService DeploymentService, Userservice Userservice, DatabaseService DatabaseService)
        {
            _ProjectService = ProjectService;
            _DeploymentService = DeploymentService;
            _DatabaseService = DatabaseService;
            _Userservice = Userservice;
        }

        
        [HttpGet("AddProject")]
        public IActionResult AddProject(string Name)
        {
            var projekt = new Project(Name);

            var project_list = new List<Project>();

            project_list.Add(_ProjectService.Create(projekt));

            var viewModel = new ShowAllProjectViewModel()
            {
                Projects = project_list
            };

            return View(viewModel);
        }


        
        [HttpGet("DelProject")]
        public IActionResult DelProject(string ProjectId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_projektem_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.AttachedToProjectId == ProjectId)
                {
                    deployments_z_projektem_do_kasacji.Add(document);
                }

            }

            if (deployments_z_projektem_do_kasacji.Count == 0)
            {
                var project_do_kasacji = _ProjectService.Get(ProjectId);

                var project_list = new List<Project>();

                project_list.Add(project_do_kasacji);

                if (project_do_kasacji == null)
                    return NotFound();

                var viewModel = new ShowAllProjectViewModel()
                {
                    Projects = project_list
                };

                _ProjectService.Remove(project_do_kasacji.ProjectId);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("NotDelProject", "Project", new { ProjectId });
            }
        }

        [HttpGet("NotDelProject")]
        public IActionResult NotDelProject(string ProjectId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_projektem_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.AttachedToProjectId == ProjectId)
                {
                    deployments_z_projektem_do_kasacji.Add(document);
                }

            }

            var document_list_toDisplay = new List<DeploymentToDisplay>();

            foreach (var document in deployments_z_projektem_do_kasacji)
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


        [HttpGet("ShowAllProject")]
        public IActionResult ShowAllProject()
        {

            var project_list = new List<Project>(_ProjectService.Get());

            var viewModel = new ShowAllProjectViewModel()
            {
                Projects = project_list
            };

            return View(viewModel);

        }

        

        [HttpGet("ShowProject")]
        public IActionResult ShowProject()
        {

            var lista_projektow = new List<SelectListItem>();

            var project_list = new List<Project>(_ProjectService.Get());

            foreach (var document in project_list)
            {
                lista_projektow.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.ProjectId });
            }
            var slist_project = new SelectList(lista_projektow, "Value", "Text");



            var viewModel = new ShowProjectViewModel()
            {
                SProjectlist = slist_project
            };

            return View(viewModel);
        }
    }
}
