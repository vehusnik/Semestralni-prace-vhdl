using DataEntity;
using DataEntity.Data;
using Semestralni_prace_vhdl.Helpers;
using Semestralni_prace_vhdl.Views;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;

namespace Semestralni_prace_vhdl.ViewModels
{
    public class PřehledPacientuVM : BaseVM
    {
        // ZDE BYLA CHYBA: Přidali jsme otazník '?', protože na začátku není vybrán nikdo
        private Pacienti? _vybranyPacient;

        public ObservableCollection<Pacienti> Pacienti { get; set; } = new ObservableCollection<Pacienti>();

        // Veřejná vlastnost také musí umožňovat null (otazník)
        public Pacienti? VybranyPacient
        {
            get => _vybranyPacient;
            set
            {
                _vybranyPacient = value;
                OnPropertyChanged(); // Aktualizuje UI
            }
        }

        public RelayCommand OtevritZpravuCommand { get; set; }

        public PřehledPacientuVM()
        {
            // Načtení dat
            NačístPacienty();

            // Inicializace příkazu
            OtevritZpravuCommand = new RelayCommand(OtevritZpravu);
        }

        private void NačístPacienty()
        {
            using (var context = new LekarContext())
            {
                // Pokud je databáze prázdná, vytvoříme testovacího pacienta
                if (!context.Pacienti.Any())
                {
                    var testovaciPacient = new Pacienti
                    {
                        Jmeno = "Jan",
                        Prijmeni = "Novák",
                        Cislo_pojistence = "850101/1234", // Toto se zobrazí ve sloupci "Rodné číslo"
                        Adresa = "Václavské náměstí 1, Praha",
                        DatumNarozeni = new DateTime(1985, 1, 1),

                        // Ostatní vlastnosti (Pohlaví, Pojišťovna atd.) jsou Enums,
                        // takže se automaticky nastaví na jejich první hodnotu (0).
                        // Pokud chcete, můžete je nastavit ručně, např:
                        // Pojistovna = DataEntity.Data.Enum.InsuranceCompany.VZP 
                    };

                    context.Pacienti.Add(testovaciPacient);
                    context.SaveChanges();
                }

                // Načtení dat do tabulky
                var data = context.Pacienti.ToList();
                Pacienti = new ObservableCollection<Pacienti>(data);
            }
        }
        // Metoda pro otevření okna
        private void OtevritZpravu(object? obj)
        {
            if (VybranyPacient == null)
            {
                MessageBox.Show("Nejdříve vyberte pacienta ze seznamu.", "Upozornění");
                return;
            }

           
            LekarskaZpravaWindow okno = new LekarskaZpravaWindow(VybranyPacient.Id);

            okno.ShowDialog();
        }
    }
}