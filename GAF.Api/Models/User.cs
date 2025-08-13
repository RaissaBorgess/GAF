using Sytem.ComponentModel.DataAnnotations;
using Sytem.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GAF.Api.Models;

 [Table("users")]
    public class User : IdentityUser
    {
        [Required]
        [StringLenght(100)]
        public string Name { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        
    }