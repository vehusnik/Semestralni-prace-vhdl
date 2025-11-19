using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Data
{
    public class Pacienti
    {
        [Key]
        public int PacientID (get; set; }
    [Required (ErrorMessage = "Jméno je povinné.")]
        [StringLength(50, ErrorMessage = "Jméno nesmí být delší než 50 znaků.")]
        public string Jmeno { get; set; }
        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(50, ErrorMessage = "Příjmení nesmí být delší než 50 znaků.")]
        public string Prijmeni { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumNarozeni { get; set; }
        [StringLength(100, ErrorMessage = "Adresa nesmí být delší než 100 znaků.")]
        public string Adresa { get; set; }
        [Phone(ErrorMessage = "Neplatné telefonní číslo.")]
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        public string Email { get; set; }
    }
}
