using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("Cliente", Schema = "dbo")]
    public class Cliente
    {
        // Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        // Nombre
        [Required]
        [Column("Nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        // Apellido
        [Column("Apellido")]
        [MaxLength(50)]
        public string Apellido { get; set; }

        // Email
        [Required]
        [Column("Email")]
        [MaxLength(50)]
        public string Email { get; set; }

    }
}