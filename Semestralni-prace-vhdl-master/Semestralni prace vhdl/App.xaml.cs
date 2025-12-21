using System.Windows;
using DataEntity; // Přidat using pro váš kontext
using Microsoft.EntityFrameworkCore; // Pro Database.EnsureCreated

namespace Semestralni_prace_vhdl
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Hook up global exception handler
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            // Automatické vytvoření databáze při startu (pokud neexistuje)
            using (var db = new LekarContext())
            {
                try
                {
                    // Pokus o aplikaci migrací (vytvoří DB pokud není, nebo ji aktualizuje)
                    db.Database.Migrate();
                }
                catch (System.Exception)
                {
                    // Pokud migrace selže (např. konflikt se starou DB vytvořenou přes EnsureCreated),
                    // smažeme starou a vytvoříme novou čistou.
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                }

                // Seed databáze
                Semestralni_prace_vhdl.Helpers.DbSeeder.Initialize(db);
            }
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Log error to console/stderr so we can see it in 'dotnet run' output
            System.Console.Error.WriteLine("CRITICAL EXCEPTION: " + e.Exception.ToString());
            
            // Optionally prevent default crash behavior if you want to inspect 
            // e.Handled = true; 
        }
    }
}