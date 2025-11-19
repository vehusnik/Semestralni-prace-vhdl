using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Data
{
 public class Lekarskazprava
    {
        public int Id { get; set; }
        public int PacientId { get; set; }           // FK
        public DateTime Datumvysetreni { get; set; }
        public string Symptomy { get; set; } = string.Empty;
        public string Diagnoza { get; set; } = string.Empty;
        public string Doporuceni { get; set; } = string.Empty;

        public virtual Pacienti? Pacient { get; set; }       // inverse navigation
    }
}