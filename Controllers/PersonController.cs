using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCarApi1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCarApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {


        public readonly ICarPersonRepository<Person> personDbbb;

        public PersonController(ICarPersonRepository<Person> _personDbb)
        {
            this.personDbbb = _personDbb;
        }


        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return personDbbb.List();
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return personDbbb.Find(id);
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] Person newPerson)
        {
            personDbbb.Add(newPerson);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Person updatePerson)
        {
            personDbbb.Update(id, updatePerson);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            personDbbb.Delete(id);
        }
    }
}
