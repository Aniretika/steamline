using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SteamQueue.Entities
{
    public class Line
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long PositionPeriod { get; set; }

        public DateTimeOffset LineStart { get; set; }

        public DateTimeOffset LineFinish { get; set; }

        public DateTimeOffset RegistrationTime { get; set; } = DateTime.Now;

        [NotMapped]
        [JsonIgnore]
        public TimeSpan ValidityPeriod
        {
            get { return TimeSpan.FromTicks(PositionPeriod); }
            set { PositionPeriod = value.Ticks; }
        }

        public IList<Position> Positions { get; set; }

        public SteamAccount SteamAccount { get; set; }

        public Guid SteamAccountId { get; set; }
    }
}
