using System.ComponentModel.DataAnnotations;

namespace SteamQueue.DTOs
{
    public class AddPositionDtoBase
    {
        //PK properties
        [Key]
        public Guid PositionId { get; set; }

        public Guid LineId { get; set; }

        public long? TelegramRequesterId { get; set; }

        public long? BotId { get; set; }

        [Required]
        public string? Requester { get; set; }

        public DateTime RegistrationTime { get; set; } = DateTime.Now;

        public DateTime? TimelineStart { get; set; }

        public DateTime? TimelineFinish { get; set; }

        public string? DescriptionText { get; set; }
    }
}
