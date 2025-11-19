using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Data.Enum
{
    public class Enums
    {

        public enum TitleBefore
        {
            [Description("Žádný")]
            None = 0,

            [Description("Mgr.")]
            Mgr = 1,

            [Description("MUDr.")]
            MUDr = 2,

            [Description("Ing.")]
            Ing = 3,

            [Description("JUDr.")]
            JUDr = 4
        }

        public enum TitleAfter
        {
            [Description("Žádný")]
            None = 0,

            [Description("Ph.D.")]
            PhD = 1,

            [Description("CSc.")]
            CSc = 2,

            [Description("MBA")]
            MBA = 3
        }

        public enum Gender
        {
            [Description("Muž")]
            Muž = 0,

            [Description("Žena")]
            Žena = 1
        }

        public enum InsuranceCompany
        {
            [Description("Všeobecná zdravotní pojišťovna")]
            VšeobecnáZdravotníPojišťovna = 0,

            [Description("Česká pojišťovna")]
            ČeskáPojišťovna = 1,

            [Description("Zdravotní pojišťovna Ministerstva vnitra")]
            ZdravotníPojišťovnaMinisterstvaVnitra = 2,

            [Description("Oborová zdravotní pojišťovna")]
            OborováZdravotníPojišťovna = 3,

            [Description("Revírní bratrská pokladna")]
            RevírníBratrskáPokladna = 4,

            [Description("VoZP ČR")]
            VoZPČR = 5,

            [Description("Zdravotní pojišťovna Škoda")]
            ZdravotníPojišťovnaŠkoda = 6
        }
    }
}