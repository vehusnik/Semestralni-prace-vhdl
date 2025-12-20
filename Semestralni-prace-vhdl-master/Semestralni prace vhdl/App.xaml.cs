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
                // Vytvoří databázi, pokud ještě není. 
                // POZOR: Pokud změníte model (přidáte sloupec), toto nebude fungovat 
                // a budete muset databázi smazat nebo použít Migrace.
                db.Database.EnsureCreated();

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