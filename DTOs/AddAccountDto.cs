using System.ComponentModel.DataAnnotations;

namespace SteamQueue.Entities
{
    public class AddAccountDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
