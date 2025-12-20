using Semestralni_prace_vhdl.ViewModels;
using DataEntity.Data;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class DetailZpravyWindow : Window
    {
        public DetailZpravyWindow(Lekarskazprava zprava)
        {
            InitializeComponent();
            var vm = new DetailZpravyVM(zprava);
            vm.RequestClose += (s, e) => this.Close();
            this.DataContext = vm;
        }
        
        public DetailZpravyWindow()
        {
            InitializeComponent();
        }
    }
}
