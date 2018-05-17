using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHome.Dto;
using SmartHome.Model;
using SmartHome.Persistence;

namespace SmartHome.WebApp.Controllers
{
    [Route("api/v1/masterunit")]
    public class MasterUnitManagerController : Controller
    {
        private readonly RepositoryService _repository;
        private readonly UserManager<User> _userManager;

        public MasterUnitManagerController(RepositoryService repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MasterUnitDto newMasterUnit)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var user = await _userManager.FindByIdAsync(newMasterUnit.OwnerId.ToString());
            if (user == null)
            {
                return Forbid();
            }

            Guid id = Guid.NewGuid();
            var result = await _repository.MasterUnits.AddAsync(new MasterUnit
            {
                Id = id,
                CustomName = newMasterUnit.CustomName,
                Owner = user,
                DataSamples = null,
                IsOn = newMasterUnit.IsOn,
                Designs = null
            });

            if(!result)
            {
                return BadRequest();
            }

            newMasterUnit.Id = id;
            return new CreatedResult($"api/v1/{id}", newMasterUnit);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return Forbid();
            }

            var result = await _repository.MasterUnits.FindAsync(wer => wer.Id == id && wer.Owner.Id == user.Id);

            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return Forbid();
            }

            var result = await _repository.MasterUnits.FindAsync(wer => wer.Owner.Id == user.Id);

            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }
    }
}