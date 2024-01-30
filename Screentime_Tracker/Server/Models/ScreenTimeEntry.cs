using Duende.IdentityServer.Models;
using Screentime_Tracker.Server.Models;
using System.ComponentModel.DataAnnotations;

public class ScreentimeEntry
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Url]
    public string WebsiteLink { get; set; } = string.Empty; // Initialize with empty string

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Time spent must be greater than 0")]
    public int TimeSpent { get; set; }

    // Make UserId nullable if it is not required at the time of creation
    public string? UserId { get; set; }

    // Navigation property for EF Core
    public virtual ApplicationUser? User { get; set; }
}