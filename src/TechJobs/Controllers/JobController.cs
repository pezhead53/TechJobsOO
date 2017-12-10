using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job job = jobData.Find(id);
            return View(job);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                SelectListItem newEmployer = new SelectListItem();
                foreach (var employer in newJobViewModel.Employers)
                {
                    if (employer.Value == newJobViewModel.EmployerID.ToString())
                    {
                        newEmployer = employer;
                    }
                }
                SelectListItem newLocation = new SelectListItem();
                foreach (var location in newJobViewModel.Locations)
                {
                    if (location.Value == newJobViewModel.LocationID.ToString())
                    {
                        newLocation = location;
                    }
                }
                SelectListItem newCoreCompetency = new SelectListItem();
                foreach (var coreCompetency in newJobViewModel.CoreCompetencies)
                {
                    if (coreCompetency.Value == newJobViewModel.CoreCompetencyID.ToString())
                    {
                        newCoreCompetency = coreCompetency;
                    }
                }
                SelectListItem newPositionType = new SelectListItem();
                foreach (var positionType in newJobViewModel.PositionTypes)
                {
                    if (positionType.Value == newJobViewModel.PositionTypeID.ToString())
                    {
                        newPositionType = positionType;
                    }
                }
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = new Employer { ID=newJobViewModel.EmployerID, Value=newEmployer.Text },
                    Location = new Location { ID = newJobViewModel.LocationID, Value = newLocation.Text },
                    CoreCompetency = new CoreCompetency { ID = newJobViewModel.CoreCompetencyID, Value = newCoreCompetency.Text },
                    PositionType = new PositionType { ID = newJobViewModel.PositionTypeID, Value = newPositionType.Text }
                };
                jobData.Jobs.Add(newJob);
                return Redirect("/job?id="+ newJob.ID);
            }

            return View(newJobViewModel);
        }
    }
}
