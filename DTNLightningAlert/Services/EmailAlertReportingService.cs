using DTNLightningAlert.Models;


namespace DTNLightningAlert.Services
{
    public class EmailAlertReportingService : IAlertReporterService
    {
        public void Report(Asset asset)
        {
            Console.WriteLine("Email Sent.");
        }
    }
}
