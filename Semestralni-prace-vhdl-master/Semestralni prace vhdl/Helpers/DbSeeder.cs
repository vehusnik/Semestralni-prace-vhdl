using DataEntity;
using DataEntity.Data;
using DataEntity.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Semestralni_prace_vhdl.Helpers
{
    public static class DbSeeder
    {
        public static void Initialize(LekarContext context)
        {
            // Pokud už existuje dostatek dat (např. více než 5), nic neděláme.
            if (context.Pacienti.Count() >= 5)
            {
                return;
            }

            var pacienti = new List<Pacienti>
            {
                CreatePacient("Jan", "Novák", "Karlova 12, Praha", "1234567890", Gender.Muž, TitleBefore.Ing, TitleAfter.None, InsuranceCompany.VšeobecnaZdravotniPojistovna, "MUDr. Svoboda"),
                CreatePacient("Petr", "Svoboda", "Dlouhá 5, Brno", "9876543210", Gender.Muž, TitleBefore.MUDr, TitleAfter.PhD, InsuranceCompany.CeskaPojistovna, "MUDr. Novotný"),
                CreatePacient("Jana", "Dvořáková", "Masarykova 1, Plzeň", "1122334455", Gender.Žena, TitleBefore.Mgr, TitleAfter.None, InsuranceCompany.ZdravotniPojistovnaMinisterstvaVnitra, "MUDr. Kovář"),
                CreatePacient("Eva", "Černá", "Lipová 8, Ostrava", "5544332211", Gender.Žena, TitleBefore.None, TitleAfter.None, InsuranceCompany.RevirniBratrskaPokladna, "MUDr. Svoboda"),
                CreatePacient("Tomáš", "Procházka", "U Lesa 9, Liberec", "6677889900", Gender.Muž, TitleBefore.Ing, TitleAfter.MBA, InsuranceCompany.ZdravotniPojistovnaSkoda, "MUDr. Novotný"),
                CreatePacient("Lucie", "Kučerová", "Zámecká 3, Pardubice", "0099887766", Gender.Žena, TitleBefore.MUDr, TitleAfter.None, InsuranceCompany.VšeobecnaZdravotniPojistovna, "MUDr. Kovář"),
                CreatePacient("Marek", "Veselý", "Nová 4, České Budějovice", "1231231234", Gender.Muž, TitleBefore.None, TitleAfter.None, InsuranceCompany.OborovaZdravotniPojistovna, "MUDr. Svoboda"),
                CreatePacient("Adéla", "Horáková", "Stará 7, Hradec Králové", "3213214321", Gender.Žena, TitleBefore.Mgr, TitleAfter.None, InsuranceCompany.CeskaPojistovna, "MUDr. Novotný"),
                CreatePacient("Pavel", "Němec", "Vítězná 2, Ústí nad Labem", "7894561230", Gender.Muž, TitleBefore.JUDr, TitleAfter.None, InsuranceCompany.VoZPCR, "MUDr. Kovář"),
                CreatePacient("Veronika", "Pokorná", "Lidická 6, Zlín", "9638527410", Gender.Žena, TitleBefore.None, TitleAfter.None, InsuranceCompany.VšeobecnaZdravotniPojistovna, "MUDr. Svoboda"),
                CreatePacient("Martin", "Krejčí", "Školní 11, Jihlava", "1472583690", Gender.Muž, TitleBefore.Ing, TitleAfter.None, InsuranceCompany.ZdravotniPojistovnaMinisterstvaVnitra, "MUDr. Novotný"),
                CreatePacient("Zuzana", "Křížová", "Mírová 8, Karlovy Vary", "1597534680", Gender.Žena, TitleBefore.MUDr, TitleAfter.CSc, InsuranceCompany.CeskaPojistovna, "MUDr. Kovář"),
                CreatePacient("Jiří", "Pospíšil", "Husova 10, Olomouc", "7531598420", Gender.Muž, TitleBefore.None, TitleAfter.None, InsuranceCompany.RevirniBratrskaPokladna, "MUDr. Svoboda"),
                CreatePacient("Lenka", "Bílková", "Růžová 5, Kladno", "3579514860", Gender.Žena, TitleBefore.Mgr, TitleAfter.None, InsuranceCompany.VoZPCR, "MUDr. Novotný"),
                CreatePacient("David", "Urban", "Sokolská 3, Most", "8524569630", Gender.Muž, TitleBefore.Ing, TitleAfter.PhD, InsuranceCompany.ZdravotniPojistovnaSkoda, "MUDr. Kovář")
            };

            context.Pacienti.AddRange(pacienti);
            context.SaveChanges();

            // Přidáme zprávy (Data jsou uložena, takže pacienti mají ID)
            // Ale EF Core sledování entit by mělo fungovat i bez nového dotazu, 
            // pokud přidáváme do podřízených kolekcí. Ale zde jsme vytvořili Pacienty bez zpráv.
            // Můžeme iterovat a přidat zprávy.

            var rnd = new Random();
            foreach (var p in pacienti)
            {
                int pocetZprav = rnd.Next(1, 4); // 1 až 3 zprávy
                for (int i = 0; i < pocetZprav; i++)
                {
                    var zprava = new Lekarskazprava
                    {
                        PacientId = p.Id,
                        DatumVysetreni = DateTime.Now.AddDays(-rnd.Next(1, 365)), // Za poslední rok
                        Lekar = p.Doctor, // Použijeme ošetřujícího lékaře
                        Popis = GetNahodnyPopis(rnd),
                        Diagnoza = GetNahodnaDiagnoza(rnd),
                        Doporuceni = "Klidový režim, kontrola za týden."
                    };
                    context.LekarskeZpravy.Add(zprava);
                }
            }
            context.SaveChanges();
        }

        private static Pacienti CreatePacient(string jmeno, string prijmeni, string adresa, string cp, Gender pohlavi, TitleBefore titulPred, TitleAfter titulZa, InsuranceCompany pojistovna, string lekar)
        {
            return new Pacienti
            {
                Jmeno = jmeno,
                Prijmeni = prijmeni,
                Adresa = adresa,
                Cislo_pojistence = cp, // Unikátnost bychom měli hlídat, ale pro seed je to OK
                DatumNarozeni = DateTime.Now.AddYears(-new Random().Next(20, 80)),
                Pohlaví = pohlavi,
                Titulpřed = titulPred,
                Titulza = titulZa,
                Pojistovna = pojistovna,
                Doctor = lekar,
                Notes = "Automaticky vygenerovaný záznam."
            };
        }

        private static string GetNahodnyPopis(Random rnd)
        {
            string[] symptomy = { 
                "Bolest hlavy a únava.", 
                "Kašel, rýma, zvýšená teplota.", 
                "Bolest v krku, chrapot.", 
                "Nevolnost, závratě.", 
                "Bolest zad v bederní oblasti.", 
                "Alergická reakce, vyrážka.",
                "Zvýšený krevní tlak.",
                "Bolest kloubů při pohybu."
            };
            return symptomy[rnd.Next(symptomy.Length)];
        }

        private static string GetNahodnaDiagnoza(Random rnd)
        {
            string[] diagnozy = { 
                "Migréna", 
                "Akutní infekce dýchacích cest", 
                "Angína", 
                "Vertigo", 
                "Lumbago", 
                "Dermatitida",
                "Hypertenze",
                "Artróza"
            };
            return diagnozy[rnd.Next(diagnozy.Length)];
        }
    }
}
