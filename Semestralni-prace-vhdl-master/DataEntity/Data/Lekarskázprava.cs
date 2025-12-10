using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataEntity.Data
{
    public class Lekarskazprava : Base.BaseModel
    {
        [Key]
        public int Id { get; set; }

        // Vazba na pacienta
        public int PacientId { get; set; }

        [ForeignKey("PacientId")]
        public virtual Pacienti Pacient { get; set; }

        public DateTime DatumVysetreni { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Popis je povinný.")]
        public string Popis { get; set; } = string.Empty;      // Příznaky
        
        public string Diagnoza { get; set; } = string.Empty;
        public string Doporuceni { get; set; } = string.Empty; // Léčba
        
        [Required(ErrorMessage = "Lékař je povinný.")]
        public string Lekar { get; set; } = string.Empty;      // Jméno lékaře
    }
}