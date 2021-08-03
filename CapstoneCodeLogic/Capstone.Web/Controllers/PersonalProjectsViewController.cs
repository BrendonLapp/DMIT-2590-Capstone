using System.Collections.Generic;
using System.Web.Mvc;
using Capstone.BLL.Controllers;
using Capstone.BLL.DTOs.PersonalProjects;
using Capstone.Web.Admin;
using Capstone.Web.Models.PersonalProjects;
using Capstone.BLL.Security;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// Personal Projects View Controller for views in the Views/ProjectCategoryView folder
    /// </summary>
    public class PersonalProjectsViewController : Controller
    {
        /// <summary>
        /// GET method for the Personal Projects Index.cshtml Form
        /// </summary>
        /// <returns>listPersonalProjectsViewModel as viewModel</returns>
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.EmployeeRole))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var controller = new PersonalProjectsController();

                List<ListPersonalProjectsViewModel> listPersonalProjectsViewModel = new List<ListPersonalProjectsViewModel>();
                List<PersonalProjectDTO> listPersonalProjectDTOs = new List<PersonalProjectDTO>();

                listPersonalProjectDTOs = controller.LookupPersonalProjects(IdentityHelper.GetEmployeeID());

                foreach (var item in listPersonalProjectDTOs)
                {
                    if (listPersonalProjectsViewModel.Exists(x => x.ProjectID == item.ProjectID))
                    {
                        ListPersonalProjectsViewModel personalProjectVM = listPersonalProjectsViewModel.Find(x => x.ProjectID == item.ProjectID);
                        personalProjectVM.AllocatedDays += item.AllocatedDays;
                    }
                    else
                    {
                        listPersonalProjectsViewModel.Add(new ListPersonalProjectsViewModel
                        {
                            ProjectID = item.ProjectID,
                            ProjectName = item.ProjectName,
                            AllocationID = item.AllocationID,
                            EmployeeID = item.EmployeeID,
                            AllocatedDays = item.AllocatedDays,
                            ActivationDate = item.ActivationDate,
                            DeactivationDate = item.DeactivationDate,
                            StartDate = item.StartDate
                        });
                    }
                }

                return View(listPersonalProjectsViewModel);
            }

        }

        /// <summary>
        /// GET method for the Personal Projects Detail.cshtml Form
        /// </summary>
        /// <returns>breakdownViewModel as viewModel</returns>
        /// <param name="ProjectID">The KEY of the TB_Capstone_PROJECT table</param>
        public ActionResult Detail(int ProjectID)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole(SecurityRoles.EmployeeRole))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var controller = new PersonalProjectsController();

                PersonalProjectBreakdownViewModel breakdownViewModel = new PersonalProjectBreakdownViewModel();
                PersonalProjectBreakdownDTO breakdownDTO = new PersonalProjectBreakdownDTO();

                breakdownDTO = controller.LookupPersonalProject(ProjectID, IdentityHelper.GetEmployeeID());

                breakdownViewModel.ProjectID = breakdownDTO.ProjectID;
                breakdownViewModel.EmployeeID = breakdownDTO.EmployeeID;
                breakdownViewModel.AllocationID = breakdownDTO.AllocationID;
                breakdownViewModel.ProjectName = breakdownDTO.ProjectName;
                breakdownViewModel.CategoryName = breakdownDTO.CategoryName;
                breakdownViewModel.Description = breakdownDTO.Description;
                breakdownViewModel.Startdate = breakdownDTO.Startdate;
                breakdownViewModel.ForecastedEndDate = breakdownDTO.ForecastedEndDate;
                breakdownViewModel.Year = breakdownDTO.Year;

                return View(breakdownViewModel);
            }
        }

        /// <summary>
        /// GET method for the Personal Projects Detail.cshtml partial view in the Detail.cshtml Form
        /// </summary>
        /// <returns>yearBreakdownViewModel as viewModel into the _YearDetailBreakdown Partial View</returns>
        /// <param name="EmployeeID">The KEY of the TB_Capstone_EMPLOYEE table</param>
        /// <param name="ProjectID">The KEY of the TB_Capstone_PROJECT table</param>
        /// <param name="Year">The year that is displayed on the page to know which year to sort results by</param>
        public ActionResult _YearDetailBreakdown(int EmployeeID, int ProjectID, int Year)
        {
            YearBreakdownViewModel yearBreakdownViewModel = new YearBreakdownViewModel();
            YearBreakdownDTO breakdownDTO = new YearBreakdownDTO();
            var controller = new PersonalProjectsController();

            breakdownDTO = controller.LookupYearBreakdownForProject(EmployeeID, ProjectID, Year);

            yearBreakdownViewModel.ProjectID = breakdownDTO.ProjectID;
            yearBreakdownViewModel.EmployeeID = breakdownDTO.EmployeeID;
            yearBreakdownViewModel.Year = breakdownDTO.Year;
            yearBreakdownViewModel.January = breakdownDTO.January;
            yearBreakdownViewModel.Febuary = breakdownDTO.Febuary;
            yearBreakdownViewModel.March = breakdownDTO.March;
            yearBreakdownViewModel.April = breakdownDTO.April;
            yearBreakdownViewModel.May = breakdownDTO.May;
            yearBreakdownViewModel.June = breakdownDTO.June;
            yearBreakdownViewModel.July = breakdownDTO.July;
            yearBreakdownViewModel.August = breakdownDTO.August;
            yearBreakdownViewModel.September = breakdownDTO.September;
            yearBreakdownViewModel.October = breakdownDTO.October;
            yearBreakdownViewModel.November = breakdownDTO.November;
            yearBreakdownViewModel.December = breakdownDTO.December;

            return PartialView("_YearDetailBreakdown", yearBreakdownViewModel);
        }

    }
}