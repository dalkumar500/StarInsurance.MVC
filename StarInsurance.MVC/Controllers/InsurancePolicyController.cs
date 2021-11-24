using Microsoft.AspNet.Identity;
using Services;
using StarInsurance.Data;
using StarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarInsurance.MVC.Controllers
{
    public class InsurancePolicyController : Controller
    {
        [Authorize]
       
            // GET: InsurancePolicy
            public ActionResult Index()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new InsurancePolicyService(userId);
                var model = service.GetInsurancePolicy();
                return View(model);
            }

            //GET
            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(InsurancePolicyCreate model)
            {
                if (!ModelState.IsValid) return View(model);

                var service = CreateInsurancePolicyService();

                if (service.CreateInsurancePolicy(model))
                {
                    TempData["SaveResult"] = "Your Policy was created.";
                    return RedirectToAction("Index");
                };

                ModelState.AddModelError("", "Policy could not be created.");

                return View(model);
            }

            public ActionResult Details(int id)
            {
                var svc = CreateInsurancePolicyService();
                var model = svc.GetInsurancePolicyById(id);
                return View(model);
            }

            public ActionResult Edit(int id)
            {
                var service = CreateInsurancePolicyService();
                var detail = service.GetInsurancePolicyById(id);
                var model =
                    new InsurancePolicyEdit
                    {
                        InsurancePolicyId = detail.InsurancePolicyId,
                        TypeOfPolicy = detail.TypeOfPolicy,
                        Name = detail.Name,
                        Address=detail.Address
                    };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, InsurancePolicyEdit model)
            {
                if (!ModelState.IsValid) return View(model);

                if (model.InsurancePolicyId != id)
                {
                    ModelState.AddModelError("", "Id Mismatch");
                    return View(model);
                }

                var service = CreateInsurancePolicyService();

                if (service.UpdateNote(model))
                {
                    TempData["SaveResult"] = "Your InsurancePolicy was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your InsurancePolicy could not be updated.");
                return View();
            }

            [ActionName("Delete")]
            public ActionResult Delete(int id)
            {
                var svc = CreateInsurancePolicyService();
                var model = svc.GetInsurancePolicyById(id);
                return View(model);
            }

            [HttpPost]
            [ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeletePost(int id)
            {
                var service = CreateInsurancePolicyService();
                service.DeleteInsurancePolicy(id);
                TempData["SaveResult"] = "Your InsurancePolicy was deleted";
                return RedirectToAction("Index");
            }

            private InsurancePolicyService CreateInsurancePolicyService()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new InsurancePolicyService(userId);
                return service;
            }
        }
    }
