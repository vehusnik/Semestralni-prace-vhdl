using DataEntity;
using DataEntity.Data;
using Semestralni_prace_vhdl.ViewModels;
using System;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class LekarskaZpravaWindow : Window
    {
        // 1. Konstruktor s ID (volá se z hlavního okna)
        public LekarskaZpravaWindow(int pacientId)
        {
            InitializeComponent();
            // Inicializace ViewModelu s ID pacienta
            this.DataContext = new LekarskaZpravaVM(pacientId);
        }

        // 2. Prázdný konstruktor (pro XAML designer)
        public LekarskaZpravaWindow()
        {
            InitializeComponent();
            this.DataContext = new LekarskaZpravaVM();
        }
    }
}