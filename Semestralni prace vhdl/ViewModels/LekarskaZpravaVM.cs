using DataEntity;
using DataEntity.Data; // Pro Lekarskazprava, LekarContext
using Semestralni_prace_vhdl.Helpers; // Pro RelayCommand
using System;
using System.Windows;

namespace Semestralni_prace_vhdl.ViewModels
{
    public class LekarskaZpravaVM : BaseVM
    {
        private readonly int _pacientId;

        // Vlastnosti pro formulář (tyto bindujeme v XAML)
        // Inicializace = string.Empty je zde správně, aby nebyla null při startu
        public DateTime DatumVysetreni { get; set; } = DateTime.Now;
        public string Symptomy { get; set; } = string.Empty;
        public string Diagnoza { get; set; } = string.Empty;
        public string Doporuceni { get; set; } = string.Empty;

        // Používáme 'required' nebo inicializaci v konstruktoru
        public RelayCommand UlozitCommand { get; set; }

        // Hlavní konstruktor (volá se při otevření okna s ID pacienta)
        public LekarskaZpravaVM(int pacientId)
        {
            _pacientId = pacientId;
            UlozitCommand = new RelayCommand(UlozitZpravu);
        }

        // Prázdný konstruktor (potřebný pro XAML designer), 
        // ale musíme inicializovat Command, aby nepadal na null
        public LekarskaZpravaVM()
        {
            UlozitCommand = new RelayCommand(_ => { });
        }

        private void UlozitZpravu(object? obj)
        {
            // Validace (volitelné) - např. nedovolit uložit prázdnou zprávu
            if (string.IsNullOrWhiteSpace(Diagnoza))
            {
                MessageBox.Show("Vyplňte prosím alespoň diagnózu.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Vytvoření entity pro databázi
            var novaZprava = new Lekarskazprava
            {
                PacientId = _pacientId,
                Datumvysetreni = DatumVysetreni, // Vlevo DB název, vpravo ViewModel hodnota
                Symptomy = Symptomy,             // ZDE BLA CHYBA: Jen přiřadíme hodnotu, nemažeme ji
                Diagnoza = Diagnoza,
                Doporuceni = Doporuceni
            };

            try
            {
                // Uložení do databáze
                using (var context = new LekarContext())
                {
                    context.LekarskeZpravy.Add(novaZprava);
                    context.SaveChanges();
                }

                MessageBox.Show("Zpráva byla úspěšně uložena!", "Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Zavření okna
                if (obj is Window w) w.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}