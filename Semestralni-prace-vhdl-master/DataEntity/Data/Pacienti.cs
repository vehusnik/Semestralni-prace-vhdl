using DataEntity.Data.Enum;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Data
{
    [AddINotifyPropertyChangedInterface]
    public class Pacienti : Base.BaseModel
    {
        [Key]
        public int Id { get; set; }
        public TitleBefore Titulpřed { get; set; }
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(50, ErrorMessage = "Jméno nesmí být delší než 50 znaků.")]
        public string Jmeno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(50, ErrorMessage = "Příjmení nesmí být delší než 50 znaků.")]
        public string Prijmeni { get; set; } = string.Empty;
        public TitleAfter Titulza { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumNarozeni { get; set; }

        [StringLength(100, ErrorMessage = "Adresa nesmí být delší než 100 znaků.")]
        public string Adresa { get; set; } = string.Empty;
        public Gender Pohlaví { get; set; }
        public string Cislo_pojistence { get; set; } = string.Empty;
        public InsuranceCompany Pojistovna { get; set; }
        public string Doctor { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        // Místo public Lekarskazprava Lekarskazprava { get; set; }
        public virtual ICollection<Lekarskazprava> LekarskeZpravy { get; set; } = new List<Lekarskazprava>();
    }
}  
