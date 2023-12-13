using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Models.DTO;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UrlShortenerController : Controller
    {
        private readonly UrlService _service;
        public UrlShortenerController(UrlService urlService)
        {
            _service = urlService;
        }
        [HttpGet("admin/urls")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            //string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            //if (userRole is not "Admin") return Forbid();
            return Ok(_service.GetAll());
        }
        [HttpGet("admin/urls/categorys")]
        [AllowAnonymous]
        public IActionResult GetByCategory(int id) 
        {
            return Ok(_service.GetByCategory(id));

        }
        [HttpGet("admin/urls/{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            //string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            //if (userRole is not "Admin") return Forbid();
            Url? url = _service.GetById(id);
            if (url is not null) return Ok(url);
            return NotFound();
        }
        [HttpGet("urls")]

        public IActionResult GetUserUrls()
        {
            int userId = Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
            List<Url> urls = _service.GetByUserId(userId);
            return Ok(urls);
        }

        [HttpGet("{code}")]
        [AllowAnonymous]
        public IActionResult RedirectToUrl(string code)
        {
            Url? url = _service.GetByCode(code);
            if (url is null) return NotFound();
            _service.UpdateClicks(url.Id);
            return Redirect(url.LargeUrl);
        }
        [HttpPost]
        public IActionResult Shorten([FromBody] UrlCreation url)
        {
            int userId = Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
            //List<int> categoryIds = url.Categories.Select(category => category.Id).ToList();
            string categoryName = url.CategoryName;
            if (string.IsNullOrEmpty(url.CategoryName)) 
            {
                return BadRequest("El nombre de la categoria es obligatorio");
            }
            return Ok(_service.Add(url, userId, categoryName));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            int userId = Int32.Parse(User.Claims.First(claim => claim.Type.Contains("nameidentifier")).Value);
            Url? url = _service.GetById(id);
            if (url is null) return NotFound();
            if (userRole is not "Admin" && userId != url.UserId) return Forbid();
            return _service.Delete(id) ? Ok() : NotFound();
        }
    }
}
