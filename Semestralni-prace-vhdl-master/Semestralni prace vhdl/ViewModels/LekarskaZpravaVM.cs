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

        // Historie pro tabulku (ObservableCollection zajistí, že se tabulka sama aktualizuje)
        public ObservableCollection<Lekarskazprava> HistorieZprav { get; set; }

        // Formulář pro novou zprávu
        private Lekarskazprava _novaZprava;
        public Lekarskazprava NovaZprava
        {
            get => _novaZprava;
            set { _novaZprava = value; OnPropertyChanged(); }
        }

        // --- HLAVNÍ KONSTRUKTOR (používá se za běhu) ---
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
                Lekar = "MUDr. Jan Novák" // Zde si můžete nastavit výchozího lékaře
            };
        }

        // --- PRÁZDNÝ KONSTRUKTOR (Důležité pro XAML Designer) ---
        // Bez tohoto by vám mohl v designu zčervenat XAML kód
        public LekarskaZpravaVM()
        {
        }
    }
}