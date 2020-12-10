using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HospitalManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using MyHospitalManagement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyHospitalManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly IDoctorsRepository doctors;

        public DoctorsController(IDoctorsRepository doctorsRepository)
        {
            this.doctors = doctorsRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return this.doctors.GetDoctors();
        }

		// GET api/<controller>/5
		[HttpGet("/api/doctor/{id}")]
		public Doctor Get(int id)
        {
			return doctors.GetDoctor(id);

        }


        // GET api/<controller>/5
        [HttpGet("/api/patients/{id}")]
        public IActionResult GetPatients(int id)
        {
            var doctor = doctors.GetDoctor(id);
            
            if (doctor == null)
                return NotFound();

            var patients = doctors.GetPatients(id);

            return Ok(patients);
        }


		// POST api/<controller>
		[HttpPost("/api/doctor")]
		public IActionResult Post([FromBody]Doctor doctor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            
            var createdDoctor = doctors.AddDoctor(doctor);

            return CreatedAtAction(nameof(Get), new { id = createdDoctor.Id }, createdDoctor);
        }

        // PUT api/<controller>/5
      [HttpPut("/api/doctor/{id}")]
        public IActionResult Put(int id, [FromBody]Doctor doctor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                doctors.UpdateDoctor(id, doctor);
                return Ok();
            }
            catch (EntityNotFoundException<Doctor>)
            {
                return NotFound();
            }
        }

        // DELETE api/<controller>/5
        [HttpPut("/api/doctor/{id}")]
        public IActionResult Delete(int id)
        {
            doctors.DeleteDoctor(id);
            return Ok();
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE");
            return resp;

            //return Ok();
        }
    }
}
