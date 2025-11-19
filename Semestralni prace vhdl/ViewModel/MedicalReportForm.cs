using System;
using System.Windows;
using DataEntity;
using DataEntity.Data;

namespace Semestralni_prace_vhdl.ViewModel
{
    public partial class MedicalReportForm : Window
    {
        private readonly int patientId;

        public MedicalReportForm(int patientId)
        {
            InitializeComponent();
            this.patientId = patientId;
        }

        // WPF uses RoutedEventArgs for button clicks
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Defensive: if date picker SelectedDate is null, use now
            var examinationDate = dtpExaminationDate.SelectedDate ?? DateTime.Now;

            var report = new Lekarskazprava
            {
                PacientId = patientId,
                Datumvysetreni = examinationDate,
                Symptomy = (Symptomy?.Text) ?? string.Empty,
                Diagnoza = (txtDiagnosis?.Text) ?? string.Empty,
                Doporuceni = (txtRecommendations?.Text) ?? string.Empty
            };

            using (var context = new LekarContext())
            {
                context.LekarskeZpravy.Add(report);
                context.SaveChanges();
            }

            MessageBox.Show("Lékařská zpráva byla úspěšně uložena.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}