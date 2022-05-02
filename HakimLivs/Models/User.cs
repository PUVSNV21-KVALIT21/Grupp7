using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HakimLivs.Models
{
    public class User
    {
        public int ID { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
    [Owned]
    public class Address
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
    }
}
