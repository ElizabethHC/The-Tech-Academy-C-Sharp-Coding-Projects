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
                var insurance = 50;
                var insuranceAge = 0;
                int bday = Int32.Parse(birthDate);
                if (bday < 25 && bday > 17)
                {
                    insuranceAge = 25;
                }
                else if (bday < 18)
                {
                    insuranceAge = 100;
                }
                else if (bday > 100)
                {
                    insuranceAge = 25;
                }
                else
                {
                    insuranceAge = 0;
                }
                var insuranceTotal = insurance + insuranceAge;

                int cyear = Int32.Parse(carYear);
                var insuranceCYear = 0;
                if (cyear < 2000)
                {
                    insuranceCYear = 25;
                }
                else if (cyear > 2015)
                {
                    insuranceCYear = 25;
                }
                else
                {
                    insuranceCYear = 0;
                }
                insuranceTotal = (insuranceTotal + insuranceCYear);


                string CarMake = carMake.ToLower();
                string CarModel = carModel.ToLower();
                var insuranceCarType = 0;
                if (CarMake == "porsche" && CarModel != "911 carrera")
                {
                    insuranceCarType = 25;
                }
                else if (CarMake == "porsche" && CarModel == "911 carrera")
                {
                    insuranceCarType = 50;
                }
                else
                {
                    insuranceCarType = 0;
                }
                insuranceTotal = (insuranceTotal + insuranceCarType);

                var theTicket = Convert.ToInt32(tickets);
                var insuranceTicket = theTicket * 10;
                insuranceTotal = (insuranceTotal + insuranceTicket);

                string dui = DUI.ToLower();
                double insuranceDui = 0;
                if (dui == "yes")
                {
                    insuranceDui = .25;
                }
                else
                {
                    insuranceDui = 0;
                }
                double insuranceTotalD = Convert.ToDouble(insuranceTotal);
                insuranceTotalD = (insuranceTotal + (insuranceTotal * insuranceDui));

                string CoverageType = coverageType.ToLower();
                double insuranceType = 0;
                if (CoverageType == "full" || CoverageType == "full coverage")
                {
                    insuranceType = .50;
                }
                else if (CoverageType == "liability" || CoverageType == "liability coverage")
                {
                    insuranceType = 0;
                }
                insuranceTotalD = (insuranceTotalD + (insuranceTotalD * insuranceType));
                string finalInsurance = Convert.ToString(insuranceTotalD);


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
                    apply.Quote = finalInsurance;

                    db.Applicants.Add(apply);
                    db.SaveChanges();
                }

                return View("Success");
            }

        }
    }
}
