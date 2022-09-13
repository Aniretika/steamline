using System.ComponentModel.DataAnnotations;

namespace SteamQueue.DTOs
{
    public class AddLineDto
    {
        [Key]
        public Guid LineId { get; set; }

        public string Name { get; set; }

        public long PositionPeriod { get; set; }

        public DateTimeOffset LineStart { get; set; }

        public DateTimeOffset LineFinish { get; set; }

        public DateTimeOffset RegistrationTime { get; set; } = DateTime.Now;
    }
}
