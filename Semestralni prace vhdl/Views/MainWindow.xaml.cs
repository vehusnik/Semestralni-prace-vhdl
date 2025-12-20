using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OtevritSeznam_Click(object sender, RoutedEventArgs e)
        {
            // Toto otevře to druhé okno s tabulkou
            PřehledPacientuView okno = new PřehledPacientuView();
            okno.Show();
        }


    }
}