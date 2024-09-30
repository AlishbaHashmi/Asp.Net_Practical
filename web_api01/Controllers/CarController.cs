using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api01.Models.DTOs; 
using web_api01.Models;
using web_api01.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace web_api01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly VehicleContext db;

        public CarController(VehicleContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            return Ok(db.Cars.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCarDetails(int id)
        {
            var car = db.Cars.FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet("{PageNo}/{PageSize}")]


        public IActionResult GetCars(int PageNo, int PageSize)
        {
            int pageNumber = PageNo < 1 ? 1 : PageNo;
            int pageSize = PageSize < 1 ? 1 : PageSize;

            var cars = db.Cars.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return Ok(cars);
        }

        [HttpGet("search/{q}")]
        public IActionResult SearchCars(string q)
        {
            var cars = db.Cars.Include(x => x.Company)
                .Where(x => x.Make.Contains(q) || x.Model.Contains(q) || x.Color.Contains(q))
                .ToList(); 

            if (!cars.Any())
            {
                return NotFound();
            }

            return Ok(cars);
        }

        [HttpPost]
        public IActionResult AddCar(CarDTO cardata) 
        {
            if (cardata != null)
            {
                var car = new Car()
                {
                    Make = cardata.Make,
                    Model = cardata.Model,
                    Year = cardata.Year,
                    Color = cardata.Color,
                 
                };

                var newaddedCar = db.Cars.Add(car);
                db.SaveChanges();
                return Ok(newaddedCar.Entity);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult EditCar(int id, CarDTO cardata) 
        {
            if (cardata != null && id > 0)
            {
                var car = db.Cars.Find(id);
                if (car == null)
                {
                    return NotFound();
                }

                car.Make = cardata.Make;
                car.Model = cardata.Model;
                car.Year = cardata.Year;
                car.Color = cardata.Color;

                db.Cars.Update(car);
                db.SaveChanges();
                return Ok(car);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = db.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok();
        }
    }
}
