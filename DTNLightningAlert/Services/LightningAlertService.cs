using DTNLightningAlert.Enums;
using DTNLightningAlert.Models;

namespace DTNLightningAlert.Services
{
    public class LightningAlertService : ILightningAlertService
    {
        private readonly HashSet<string> _assetsReported;        
        private readonly ILightningStrikeProcessor _lightningStrikeProcessor;
        private readonly IAssetProcessor _assetProcessor;
        public LightningAlertService(
            ILightningStrikeProcessor lightningStrikeProcessor, 
            IAssetProcessor assetProcessor, 
            HashSet<string> assetsReported)
        {
            _assetProcessor = assetProcessor;
            _lightningStrikeProcessor = lightningStrikeProcessor;
            _assetsReported = assetsReported;
        }               

        public void ExecuteLightningAlert()
        {
            Console.WriteLine("Processing....");

            var lightningStrikes = _lightningStrikeProcessor.GetLightningStrikes();

            foreach (var item in lightningStrikes)
            {

                if (item.FlashType == FlashType.HeartBeat)
                    continue;

                var asset = _assetProcessor.GetAsset(item);

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

            if (asset != null)
                Console.WriteLine($"lightning alert for {asset.AssetOwner}:{asset.AssetName}");
        }
    }
}
