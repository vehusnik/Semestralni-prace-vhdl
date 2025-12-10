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

        public string Popis { get; set; }      // Příznaky
        public string Diagnoza { get; set; }
        public string Doporuceni { get; set; } // Léčba
        public string Lekar { get; set; }      // Jméno lékaře
    }
}