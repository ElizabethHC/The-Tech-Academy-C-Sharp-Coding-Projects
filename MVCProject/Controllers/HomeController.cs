using MVCProject.Models;
using MVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Apply(string firstName, string lastName, string emailAddress, 
            string birthDate, string carYear, string carMake, string carModel, string DUI,
            string tickets, string coverageType)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) ||
                string.IsNullOrEmpty(birthDate) || string.IsNullOrEmpty(carYear) || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(DUI) || string.IsNullOrEmpty(tickets) ||
                string.IsNullOrEmpty(coverageType))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (CarInsuranceEntities db = new CarInsuranceEntities())
                {
                    var apply = new Applicant();
                    apply.FirstName = firstName;
                    apply.LastName = lastName;
                    apply.EmailAddress = emailAddress;
                    apply.BirthDate = birthDate;
                    apply.CarYear = carYear;
                    apply.CarMake = carMake;
                    apply.CarModel = carModel;
                    apply.DUI = DUI;
                    apply.Tickets = tickets;
                    apply.CoverageType = coverageType;

                    db.Applicants.Add(apply);
                    db.SaveChanges();


                }

                    return View("Success");
            }

        }
    }
}
