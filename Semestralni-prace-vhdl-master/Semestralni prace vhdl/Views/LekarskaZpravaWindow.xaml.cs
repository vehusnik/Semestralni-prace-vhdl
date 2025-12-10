using DataEntity;
using DataEntity.Data;
using Semestralni_prace_vhdl.ViewModels;
using System;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class LekarskaZpravaWindow : Window
    {
        // 1. Konstruktor s ID (volá se z hlavního okna)
        public LekarskaZpravaWindow(int pacientId)
        {
            InitializeComponent();
            // Inicializace ViewModelu s ID pacienta
            this.DataContext = new LekarskaZpravaVM(pacientId);
        }

        // 2. Prázdný konstruktor (pro XAML designer)
        public LekarskaZpravaWindow()
        {
            InitializeComponent();
            this.DataContext = new LekarskaZpravaVM();
        }

        // 3. Tlačítko ULOŽIT
        private void BtnUlozitZpravu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Získáme data z ViewModelu
                var viewModel = (LekarskaZpravaVM)this.DataContext;
                var zpravaKUlozeni = viewModel.NovaZprava;

                // Jednoduchá validace
                if (string.IsNullOrWhiteSpace(zpravaKUlozeni.Popis))
                {
                    MessageBox.Show("Vyplňte prosím popis obtíží.");
                    return;
                }

                // Otevřeme nové připojení pro uložení
                using (var db = new LekarContext())
                {
                    // Protože 'zpravaKUlozeni' byla vytvořena ve ViewModelu (kde už je kontext zavřený),
                    // je nyní "detached" (odpojená). Můžeme ji bezpečně přidat.
                    db.LekarskeZpravy.Add(zpravaKUlozeni);
                    db.SaveChanges();
                }

                MessageBox.Show("Zpráva byla úspěšně uložena.");

                // AKTUALIZACE UI:
                // 1. Přidáme zprávu do seznamu historie, aby ji uživatel hned viděl v tabulce vpravo
                viewModel.HistorieZprav.Insert(0, zpravaKUlozeni);

                // 2. Vyčistíme formulář pro psaní další zprávy (vytvoříme novou instanci)
                viewModel.NovaZprava = new Lekarskazprava
                {
                    PacientId = zpravaKUlozeni.PacientId,
                    DatumVysetreni = DateTime.Now,
                    Lekar = zpravaKUlozeni.Lekar
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při ukládání: " + ex.Message);
            }
        }
    }
}