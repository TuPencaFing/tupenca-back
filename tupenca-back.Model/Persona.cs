using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tupenca_back.Model
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(64)]
        [JsonIgnore]
        public byte[] HashedPassword { get; set; }

        [MaxLength(128)]
        [JsonIgnore]
        public byte[] PasswordSalt{ get; set; }
    }
}
