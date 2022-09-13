using System.ComponentModel.DataAnnotations;

namespace SteamQueue.Entities
{
    public class SteamAccount
    {
        [Key]
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Line Line { get; set; }
    }
}
