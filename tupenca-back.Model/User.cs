using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] HashedPassword { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt{ get; set; }
    }
}
