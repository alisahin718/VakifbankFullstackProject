using Business.Repositories.Service;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerRelationshipsController : ControllerBase
    {
        private readonly IDealerRelationshipService _dealerRelationshipService;

        public DealerRelationshipsController(IDealerRelationshipService dealerRelationshipService)
        {
            _dealerRelationshipService = dealerRelationshipService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(DealerRelationship dealerRelationship)
        {
            var result = await _dealerRelationshipService.Add(dealerRelationship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(DealerRelationship dealerRelationship)
        {
            var result = await _dealerRelationshipService.Update(dealerRelationship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(DealerRelationship dealerRelationship)
        {
            var result = await _dealerRelationshipService.Delete(dealerRelationship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _dealerRelationshipService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dealerRelationshipService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
