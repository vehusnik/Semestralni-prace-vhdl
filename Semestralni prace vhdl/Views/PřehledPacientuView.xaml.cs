using DataEntity.Data;
using System.Windows;
using System.Windows.Controls;

namespace Semestralni_prace_vhdl.Views
{
    public partial class PřehledPacientuView : Window
    {
        public PřehledPacientuView()
        {
            InitializeComponent();
        }

        // TOTO JE TA METODA PRO TLAČÍTKO PŘIDAT
        private void PridatPacienta_Click(object sender, RoutedEventArgs e)
        {
            // Vytvoříme instanci okna pro nového pacienta
            NovyPacientWindow formular = new NovyPacientWindow();

            // Otevřeme ho.
            // ShowDialog() je lepší než Show(), protože nedovolí klikat do seznamu,
            // dokud tento formulář nezavřeš nebo neuložíš.
            formular.ShowDialog();
        }
        private void PridatZpravu_Click(object sender, RoutedEventArgs e)
        {
            // Otevřeme okno lékařské zprávy
            LekarskaZpravaWindow zpravaOkno = new LekarskaZpravaWindow();
            zpravaOkno.ShowDialog();
        }
        private void BtnUpravit_Click(object sender, RoutedEventArgs e)
        {
            // Získáme tlačítko, na které se kliklo
            Button btn = (Button)sender;

            // Získáme data z řádku (předpokládám, že vaše třída se jmenuje 'Pacient')
            var vybranyPacient = btn.DataContext as Pacienti;

            if (vybranyPacient != null)
            {
                // Zde otevřete okno pro úpravu a pošlete tam data
                // Příklad:
                // OknoUpravy okno = new OknoUpravy(vybranyPacient);
                // okno.Show();

                MessageBox.Show("Upravuji pacienta: " + vybranyPacient.Prijmeni); // Prozatímní test
            }
        }
    }
}