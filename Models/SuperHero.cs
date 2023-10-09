using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace SuperHeroAPI.Models
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
