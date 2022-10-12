// See https://aka.ms/new-console-template for more information
using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Services;
using DTNLightningAlert.Repository;

IAssetRepository _assetRepo;
ILightningStrikeRepository _lightningStrikeRepo;
ITileSystemService _tileSystemService;
List<IAlertReporterService> _alertReportService = new List<IAlertReporterService>();

HashSet<string> _assetsReported = new HashSet<string>();

while (true)
{
    try
    {
        Console.WriteLine("Enter Lightning Strike File Name:");
        var lightningFileName = Console.ReadLine();

        Console.WriteLine("Enter Assets File Name:");
        var assetsFileName = Console.ReadLine();        

        _tileSystemService = new TileSystemService();

        _assetRepo = new AssetRepository(_tileSystemService, assetsFileName);

        _lightningStrikeRepo = new LightningStrikeRepository(lightningFileName);

        _alertReportService.AddRange(new IAlertReporterService[] { new EmailAlertReportingService(), new ConsoleAlertReportingService() });

        var lightningAlertService = new LightningAlertService(_alertReportService, _lightningStrikeRepo, _assetRepo, _assetsReported);

        lightningAlertService.ExecuteLightningAlert();

        _assetsReported = lightningAlertService.GetAssetsReported();

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


    Console.Write("Enter Y to continue:");
    if (Console.ReadLine() != "Y")
        break;

}

