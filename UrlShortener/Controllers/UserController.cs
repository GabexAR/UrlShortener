using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Security.Claims;
using UrlShortener.Data.Models.DTO;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }
        [HttpGet("admin/users")]
        public IActionResult GetAll()
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole != "Admin") return Forbid();
            return Ok(_service.GetAll());
        }
        [HttpGet("admin/users/{id}")]
        public IActionResult GetById(int id)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole != "Admin") return Forbid();
            UserDto? user = _service.GetById(id);
            if (user is not null) return Ok(user);
            return NotFound("User not found");
        }
        [HttpGet("user/{id}/cantConverv")]
        public IActionResult GetCantConver(int id)
        {
            int userId = Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
            return Ok(_service.GetCantConver(userId));
        }
        [HttpPost("resetCantConver")]
        public IActionResult ResetCantCover(int id)
        {
            int userId = Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
            var mensaje = _service.ResetCantCover(userId);
            if(mensaje == 1)
            {
                return Ok("Contador reseteado correctamente.");

            }
            return Forbid();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UserCreationDto dto)
        {
            if (_service.Exists(dto.Email)) return Conflict("Email taken");
            _service.Add(dto);
            UserDto newUser = _service.GetByEmail(dto.Email)!;
            return Created(newUser.Id.ToString(), newUser);
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserUpdateDto dto)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            string email = User.Claims.First(claim => claim.Type.Contains("email")).Value;
            if (userRole != "Admin" && email != dto.Email) return Forbid();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] UserDeletionDto dto)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            string email = User.Claims.First(claim => claim.Type.Contains("email")).Value;
            if (userRole != "Admin" && email != dto.Email) return Forbid();
            if (!_service.Exists(dto.Email)) return NotFound("User not found");
            _service.Delete(dto);
            return NoContent();
        }
    }
}
