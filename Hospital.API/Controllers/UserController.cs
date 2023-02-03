using Hospital.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IURepository<User> _repository;

        public UserController(IURepository<User> repository)
        {
            _repository = repository;

        }
        [HttpGet("GetAllUser")]
        public IEnumerable<User> GetUser()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("GetUserById/{id}", Name = "GetUserById")]
        public async Task<ActionResult> GetUserId(int id)
        {
            var user = await _repository.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(user);
            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
        }
    }
}

