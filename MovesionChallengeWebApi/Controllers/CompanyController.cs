using Microsoft.AspNetCore.Mvc;
using MovesionChallengeWebApi.Entities;
using MovesionChallengeWebApi.Helpers;
using MovesionChallengeWebApi.Services;

namespace MovesionChallengeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Auth]
        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var companies = _companyService.GetAll();
            return Ok(companies);
        }

        [Auth]
        [HttpGet(nameof(GetById))]
        public IActionResult GetById([FromQuery] int id)
        {
            var company= _companyService.GetById(id);
            return Ok(company);
        }

        [Auth]
        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            _companyService.Delete(id);
            return NoContent();
        }

        [Auth]
        [HttpPost]
        public IActionResult Insert([FromBody] Company company)
        {
            _companyService.Insert(company);
            return NoContent();
        }

        [Auth]
        [HttpPut]
        public IActionResult Update([FromBody] Company company)
        {
            _companyService.Update(company);
            return NoContent();
        }
    }
}