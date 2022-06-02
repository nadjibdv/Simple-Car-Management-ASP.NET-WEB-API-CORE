using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebCarApi1.Models;
using WebCarApi1.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCarApi1.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        public readonly ICarPersonRepository<Person> personDbb;
        public readonly ICarPersonRepository<Car> carDbb;
        public readonly IHostingEnvironment hosting;



        public CarController(ICarPersonRepository<Car> _carDbb, ICarPersonRepository<Person> _personDbb, IHostingEnvironment _hosting)
        {
            this.personDbb = _personDbb;
            this.carDbb = _carDbb;
            this.hosting = _hosting;

        }
        // GET: api/<CarController>
        [HttpGet]
        public IList<CarListViewModels> Get()
        {
             return carDbb.Find2(0).ToList();
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public IList<CarListViewModels> Get(int id)
        {
            return carDbb.Find2(id);
        }

        // POST api/<CarController>
        [HttpPost]
        public void Post([FromBody] Car newCar)
        {
            carDbb.Add(newCar);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car updateCar)
        {
            try
            {
                carDbb.Update(id, updateCar);
               

            }
            catch (Exception e)
            {
                
            }   
  


           
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id){
            

            try
            {

                if (id > 0)
                {
                    string aaa = carDbb.Find(id).car_ImageUrl;
                    carDbb.Delete(id);

                    if (aaa != null) {
                        var physicalPath = hosting.ContentRootPath + "/Photos/car/" + aaa;
                        var fileInfo = new FileInfo(physicalPath);
                        if (fileInfo.Exists) fileInfo.Delete(); }
                }

               
            }
            catch { }
        }


        // https://localhost:44377/api/car/UplodFile
        [Route("UplodFile")]
        [HttpPost]
        public IActionResult Upload()
        {
            try
            {

                var file = Request.Form.Files[0];


                string car_id_up = Request.Form["car_id"].ToString();

                
                var folderName = Path.Combine("Photos", "car");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                int idd = Convert.ToInt32(car_id_up);

                try{

                    if (idd > 0)
                    {
                        var physicalPath = hosting.ContentRootPath + "/Photos/car/" + carDbb.Find(idd).car_ImageUrl;
                        var fileInfo = new FileInfo(physicalPath);
                        if (fileInfo.Exists) fileInfo.Delete();
                    }


                }catch { }

                
              

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    
                    
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    
                        return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


  







    }
}
