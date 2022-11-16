using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
