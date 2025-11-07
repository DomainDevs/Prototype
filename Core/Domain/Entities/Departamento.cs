using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace Domain.Entities
{
    [Table("Departamento", Schema = "dbo")]
    public class Departamento
    {
        // Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        // Nombre
        [Required]
        [Column("Nombre")]
        [MaxLength(200)]
        public string Nombre { get; set; }

    }
}