using DataEntity.Data;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    // OPRAVA 1: Třída se musí jmenovat stejně jako soubor -> NovyPacientWindow
    public partial class NovyPacientWindow : Window
    {
        public Pacienti PacientData { get; set; }

        // --- VARIANTA A: Nový pacient ---
        public NovyPacientWindow()
        {
            InitializeComponent(); // Teď už to nebude svítit červeně
            PacientData = new Pacienti();
            this.DataContext = PacientData;
            Title = "Přidat nového pacienta";
        }

        // --- VARIANTA B: Úprava pacienta ---
        public NovyPacientWindow(Pacienti pacientKUprave)
        {
            InitializeComponent();
            PacientData = pacientKUprave;
            this.DataContext = PacientData;
            Title = "Upravit existujícího pacienta";
        }

        // --- Tlačítko ULOŽIT ---
        private void BtnUlozit_Click(object sender, RoutedEventArgs e)
        {
            // OPRAVA 2: Tady jste měl překlep "PacientiData". Správně je "PacientData".
            if (PacientData.Id == 0)
            {
                // INSERT (Nový)
                // Zde doplňte volání databáze, např.: _context.Pacienti.Add(PacientData); _context.SaveChanges();
                MessageBox.Show("Nový pacient byl přidán.");
            }
            else
            {
                // UPDATE (Existující)
                // Zde doplňte volání databáze, např.: _context.SaveChanges();
                MessageBox.Show("Změny byly uloženy.");
            }

            this.Close();
        }
    }
}