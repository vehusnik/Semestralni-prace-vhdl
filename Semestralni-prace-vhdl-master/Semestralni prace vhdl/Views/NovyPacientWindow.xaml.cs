using DataEntity.Data;
using Semestralni_prace_vhdl.ViewModels;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class NovyPacientWindow : Window
    {
        public NovyPacientWindow()
        {
            InitializeComponent();
            var vm = new NovyPacientVM();
            vm.RequestClose += (s, e) => 
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = vm;
            Title = "Přidat nového pacienta";
        }

        public NovyPacientWindow(Pacienti pacientKUprave)
        {
            InitializeComponent();
            var vm = new NovyPacientVM(pacientKUprave);
            vm.RequestClose += (s, e) => 
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = vm;
            Title = "Upravit existujícího pacienta";
        }
    }
}