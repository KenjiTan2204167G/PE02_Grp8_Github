using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screentime_Tracker.Shared
{
    public class ScreentimeEntryDto
    {
        public string WebsiteLink { get; set; } = string.Empty; // Initialize with empty string
        public int TimeSpent { get; set; }
    }
}

