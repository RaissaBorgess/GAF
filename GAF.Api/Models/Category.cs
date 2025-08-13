using Sytem.ComponentModel.DataAnnotations;
using Sytem.ComponentModel.DataAnnotations.Schema;

namespace GAF.Api.Models;

[Table("categorias")]
public class Category
{
    [Key]
    public int id { get; set; }
    
    [Required]
    [StringLenght(100)]
    public string Name { get; set; }

    [StringLenght(500)]
    public string Description { get; set; }

    [Required]
    [StringLenght(7)]
    public string Color { get; set; } = "#000000";

    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
