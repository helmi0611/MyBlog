using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(12, MinimumLength =3, ErrorMessage="Username harus lebih dari 3 karakter atau kurang dari 12 karakter")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Photo { get; set; }
    }
}