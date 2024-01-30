namespace Screentime_Tracker.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScreentimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScreentimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This action handles GET requests to get the Screentime entries for the logged-in user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreentimeEntryDto>>> GetScreentimeEntries()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var entries = await _context.ScreentimeEntries
                .Where(e => e.UserId == userId)
                .Select(e => new ScreentimeEntryDto
                {
                    WebsiteLink = e.WebsiteLink,
                    TimeSpent = e.TimeSpent
                })
                .ToListAsync();

            return entries;
        }

        // This action handles POST requests to create new Screentime entries
        [HttpPost]
        public async Task<IActionResult> PostScreentimeEntry(ScreentimeEntryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var entry = new ScreentimeEntry
            {
                UserId = userId,
                WebsiteLink = dto.WebsiteLink,
                TimeSpent = dto.TimeSpent
            };
                
            try
            {
                _context.ScreentimeEntries.Add(entry);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetScreentimeEntries), new { id = entry.Id }, dto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

    }
}
