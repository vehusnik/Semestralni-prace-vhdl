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
        private void TxtVyhledavani_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtVyhledavani.Text == "Zadejte jméno a příjmení")
            {
                TxtVyhledavani.Text = "";
                TxtVyhledavani.FontStyle = FontStyles.Normal;
                TxtVyhledavani.Foreground = System.Windows.Media.Brushes.Black;
            }
        }
        private void TxtVyhledavani_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtVyhledavani.Text))
            {
                TxtVyhledavani.Text = "Zadejte jméno a příjmení";
                TxtVyhledavani.FontStyle = FontStyles.Italic;
                TxtVyhledavani.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}