using DataEntity;
using DataEntity.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Semestralni_prace_vhdl.ViewModels
{
    public class LekarskaZpravaVM : BaseVM
    {
        // Data pro zobrazení v hlavičce
        public Pacienti Pacient { get; set; }

        // Historie pro tabulku
        public ObservableCollection<Lekarskazprava> HistorieZprav { get; set; }

        // Formulář pro novou zprávu
        private Lekarskazprava _novaZprava;
        public Lekarskazprava NovaZprava
        {
            get => _novaZprava;
            set { _novaZprava = value; OnPropertyChanged(); }
        }

        // --- COMMANDS ---
        public System.Windows.Input.ICommand SaveCommand { get; }
        public System.Windows.Input.ICommand OpenDetailCommand { get; }

        // --- HLAVNÍ KONSTRUKTOR ---
        public LekarskaZpravaVM(int pacientId)
        {
            using (var db = new LekarContext())
            {
                // 1. Načteme info o pacientovi
                Pacient = db.Pacienti.FirstOrDefault(p => p.Id == pacientId);

                // 2. Načteme historii zpráv
                var historie = db.LekarskeZpravy
                                 .Where(z => z.PacientId == pacientId)
                                 .OrderByDescending(z => z.DatumVysetreni)
                                 .ToList();

                HistorieZprav = new ObservableCollection<Lekarskazprava>(historie);
            }

            // 3. Připravíme prázdnou novou zprávu
            NovaZprava = new Lekarskazprava
            {
                PacientId = pacientId,
                DatumVysetreni = DateTime.Now,
                Lekar = "MUDr. Jan Novák" // Výchozí lékař
            };

            SaveCommand = new Helpers.RelayCommand(Save);
            OpenDetailCommand = new Helpers.RelayCommand(OpenDetail);
        }

        // --- PRÁZDNÝ KONSTRUKTOR (pro Design time) ---
        public LekarskaZpravaVM()
        {
        }

        private void Save(object? parameter)
        {
            // Validace
            if (string.IsNullOrWhiteSpace(NovaZprava.Popis))
            {
                System.Windows.MessageBox.Show("Popis je povinný.", "Chyba", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new LekarContext())
                {
                    db.LekarskeZpravy.Add(NovaZprava);
                    db.SaveChanges();
                }

                // Aktualizace UI - historie
                HistorieZprav.Insert(0, NovaZprava);
                System.Windows.MessageBox.Show("Zpráva uložena.", "Úspěch", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                // Reset formuláře
                NovaZprava = new Lekarskazprava
                {
                    PacientId = NovaZprava.PacientId,
                    DatumVysetreni = DateTime.Now,
                    Lekar = NovaZprava.Lekar
                };
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Chyba při ukládání: {ex.Message}", "Chyba", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void OpenDetail(object? parameter)
        {
            if (parameter is Lekarskazprava zprava)
            {
                var detailWindow = new Semestralni_prace_vhdl.Views.DetailZpravyWindow(zprava);
                detailWindow.ShowDialog();
            }
        }
    }
}