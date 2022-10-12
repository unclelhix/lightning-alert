using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Helpers;
using DTNLightningAlert.Models;
using System.Diagnostics;
using System.Text.Json;

namespace DTNLightningAlert.Services
{
    public class AssetProcessor : IAssetProcessor
    {
        private const int levelDetail = 12;

        private string _fileLocation;

        private readonly Dictionary<string, Asset> _assetDictionary = new Dictionary<string, Asset>();

        private readonly string _filePath = Directory.GetCurrentDirectory().Replace(@"bin\Debug\net6.0", @"DataSource\");
        public AssetProcessor(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            if (Path.GetExtension(fileName) != ".json")
                throw new ArgumentException($"Invalid file extension.");

            _fileLocation = GetFileLocation(fileName);
            if (!File.Exists(_fileLocation))
                throw new ArgumentException($"{_fileLocation} does not exist.");           

            ProcessAssetsData();
        }
        private void ProcessAssetsData()
        {
            try
            {

                var fileText = File.ReadAllText(_fileLocation);

                var assets = JsonSerializer.Deserialize<List<Asset>>(fileText, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                foreach (var asset in assets)
                {
                    if (string.IsNullOrEmpty(asset.QuadKey))
                        continue;

                    if (!_assetDictionary.ContainsKey(asset.QuadKey))
                        _assetDictionary.Add(asset.QuadKey, asset);
                   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Assets Proccesor encountered an error: {ex.Message}");
                throw new LightningAlertException($"{_fileLocation} invalid json format.");
            }
        }
        private string GetFileLocation(string fileName) {
            return _fileLocation = $"{_filePath}{fileName}";
        }
        public Asset GetAsset(LightningStrike lightningStrike)
        {
            TileSystem.LatLongToPixelXY(lightningStrike.Latitude, lightningStrike.Longitude, levelDetail, out int pixelX, out int pixelY);

            TileSystem.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);

            var lightningStrikeQuadKey = TileSystem.TileXYToQuadKey(tileX, tileY, levelDetail);

            if (string.IsNullOrEmpty(lightningStrikeQuadKey))
                return null;

            _assetDictionary.TryGetValue(lightningStrikeQuadKey, out Asset asset);

            return asset;
        }

    }
}
