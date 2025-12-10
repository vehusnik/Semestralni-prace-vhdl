using DataEntity;
using DataEntity.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Důležité pro metodu ToList()
using System.Windows;
using System.Windows.Controls;

namespace Semestralni_prace_vhdl.Views
{
    public partial class PřehledPacientuView : Window
    {
        public PřehledPacientuView()
        {
            InitializeComponent();

            // Načíst data hned při spuštění okna
            NacistData();
        }

        // --- HLAVNÍ METODA PRO AKTUALIZACI SEZNAMU ---
        private void NacistData()
        {
            // Vytvoříme nové připojení k databázi
            using (var db = new LekarContext())
            {
                // Stáhneme aktuální seznam všech pacientů
                // .ToList() zajistí okamžité provedení SQL dotazu
                var seznam = db.Pacienti.ToList();

                // Naplníme tabulku (DataGrid) daty
                // Ujistěte se, že v XAML se tabulka jmenuje x:Name="DG_Pacienti"
                DG_Pacienti.ItemsSource = seznam;
            }
        }

        // Tlačítko PŘIDAT PACIENTA
        private void PridatPacienta_Click(object sender, RoutedEventArgs e)
        {
            NovyPacientWindow formular = new NovyPacientWindow();

            // Otevřeme okno jako "Dialog" (blokuje hlavní okno, dokud se nezavře)
            // ShowDialog() vrátí true, pokud jste ve formuláři dali "this.DialogResult = true"
            bool? vysledek = formular.ShowDialog();

            if (vysledek == true)
            {
                // Pokud uživatel klikl na Uložit, obnovíme tabulku
                NacistData();
            }
        }

        // Tlačítko UPRAVIT
        private void BtnUpravit_Click(object sender, RoutedEventArgs e)
        {
            // Zjistíme, který řádek je vybraný
            var vybranyPacient = DG_Pacienti.SelectedItem as Pacienti;

            if (vybranyPacient != null)
            {
                // Předáme vybraného pacienta do formuláře
                NovyPacientWindow formular = new NovyPacientWindow(vybranyPacient);

                // Opět čekáme na výsledek
                if (formular.ShowDialog() == true)
                {
                    // Pokud došlo k úpravě a uložení, obnovíme data
                    NacistData();
                }
            }
            else
            {
                MessageBox.Show("Prosím, vyberte pacienta k úpravě.");
            }
        }

        // Tlačítko ODSTRANIT
        private void BtnOdstranit_Click(object sender, RoutedEventArgs e)
        {
            var vybranyPacient = DG_Pacienti.SelectedItem as Pacienti;

            if (vybranyPacient != null)
            {
                var dotaz = MessageBox.Show($"Opravdu smazat pacienta {vybranyPacient.Prijmeni}?", "Potvrzení", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dotaz == MessageBoxResult.Yes)
                {
                    using (var db = new LekarContext())
                    {
                        // Musíme objekt připojit k tomuto kontextu nebo ho najít, abychom ho mohli smazat
                        db.Pacienti.Remove(vybranyPacient);
                        db.SaveChanges();
                    }
                    // Obnovíme seznam, aby zmizel smazaný řádek
                    NacistData();
                }
            }
            else
            {
                MessageBox.Show("Vyberte pacienta ke smazání.");
            }
        }

        // Ostatní metody (Lupa, Zpráva...)
        private void PridatZpravu_Click(object sender, RoutedEventArgs e)
        {
            var vybranyPacient = DG_Pacienti.SelectedItem as Pacienti;
            if (vybranyPacient != null)
            {
                // Zde otevřete okno zprávy (pokud ho máte)
                LekarskaZpravaWindow zpravaOkno = new LekarskaZpravaWindow(vybranyPacient.Id);
                zpravaOkno.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vyberte pacienta pro přidání zprávy.");
            }
        }

        // Kliknutí na lupu v řádku (pokud ji používáte)
        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            // 1. Získáme tlačítko (lupu), na které uživatel klikl
            var button = sender as Button;

            // 2. Z vlastnosti DataContext tlačítka získáme data o pacientovi na daném řádku
            var vybranyPacient = button.DataContext as Pacienti;

            if (vybranyPacient != null)
            {
                // 3. Vytvoříme okno lékařské zprávy a předáme mu ID pacienta
                LekarskaZpravaWindow zpravaOkno = new LekarskaZpravaWindow(vybranyPacient.Id);

                // 4. Otevřeme ho jako dialog (uživatel musí okno zavřít, než může klikat jinam)
                zpravaOkno.ShowDialog();
            }
        }
    }
    
}