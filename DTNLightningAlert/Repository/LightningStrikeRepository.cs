using DTNLightningAlert.Enums;
using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Models;
using System.Diagnostics;
using System.Text.Json;

namespace DTNLightningAlert.Repository
{
    public class LightningStrikeRepository : ILightningStrikeRepository
    {
        private string _fileLocation;
        
        public LightningStrikeRepository(string fileLocation)
        {
            _fileLocation = fileLocation;

            if (_fileLocation == null)
                throw new ArgumentNullException(nameof(_fileLocation));
            if (Path.GetExtension(_fileLocation) != ".json")
                throw new ArgumentException($"Invalid file extension!");           
            if (!File.Exists(_fileLocation))
                throw new LightningAlertException($"{_fileLocation} does not exist!");

        }

        public IEnumerable<LightningStrike> GetLightningStrikes()
        {

            using (var streamReader = File.OpenText(_fileLocation))
            {
                while (!streamReader.EndOfStream)
                {
                    var lineValue = streamReader.ReadLine();

                    if (string.IsNullOrEmpty(lineValue))
                        continue;

                    var lightningStrike = Deserialize(lineValue);

                    if (!IsLightningStrike((int)lightningStrike.FlashType))
                        continue;

                    if (lightningStrike != null)
                        yield return lightningStrike;
                }
            }
        }
        private LightningStrike Deserialize(string lineValue)
        {
            try
            {
                // Used JsonSerializerDefaults.Web to map  decerialized object to LightningStrike model properly
                var lightningStrike = JsonSerializer.Deserialize<LightningStrike>(lineValue, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return lightningStrike;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lightning Strike Processor encountered an error. {ex.Message}");
                throw new LightningAlertException($"{lineValue} invalid json format.");
            }

        }

        private static bool IsLightningStrike(int flashType)
        {
            return flashType == (int)FlashType.CloudToGround || flashType == (int)FlashType.CloudToCloud;
        }
        
    }
}
