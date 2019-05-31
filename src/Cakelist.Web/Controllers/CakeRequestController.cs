using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Business.Interfaces;
using Cakelist.Web.Models.CakeRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cakelist.Web.Controllers
{
    public class CakeRequestController : Controller
    {
        private readonly ICakelistService _cakelistService;
        private readonly ICakeRequestRepository _cakeRequestRepository;
        private readonly IUserRepository _userRepository;

        public CakeRequestController(ICakelistService cakelistService, ICakeRequestRepository cakeRequestRepository, IUserRepository userRepository)
        {
            _cakelistService = cakelistService;
            _cakeRequestRepository = cakeRequestRepository;
            _userRepository = userRepository;
        }


        // GET: CakeRequest
        public ActionResult Index()
        {
            return View();
        }

        // GET: CakeRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CakeRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CakeRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCakeRequest cakeRequest)
        {

            // Chech if the modelstate / model is valid, if not return 400 - Bad request
            if (!ModelState.IsValid) {
                return View();
            }

            // Find users
            //TODO: Throw if we not find the user
            var createdBy = await _userRepository.GetByIdAsync(cakeRequest.CreatedByUserId);
            var assignedTo = await _userRepository.GetByIdAsync(cakeRequest.AssignedToUserId);

            // Create request
            var createdCakeRequest = await _cakelistService.AddCakeRequestAsync(createdBy, assignedTo, cakeRequest.Reason);

            return View();

        }

        // GET: CakeRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CakeRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CakeRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CakeRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}