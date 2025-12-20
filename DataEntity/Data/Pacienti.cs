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
        [StringLength(50, ErrorMessage = "Titul před nesmí být delší než 50 znaků.")]
        public string Titulpřed { get; set; } = string.Empty;
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(50, ErrorMessage = "Jméno nesmí být delší než 50 znaků.")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Jméno nesmí obsahovat číslice.")]
        public string Jmeno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(50, ErrorMessage = "Příjmení nesmí být delší než 50 znaků.")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Příjmení nesmí obsahovat číslice.")]
        public string Prijmeni { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "Titul za nesmí být delší než 50 znaků.")]
        public string Titulza { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DatumNarozeni { get; set; }

        [Required(ErrorMessage = "Adresa je povinná.")]
        [StringLength(100, ErrorMessage = "Adresa nesmí být delší než 100 znaků.")]
        public string Adresa { get; set; } = string.Empty;

        public Gender Pohlaví { get; set; }

        [Required(ErrorMessage = "Číslo pojištěnce je povinné.")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Číslo pojištěnce musí obsahovat pouze číslice a maximálně 10 znaků.")]
        public string Cislo_pojistence { get; set; } = string.Empty;

        public InsuranceCompany Pojistovna { get; set; }

        [Required(ErrorMessage = "Lékař je povinný údaj.")]
        public string Doctor { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        // Místo public Lekarskazprava Lekarskazprava { get; set; }
        public virtual ICollection<Lekarskazprava> LekarskeZpravy { get; set; } = new List<Lekarskazprava>();
    }
}  
