using Hospital.API.Model;
using Hospital.API.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IGetRepository<DoctorDto> _doctorDtoRepository;
    
       


        public DoctorController(IRepository<Doctor> repository, IDoctorRepository doctorRepository,IGetRepository<DoctorDto> doctorDtoRepository)
        {
            _repository = repository;
            _doctorRepository = doctorRepository;
            _doctorDtoRepository = doctorDtoRepository;

           

        }

        [HttpGet("GetAllDoctor")]
        public IEnumerable<DoctorDto> GetDoctor()
        {
            return _doctorDtoRepository.GetAll();
        }

        [HttpGet]
        [Route("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<ActionResult> GetDectorById(int id)
        {
            var doctor = await _doctorDtoRepository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return NotFound();
        }

        [HttpGet("SearchDoctor/{specializationName}")]
        public async Task<ActionResult> SearchDoctorBySpecialization(string specializationName)
        {
            var result = await _doctorRepository.SearchBySpecialization(specializationName);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound("Plese provide valid Specialization ");
        }
        [Authorize(Roles = "admin,employee")]
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.Create(doctor);
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id }, doctor);
        }
        [Authorize(Roles = "admin,employee")]
        [HttpPut("UpdateDoctor/{id}")]

        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("DeleteDoctor/{id}", Name = "DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Doctor not found");
        }

        //get Specialization

        [HttpGet("GetSpecializations")]
        
        public async Task<IActionResult> GetSpecializations()
        {
            var specializations = await _doctorRepository.GetSpecializations();
            return Ok(specializations);
        }
    }
}
