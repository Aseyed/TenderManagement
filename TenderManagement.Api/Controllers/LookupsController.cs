using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagement.Application.Interfaces.Services;
using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LookupDto>))]
        public async Task<ActionResult<IEnumerable<LookupDto>>> GetCategories()
        {
            var categories = await _lookupService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("statuses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LookupDto>))]
        public async Task<ActionResult<IEnumerable<LookupDto>>> GetStatuses()
        {
            var statuses = await _lookupService.GetAllStatusesAsync();
            return Ok(statuses);
        }
    }
}