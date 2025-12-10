using DataEntity.Data;
using DataEntity.Data.Enum; // Pro Enums
using System;
using System.Collections.Generic;
using System.Linq;

namespace Semestralni_prace_vhdl.ViewModels
{
    // Dědíme z BaseVM, abychom mohli používat OnPropertyChanged
    public class NovyPacientVM : BaseVM
    {
        // --- 1. DATA PACIENTA ---
        private Pacienti _pacient;
        public Pacienti Pacient
        {
            get => _pacient;
            set
            {
                _pacient = value;
                OnPropertyChanged(); // Upozorní formulář při změně celého objektu
            }
        }

        // --- 2. ENUMY PRO COMBOBOXY (Zdroje dat) ---
        // Tyto seznamy se nemění, takže stačí obyčejný getter
        public IEnumerable<TitleBefore> TitulyPred => Enum.GetValues(typeof(TitleBefore)).Cast<TitleBefore>();
        public IEnumerable<TitleAfter> TitulyZa => Enum.GetValues(typeof(TitleAfter)).Cast<TitleAfter>();
        public IEnumerable<Gender> PohlaviMožnosti => Enum.GetValues(typeof(Gender)).Cast<Gender>();
        public IEnumerable<InsuranceCompany> Pojistovny => Enum.GetValues(typeof(InsuranceCompany)).Cast<InsuranceCompany>();

        // --- 3. KONSTRUKTORY ---

        // Konstruktor pro přidání NOVÉHO pacienta
        public NovyPacientVM()
        {
            Pacient = new Pacienti();
        }

        // Konstruktor pro ÚPRAVU existujícího pacienta
        public NovyPacientVM(Pacienti existujiciPacient)
        {
            Pacient = existujiciPacient;
        }
    }
}