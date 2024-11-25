using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stremio.Models;

namespace Stremio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    public readonly VideoContext _context;

    public VideoController(VideoContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetVideos")]
    public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
    {
        return await _context.Videos.ToListAsync();
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Video>> GetVideo(long Id)
    {
        var video = await _context.Videos.FindAsync(Id);

        if (video == null)
        {
            return NotFound();
        }

        return video;
    }

    [HttpPost(Name = "PostVideo")]
    public async Task<ActionResult<Video>> PostVideo(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetVideo),
            new { Id = video.Id, UrlLocation = video.UrlLocation },
            video
        );
    }
}
