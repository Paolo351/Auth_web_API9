using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web_API9.Models.Application.Database;
using web_API9.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using web_API9.Models.Application.Deployment;

namespace web_API9.Controllers
{

    [Route("Database")]
    [ApiController]
    public class DatabaseController : Controller
    {
        private readonly DatabaseService _DatabaseService;
        private readonly DeploymentService _DeploymentService;
        private readonly  ProjectService _ProjectService;
        private readonly Userservice _Userservice;

        public DatabaseController(DatabaseService DatabaseService, DeploymentService DeploymentService, ProjectService ProjectService, Userservice Userservice)
        {
            _DatabaseService = DatabaseService;
            _DeploymentService = DeploymentService;
            _ProjectService = ProjectService;
            _Userservice = Userservice;
        }

        
        [HttpGet("ShowAllDatabase")]
        public IActionResult ShowAllDatabase()
        {
            
            var database_list = new List<Database>(_DatabaseService.Get());

            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };

            return View(viewModel);
            
        }

        
        [HttpGet("AddDatabase")]
        public IActionResult AddDatabase(string Name, string Engine)
        {
            var baza = new Database(Name, Engine);
           
            var database_list = new List<Database>();

            database_list.Add(_DatabaseService.Create(baza));

            var viewModel = new ShowAllDatabaseViewModel()
            {
                Databases = database_list
            };

            return View(viewModel);
        }

        

        [HttpGet("DelDatabase")]
        public IActionResult DelDatabase(string DatabaseId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_baza_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.TargetDbId == DatabaseId)
                {
                    deployments_z_baza_do_kasacji.Add(document);
                }

            }

            if (deployments_z_baza_do_kasacji.Count == 0)
            {
                var database_do_kasacji = _DatabaseService.Get(DatabaseId);

                var database_list = new List<Database>();

                database_list.Add(database_do_kasacji);

                if (database_do_kasacji == null)
                    return NotFound();

                var viewModel = new ShowAllDatabaseViewModel()
                {
                    Databases = database_list
                };

                _DatabaseService.Remove(database_do_kasacji.DatabaseId);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("NotDelDatabase", "Database", new { DatabaseId });
            }  
        }

        [HttpGet("NotDelDatabase")]
        public IActionResult NotDelDatabase(string DatabaseId)
        {
            var all_deployments_list = new List<Deployment>(_DeploymentService.Get());

            var deployments_z_baza_do_kasacji = new List<Deployment>();

            foreach (var document in all_deployments_list)
            {
                if (document.TargetDbId == DatabaseId)
                {
                    deployments_z_baza_do_kasacji.Add(document);
                }

            }

            var document_list_toDisplay = new List<DeploymentToDisplay>();

            foreach (var document in deployments_z_baza_do_kasacji)
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

        

        [HttpGet("ShowDatabase")]
        public IActionResult ShowDatabase()
        {
            var lista_baz = new List<SelectListItem>();

            var database_list = new List<Database>(_DatabaseService.Get());

            foreach (var document in database_list)
            {
                lista_baz.Add(new SelectListItem { Selected = false, Text = document.Name, Value = document.DatabaseId });
            }
            var slist_database = new SelectList(lista_baz, "Value", "Text");



            var viewModel = new ShowDatabaseViewModel()
            {

                SDatabaselist = slist_database
            };

            return View(viewModel);
        }
    }
}
