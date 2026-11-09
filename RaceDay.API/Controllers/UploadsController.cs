using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers;

[ApiController]
[Route("api/uploads")]
[Authorize]
public class UploadsController(BlobStorageService storage) : ControllerBase
{
    [HttpPost("event-banner")]
    [Authorize(Roles = "Organiser")]
    public Task<IActionResult> UploadEventBanner(IFormFile file) => Upload(file, "event-banners");

    [HttpPost("profile-picture")]
    public Task<IActionResult> UploadProfilePicture(IFormFile file) => Upload(file, "profile-pictures");

    private async Task<IActionResult> Upload(IFormFile file, string container)
    {
        if (file is null || file.Length == 0 || !file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase) || file.Length > 5 * 1024 * 1024)
            return BadRequest(new { message = "Choose an image file no larger than 5 MB." });
        try
        {
            var url = await storage.UploadAsync(file, container);
            return Ok(new { url });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = ex.Message });
        }
    }
}
