using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Semestralni_prace_vhdl.ViewModels
{
    // Toto je základ pro "mozek" oken
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}