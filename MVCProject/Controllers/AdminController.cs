using MVCProject.Models;
using MVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                var applying = db.Applicants;
                var applyVms = new List<ApplyVM>();
                foreach (var apply in applying)
                {
                    var applyVm = new ApplyVM();
                    applyVm.FirstName = apply.FirstName;
                    applyVm.LastName = apply.LastName;
                    applyVm.EmailAddress = apply.EmailAddress;
                    applyVm.Quote = apply.Quote;
                    applyVms.Add(applyVm);
                }

                return View(applyVms);
            }
        }
    }
}