using DTNLightningAlert.Models;

namespace DTNLightningAlert.Services
{
    public class ConsoleAlertReportingService : IAlertReporterService
    {
        public void Report(Asset asset)
        {
            if (asset != null)
                Console.WriteLine($"lightning alert for {asset.AssetOwner}:{asset.AssetName}");
        }
    }
}
