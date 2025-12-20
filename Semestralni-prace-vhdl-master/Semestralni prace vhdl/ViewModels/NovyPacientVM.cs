using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using DataEntity;
using DataEntity.Data;
using DataEntity.Data.Enum; // Pro Enums
using System;
using System.Collections.Generic;

namespace Semestralni_prace_vhdl.ViewModels
{
    // Dědíme z BaseVM, abychom mohli používat OnPropertyChanged
    public class NovyPacientVM : BaseVM
    {
        // Událost pro zavření okna (View se přihlásí)
        public event EventHandler? RequestClose;

        // --- 1. DATA PACIENTA ---
        private Pacienti _pacient;
        public Pacienti Pacient
        {
            get => _pacient;
            set
            {
                _pacient = value;
                OnPropertyChanged();
                // Validaci celého objektu bychom mohli spouštět zde, pokud by to bylo potřeba
            }
        }

        // --- 2. ENUMY PRO COMBOBOXY (Zdroje dat) ---
        public IEnumerable<TitleBefore> TitulyPred => Enum.GetValues(typeof(TitleBefore)).Cast<TitleBefore>();
        public IEnumerable<TitleAfter> TitulyZa => Enum.GetValues(typeof(TitleAfter)).Cast<TitleAfter>();
        public IEnumerable<Gender> PohlaviMožnosti => Enum.GetValues(typeof(Gender)).Cast<Gender>();
        public IEnumerable<InsuranceCompany> Pojistovny => Enum.GetValues(typeof(InsuranceCompany)).Cast<InsuranceCompany>();

        // --- COMMANDS ---
        public ICommand SaveCommand { get; }

        // --- 3. KONSTRUKTORY ---

        // Konstruktor pro přidání NOVÉHO pacienta
        public NovyPacientVM()
        {
            Pacient = new Pacienti
            {
                DatumNarozeni = DateTime.Now // Defaultní datum (dnešek), aby tam nebylo 0001
            };
            SaveCommand = new Helpers.RelayCommand(Save, CanSave);
        }

        // Konstruktor pro ÚPRAVU existujícího pacienta
        public NovyPacientVM(Pacienti existujiciPacient)
        {
            Pacient = existujiciPacient;
            SaveCommand = new Helpers.RelayCommand(Save, CanSave);
        }

        private bool CanSave(object? parameter)
        {
            // Povolit uložení pouze pokud nejsou žádné validační chyby
            // V tomto jednoduchém případě můžeme spoléhat na validaci při pokusu o uložení
            // nebo implementovat průběžnou kontrolu validace modelu Pacient.
            // Pro začátek povolíme vždy a validujeme při Save.
            return true; 
        }

        private void Save(object? parameter)
        {
            if (!ValidatePacient())
            {
                MessageBox.Show("Prosím opravte chyby ve formuláři.", "Chyba validace", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new LekarContext())
                {
                    if (Pacient.Id == 0)
                    {
                        db.Pacienti.Add(Pacient);
                        db.SaveChanges();
                        MessageBox.Show("Pacient úspěšně uložen.", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        db.Pacienti.Update(Pacient);
                        db.SaveChanges();
                        MessageBox.Show("Změny uloženy.", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                
                // Signál pro zavření okna
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidatePacient()
        {
            var context = new ValidationContext(Pacient);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(Pacient, context, results, true);

            if (!isValid)
            {
                // Zde bychom mohli naplnit Errors dictionary v BaseVM, aby se chyby zobrazily v UI
                // Ale protože bindujeme přímo vlastnosti Pacient objektu, UI validace (červený rámeček)
                // by se měla postarat sama, pokud Pacient implementuje IDataErrorInfo nebo
                // pokud Binding používá ValidatesOnExceptions/ValidatesOnNotifyDataErrors 
                // a Pacient hází výjimky. 
                
                // Jelikož Pacient používá DataAnnotations, musíme zajistit, aby UI o chybách vědělo.
                // Nejjednodušší cesta pro Strict MVVM s DataAnnotations na Modelu je, 
                // aby Model implementoval INotifyDataErrorInfo (což jsme neudělali, Pacient je jen POCO s atributy)
                // NEBO aby ViewModel "obaloval" vlastnosti.
                
                // V tomto případě, pro zjednodušení a zachování stávající struktury,
                // zobrazíme chyby v MessageBoxu (jak je v kódu výše) a spoléháme na to,
                // že WPF ValidatesOnDataErrors zobrazí chyby v UI pokud upravíme XAML.
            }

            return isValid;
        }
    }
}