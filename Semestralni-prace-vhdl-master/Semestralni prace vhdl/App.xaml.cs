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

            // Automatické vytvoření databáze při startu (pokud neexistuje)
            using (var db = new LekarContext())
            {
                // Vytvoří databázi, pokud ještě není. 
                // POZOR: Pokud změníte model (přidáte sloupec), toto nebude fungovat 
                // a budete muset databázi smazat nebo použít Migrace.
                db.Database.EnsureCreated();
            }
        }
    }
}