using DataEntity.Data;
using System.Windows.Input;
using System.Windows;

namespace Semestralni_prace_vhdl.ViewModels
{
    public class DetailZpravyVM : BaseVM
    {
        public Lekarskazprava Zprava { get; private set; }
        public ICommand CloseCommand { get; }

        public event System.EventHandler RequestClose;

        public DetailZpravyVM(Lekarskazprava zprava)
        {
            Zprava = zprava;
            CloseCommand = new Helpers.RelayCommand(Close);
        }

        // Pro Design time
        public DetailZpravyVM()
        {
            Zprava = new Lekarskazprava();
        }

        private void Close(object? parameter)
        {
            RequestClose?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
