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
    [Authorize]
    public class MasterUnitManagerController : Controller
    {
        private readonly IRepositoryService _repository;
        private readonly UserManager<User> _userManager;

        public MasterUnitManagerController(IRepositoryService repository, UserManager<User> userManager)
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

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return Forbid();
            }

            newMasterUnit.OwnerId = user.Id;
            Guid id = Guid.NewGuid();
            var result = await _repository.MasterUnits.AddAsync(new MasterUnit
            {
                Id = id,
                CustomName = newMasterUnit.CustomName,
                User = user,
                UserId = user.Id,
                ConcurrencyLock = Guid.NewGuid(),
                DataSamples = null,
                IsOn = newMasterUnit.IsOn,
                Designs = null
            });

            if(!result)
            {
                return BadRequest();
            }

            newMasterUnit.Id = id;
            return new CreatedResult($"api/v1/masterunit/{id}", newMasterUnit);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return Forbid();
            }

            var result = await _repository.MasterUnits.FindAsync(wer => wer.Id == id && wer.User.Id == user.Id);

            if (result == null)
            {
                return NotFound();
            }
            var dtoResult = MasterUnitDto.Mapper(result);

            return new JsonResult(dtoResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return Forbid();
            }

            var result = await _repository.MasterUnits.FindListAsync(wer => wer.User.Id == user.Id);

            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = MasterUnitDto.Mapper(result);

            return new JsonResult(dtoResult);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MasterUnitDto updated)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (user == null)
            {
                return Forbid();
            }

            var result = await _repository.MasterUnits.FindAsync(wer => wer.User.Id == user.Id && wer.Id == updated.Id);

            if (result == null)
            {
                return NotFound();
            }

            if(result.ConcurrencyLock != updated.eTag)
            {
                var dtoList = new List<MasterUnitDto>
                {
                    updated,
                    MasterUnitDto.Mapper(result)
                };
                return BadRequest(dtoList);
            }

            updated.eTag = Guid.NewGuid();

            if (await _repository.MasterUnits.ModifyAsync(MasterUnitDto.Mapper(updated, user)))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}