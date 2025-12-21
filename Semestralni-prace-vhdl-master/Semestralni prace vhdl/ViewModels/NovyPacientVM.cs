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
        // Helper to get descriptions
        private IEnumerable<string> GetEnumDescriptions<T>() where T : Enum
        {
             return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => 
                {
                    var field = e.GetType().GetField(e.ToString());
                    var attr = (System.ComponentModel.DescriptionAttribute?)Attribute.GetCustomAttribute(field!, typeof(System.ComponentModel.DescriptionAttribute));
                    return attr?.Description ?? e.ToString();
                })
                .Where(s => s != "Žádný"); // Filter out "None" if desired, or keep it.
        }

        public IEnumerable<string> TitulyPred => GetEnumDescriptions<TitleBefore>();
        public IEnumerable<string> TitulyZa => GetEnumDescriptions<TitleAfter>();
        public IEnumerable<Gender> PohlaviMožnosti => Enum.GetValues(typeof(Gender)).Cast<Gender>();
        public IEnumerable<InsuranceCompany> Pojistovny => Enum.GetValues(typeof(InsuranceCompany)).Cast<InsuranceCompany>();

        // Properties for Picker
        private string? _selectedTitlePred;
        public string? SelectedTitlePred
        {
            get => _selectedTitlePred;
            set { _selectedTitlePred = value; OnPropertyChanged(); }
        }

        private string? _selectedTitleZa;
        public string? SelectedTitleZa
        {
            get => _selectedTitleZa;
            set { _selectedTitleZa = value; OnPropertyChanged(); }
        }

        // --- COMMANDS ---
        public ICommand SaveCommand { get; }
        public ICommand AddTitlePredCommand { get; }
        public ICommand AddTitleZaCommand { get; }

        // --- 3. KONSTRUKTORY ---

        // Konstruktor pro přidání NOVÉHO pacienta
        public NovyPacientVM()
        {
            Pacient = new Pacienti
            {
                DatumNarozeni = DateTime.Now // Defaultní datum (dnešek), aby tam nebylo 0001
            };
            SaveCommand = new Helpers.RelayCommand(Save, CanSave);
            AddTitlePredCommand = new Helpers.RelayCommand(_ => AddTitle(true));
            AddTitleZaCommand = new Helpers.RelayCommand(_ => AddTitle(false));
        }

        // Helper Method for adding titles
        private void AddTitle(bool isPred)
        {
            if (isPred)
            {
                if (string.IsNullOrWhiteSpace(SelectedTitlePred)) return;
                
                if (string.IsNullOrWhiteSpace(Pacient.Titulpřed))
                {
                    Pacient.Titulpřed = SelectedTitlePred;
                }
                else
                {
                    Pacient.Titulpřed += " " + SelectedTitlePred;
                }
                OnPropertyChanged(nameof(Pacient)); 
            }
            else
            {
                if (string.IsNullOrWhiteSpace(SelectedTitleZa)) return;

                if (string.IsNullOrWhiteSpace(Pacient.Titulza))
                {
                    Pacient.Titulza = SelectedTitleZa;
                }
                else
                {
                    Pacient.Titulza += " " + SelectedTitleZa;
                }
            }
        }

        // Konstruktor pro ÚPRAVU existujícího pacienta
        public NovyPacientVM(Pacienti existujiciPacient)
        {
            Pacient = existujiciPacient;
            SaveCommand = new Helpers.RelayCommand(Save, CanSave);
            AddTitlePredCommand = new Helpers.RelayCommand(_ => AddTitle(true));
            AddTitleZaCommand = new Helpers.RelayCommand(_ => AddTitle(false));
        }

        private bool CanSave(object? parameter)
        {
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
            return isValid;
        }
    }
}