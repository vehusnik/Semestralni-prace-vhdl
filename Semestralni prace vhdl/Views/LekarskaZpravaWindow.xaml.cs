using Semestralni_prace_vhdl.ViewModels;
using System.Windows; // Nutné pro přístup k ViewModelu

namespace Semestralni_prace_vhdl.Views // Pozor na namespace
{
    public partial class LekarskaZpravaWindow : Window
    {
        // Konstruktor, který přijímá ID pacienta
        public LekarskaZpravaWindow(int pacientId)
        {
            InitializeComponent();

            // Nastavíme DataContext na náš ViewModel a pošleme mu ID
            this.DataContext = new LekarskaZpravaVM(pacientId);
        }

        // Tento prázdný konstruktor tam nechte, kdyby ho XAML potřeboval, 
        // ale my budeme používat ten horní.
        public LekarskaZpravaWindow()
        {
            InitializeComponent();
            this.DataContext = new LekarskaZpravaVM();
        }
    }
}