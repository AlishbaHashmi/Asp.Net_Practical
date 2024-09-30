using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api01.Models.DTOs;
using web_api01.Models;
using web_api01.Data;

namespace web_api01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly VehicleContext db;
        public CompanyController(VehicleContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(db.Companies.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCompanyDetails(int id)
        {
            var company = db.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
            {

                return NotFound();
            }
            else
            {

                return Ok(company);
            }
        }


        [HttpPost]
        public IActionResult AddCompany(CompanyDTO companydata)
        {

            if (companydata != null)
            {
                var company = new Company()
                {
                    Name = companydata.Name,
                    Address = companydata.Address,

                };

                var newaddedcompany = db.Companies.Add(company);
                db.SaveChanges();
                return Ok(newaddedcompany.Entity);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]

        public IActionResult EditCompany(int id, CompanyDTO companydata)
        {

            if (companydata != null && id != null)
            {
                var company = db.Companies.Find(id);

                company.Name = companydata.Name;
                company.Address = companydata.Address;



                var newaddedcompany = db.Companies.Update(company);
                db.SaveChanges();
                return Ok(newaddedcompany.Entity);

            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = db.Companies.Find(id);

            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return Ok();

        }
    }
}
