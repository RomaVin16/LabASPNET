using System.ComponentModel.DataAnnotations.Schema;

namespace LabASPNET.Models
{
    [Table("users")]
    public class User
    {
        [Column("userid")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("registrationdate")]
        public DateTime RegistrationDate { get; set; }
    }
}
