// See https://aka.ms/new-console-template for more information
using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Services;

IAssetProcessor _assetProcessor;
ILightningStrikeProcessor _lightningStrikeProcessor;
HashSet<string> _assetsReported = new HashSet<string>();

while (true)
{
    try
    {
        Console.WriteLine("Enter Lightning Strike File Name:");
        var lightningFileName = Console.ReadLine();

        Console.WriteLine("Enter Assets File Name:");
        var assetsFileName = Console.ReadLine();

        _assetProcessor = new AssetProcessor(assetsFileName);

        _lightningStrikeProcessor = new LightningStrikeProcessor(lightningFileName);

        var lightningAlertService = new LightningAlertService(_lightningStrikeProcessor, _assetProcessor, _assetsReported);

        lightningAlertService.ExecuteLightningAlert();

        _assetsReported = lightningAlertService.GetLightningAssetAlerts();

    }

    catch (ArgumentException argumentException)
    {
        Console.WriteLine($"Error: {argumentException.Message}. Please contact administrator");
    }
    catch (LightningAlertException ex)
    {
        Console.WriteLine($"Error: {ex.Message}. Please contact administrator");
    }
    catch (Exception)
    {
        Console.WriteLine($"Error: Please contact the administrator");
    }


    Console.WriteLine("Enter Y to continue:");
    if (Console.ReadLine() != "Y")
        break;

}

