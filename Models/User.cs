using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba_backend.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public String Nombre { get; set; }
    [Column("correo")]
    public String Correo { get; set; }

    [ForeignKey("PerfilId")]
    public Guid PerfilId { get; set; }

}