using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HakimLivs.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
    [Owned]
    public class Address
    {
        [Required]
        [Display(Name = "Gatuadress")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Postkod")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "Stad")]
        public string City { get; set; }
    }
}
