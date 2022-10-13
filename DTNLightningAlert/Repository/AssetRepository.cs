using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Helpers;
using DTNLightningAlert.Models;
using DTNLightningAlert.Services;
using System.Diagnostics;
using System.Text.Json;

namespace DTNLightningAlert.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private const int levelDetail = 12;

        private string _fileLocation;
        private readonly ITileSystemService _tileSystemService;
        private readonly Dictionary<string, Asset> _assetDictionary = new Dictionary<string, Asset>();

        public AssetRepository(
            ITileSystemService tileSystemService, 
            string fileLocation)
        {
            _fileLocation = fileLocation;
            _tileSystemService = tileSystemService;

            if (_fileLocation == null)
                throw new ArgumentNullException(nameof(_fileLocation));
            if (Path.GetExtension(_fileLocation) != ".json")
                throw new ArgumentException($"Invalid file extension.");
            if (!File.Exists(_fileLocation))
                throw new ArgumentException($"{_fileLocation} does not exist.");            

            ProcessAssetsData();
        }
        private void ProcessAssetsData()
        {
            try
            {

                var fileText = File.ReadAllText(_fileLocation);

                // Used JsonSerializerDefaults.Web to map decerialized object to Assets model properly
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
        
        public Asset GetAsset(LightningStrike lightningStrike)
        {            

            var lightningStrikeQuadKey = _tileSystemService.GetQuadKey(lightningStrike.Latitude,lightningStrike.Longitude, levelDetail);

            if (string.IsNullOrEmpty(lightningStrikeQuadKey))
                return null;

            _assetDictionary.TryGetValue(lightningStrikeQuadKey, out Asset asset);

            return asset;
        }

    }
}
