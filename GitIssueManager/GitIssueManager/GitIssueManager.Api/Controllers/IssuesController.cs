using GitIssueManager.Core;
using GitIssueManager.Core.Dtos;
using GitIssueManager.Core.Strategies;
using Microsoft.AspNetCore.Mvc;

namespace GitIssueManager.Api.Controllers
{
    [ApiController]
    [Route("api/{service}/issues")]
    public class IssuesController : ControllerBase
    {
        private readonly IEnumerable<IServiceStrategy> _issueStrategies;

        public IssuesController(IEnumerable<IServiceStrategy> issueStrategies)
        {
            _issueStrategies = issueStrategies;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string service, [FromBody] IssueCommandDto dto)
        {
            if (!Enum.TryParse<GitHostingServiceType>(service, true, out var hostingType))
                return BadRequest(new { error = $"Unsupported service '{service}'." });

            var strategy = _issueStrategies.FirstOrDefault(s => s.Supports(hostingType));
            if (strategy is null)
                return NotFound(new { error = $"Manager not support service '{service}'." });

            var created = await strategy.CreateAsync(dto);

            return Created($"api/{service}/issues/{created.IssueNumber}", created);
        }

        [HttpPatch("{issueNumber:int:min(1)}")]
        public async Task<IActionResult> Update(string service, int issueNumber, [FromBody] IssueCommandDto dto)
        {
            if (!Enum.TryParse<GitHostingServiceType>(service, true, out var hostingType))
                return BadRequest(new { error = $"Unsupported service '{service}'." });

            var strategy = _issueStrategies.FirstOrDefault(s => s.Supports(hostingType));
            if (strategy is null)
                return NotFound(new { error = $"Manager not support service '{service}'." });

            var updated = await strategy.UpdateAsync(issueNumber, dto);
            return Ok(updated);
        }

        [HttpPatch("{issueNumber:int:min(1)}/close")]
        public async Task<IActionResult> Close(string service, int issueNumber, [FromBody] StateIssueDto dto)
        {
            if (!Enum.TryParse<GitHostingServiceType>(service, true, out var hostingType))
                return BadRequest(new { error = $"Unsupported service '{service}'." });

            var strategy = _issueStrategies.FirstOrDefault(s => s.Supports(hostingType));
            if (strategy is null)
                return NotFound(new { error = $"Manager not support service '{service}'." });

            await strategy.CloseAsync(issueNumber, dto);
            return NoContent();
        }

    }
}
