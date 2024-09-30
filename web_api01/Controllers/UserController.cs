using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api01.Models.DTOs;
using web_api01.Models;
using web_api01.Data;

namespace web_api01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VehicleContext _context;
        public UserController(VehicleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Users()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpPost]
        public IActionResult Signup(UserDTO userdata)
        {
            var check = _context.Users.FirstOrDefault(u => u.Email == userdata.Email);
            if (check != null)
            {
                return BadRequest("User already exists");
            }
            else
            {
                var hasher = new PasswordHasher<string>();
                var hashPass = hasher.HashPassword(userdata.Email, userdata.Password);
                User newuser = new User()
                {
                    Username = userdata.Username,
                    Email = userdata.Email,
                    Password = hashPass,
                    RoleId = userdata.RoleId,
                };
                var addedUser = _context.Users.Add(newuser);
                _context.SaveChanges();
                return Ok(addedUser.Entity);
            }
        }
    }
}
