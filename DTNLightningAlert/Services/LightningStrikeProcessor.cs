using DTNLightningAlert.Enums;
using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Models;
using System.Diagnostics;
using System.Text.Json;

namespace DTNLightningAlert.Services
{
    public class LightningStrikeProcessor : ILightningStrikeProcessor
    {     
        private string _fileLocation;
        private readonly string _filePath = Directory.GetCurrentDirectory().Replace(@"bin\Debug\net6.0", @"DataSource\");
        public LightningStrikeProcessor(string fileName)
        {         
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            if (Path.GetExtension(fileName) != ".json")
                throw new ArgumentException($"Invalid file extension!");

            _fileLocation = GetFileLocation(fileName);
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
        private string GetFileLocation(string fileName)
        {
            return $"{_filePath}{fileName}";
        }
    }
}
