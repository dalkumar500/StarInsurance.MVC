using Microsoft.AspNet.Identity;
using Services;
using StarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarInsurance.MVC.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        
        
            // GET: Claim
            public ActionResult Index()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new ClaimService(userId);
                var model = service.Getclaims();
                return View(model);
            }

            //GET
            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(ClaimCreate model)
            {
                if (!ModelState.IsValid) return View(model);

                var service = CreateClaimService();

                if (service.CreateClaim(model))
                {
                    TempData["SaveResult"] = "Your Claim was created.";
                    return RedirectToAction("Index");
                };

                ModelState.AddModelError("", "Claim could not be created.");

                return View(model);
            }

            public ActionResult Details(int id)
            {
                var svc = CreateClaimService();
                var model = svc.GetClaimById(id);
                return View(model);
            }

            public ActionResult Edit(int id)
            {
                var service = CreateClaimService();
                var detail = service.GetClaimById(id);
                var model =
                    new ClaimEdit
                    {
                        ClaimId = detail.ClaimId,
                        ClaimType = detail.ClaimType,
                        Description = detail.Description,
                        ClaimAmount = detail.ClaimAmount,
                        DateOfIncident=detail.DateOfIncident,
                        DateOfClaim = detail.DateOfClaim,
                        IsValid     =detail.IsValid
                    };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, ClaimEdit model)
            {
                if (!ModelState.IsValid) return View(model);

                if (model.ClaimId != id)
                {
                    ModelState.AddModelError("", "Id Mismatch");
                    return View(model);
                }

                var service = CreateClaimService();

                if (service.UpdateClaim(model))
                {
                    TempData["SaveResult"] = "Your Claim was updated.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Your Claim could not be updated.");
                return View();
            }

            [ActionName("Delete")]
            public ActionResult Delete(int id)
            {
                var svc = CreateClaimService();
                var model = svc.GetClaimById(id);
                return View(model);
            }

            [HttpPost]
            [ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeletePost(int id)
            {
                var service = CreateClaimService();
                service.DeleteClaim(id);
                TempData["SaveResult"] = "Your Claim was deleted";
                return RedirectToAction("Index");
            }

            private ClaimService CreateClaimService()
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new ClaimService(userId);
                return service;
            }
        }
    }
