using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/Attach")]
    public class AttachController : ControllerBase
    {
        private readonly IAttachService _attachService;

        public AttachController(IAttachService attachService)
        {
            _attachService = attachService;
        }

        [HttpPost("/Create")]
        public async Task<IActionResult> CreateAttach([FromBody] UploadAttachDto dto)
        {
            var attach = await _attachService.CreateAttachAsync(dto);

            if(attach == null)
                return NotFound();

            return Created();
        }
    }
}
