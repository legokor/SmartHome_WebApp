using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartHome.Dto;
using SmartHome.Persistence;
using SmartHome.Model;
using Microsoft.AspNetCore.Identity;

namespace SmartHomeWebApp.Controllers
{
    [Authorize]
    public class DesignerController : Controller
    {
        private readonly RepositoryService _repository;
        private readonly UserManager<User> _userManager;

        public DesignerController(RepositoryService repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DesignerDto model)
        {
            if(!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var design = new Design
            {
                Creator = user,
                Sequence = model.Sequence
            };

            await _repository.Desings.AddAsync(design);

            return Created("", model);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var result = await _repository.Desings.FindListAsync(wer => wer.Creator.Id == user.Id);

            if (result == null || result?.Count == 0)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DesignerDto model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var design = new Design
            {
                Creator = user,
                Sequence = model.Sequence
            };

            await _repository.Desings.AddAsync(design);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DesignerDto model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var design = await _repository.Desings.FindAsync(wer => (wer.Id == model.Id) && (wer.Creator.Id == user.Id));

            if(design == null)
            {
                return BadRequest();
            }

            if(!await _repository.Desings.RemoveAsync(design))
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }

    }
}