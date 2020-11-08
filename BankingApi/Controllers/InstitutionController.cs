using BankingApi.Core.Domain;
using BankingApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionDataService _institutionDataService;

        public InstitutionController(IInstitutionDataService institutionDataService)
        {
            _institutionDataService = institutionDataService;
        }

        // GET: api/<InstitutionController>
        [HttpGet]
        public IActionResult Get()
        {
            var pagedInstitutions = _institutionDataService.GetAllInstitutions();

            return Ok(pagedInstitutions);
        }

        // GET api/<InstitutionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Add(Institution institution)
        {
            await _institutionDataService.AddInstitution(institution);

            return Ok();
        }
    }
}
