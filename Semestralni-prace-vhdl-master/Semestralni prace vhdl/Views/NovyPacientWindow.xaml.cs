using DataEntity;
using DataEntity.Data;
using Microsoft.EntityFrameworkCore;
using Semestralni_prace_vhdl.ViewModels;
using System;
using System.Windows;

namespace Semestralni_prace_vhdl.Views
{
    public partial class NovyPacientWindow : Window
    {
        // ... Konstruktory máte z minula, ty jsou OK ...
        public NovyPacientWindow()
        {
            InitializeComponent();
            this.DataContext = new NovyPacientVM();
            Title = "Přidat nového pacienta";
        }

        public NovyPacientWindow(Pacienti pacientKUprave)
        {
            InitializeComponent();
            this.DataContext = new NovyPacientVM(pacientKUprave);
            Title = "Upravit existujícího pacienta";
        }

        // TOTO JE TA KLÍČOVÁ ČÁST PRO ZÁPIS
        private void BtnUlozit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Vytáhneme data z ViewModelu
                var viewModel = (NovyPacientVM)this.DataContext;
                var pacientKUlozeni = viewModel.Pacient;

                // 2. Otevřeme připojení k databázi
                using (var db = new LekarContext())
                {
                    // Rozhodneme, zda přidáváme nebo upravujeme
                    if (pacientKUlozeni.Id == 0)
                    {
                        // INSERT: Nový pacient nemá ID (je 0)
                        db.Pacienti.Add(pacientKUlozeni);
                        db.SaveChanges(); // Tady databáze vygeneruje ID a uloží to
                        MessageBox.Show("Uloženo do databáze!");
                    }
                    else
                    {
                        // UPDATE: Pacient už existuje
                        db.Pacienti.Update(pacientKUlozeni);
                        db.SaveChanges(); // Uloží změny
                        MessageBox.Show("Změny uloženy v databázi!");
                    }
                }

                // 3. Řekneme hlavnímu oknu, že se to povedlo
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při zápisu do DB: " + ex.Message);
            }
        }
    }
}