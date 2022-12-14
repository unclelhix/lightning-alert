using DTNLightningAlert.Enums;
using DTNLightningAlert.Models;
using DTNLightningAlert.Repository;

namespace DTNLightningAlert.Services
{
    /// <summary>
    /// This service handles the execution of lightning alert
    /// </summary>
    public class LightningAlertService : ILightningAlertService
    {
        private readonly HashSet<string> _assetsReported;        
        private readonly ILightningStrikeRepository _lightningStrikeRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly List<IAlertReporterService> _alertReporterService;

        public LightningAlertService(
            List<IAlertReporterService> alertReporterService,
            ILightningStrikeRepository lightningStrikeProcessor, 
            IAssetRepository assetProcessor, 
            HashSet<string> assetsReported)
        {
            _alertReporterService = alertReporterService;
            _assetRepository = assetProcessor;
            _lightningStrikeRepository = lightningStrikeProcessor;
            _assetsReported = assetsReported;
        }               

        public void ExecuteLightningAlert()
        {
            Console.WriteLine("Processing....");

            var lightningStrikes = _lightningStrikeRepository.GetLightningStrikes();

            foreach (var item in lightningStrikes)
            {

                if (item.FlashType == FlashType.HeartBeat)
                    continue;

                var asset = _assetRepository.GetAsset(item);

                if (asset == null)
                    continue;

                AddLightningStrikeAlert(asset);

            }

            Console.WriteLine("Done");
        }

        public HashSet<string> GetAssetsReported()
        {
            return _assetsReported;
        }

        private void AddLightningStrikeAlert(Asset asset)
        {
            if (asset == null || string.IsNullOrEmpty(asset.QuadKey) || _assetsReported.Contains(asset.QuadKey))
                return;

            _assetsReported.Add(asset.QuadKey);

            ExecuteAlert(asset);
        }

        private void ExecuteAlert(Asset asset)
        {
            _alertReporterService.ForEach(x=>x.Report(asset));            
        }
    }
}
