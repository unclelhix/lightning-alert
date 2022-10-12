
namespace DTNLightningAlert.Services
{
    public interface ILightningAlertService 
    {
        void ExecuteLightningAlert();
        HashSet<string> GetAssetsReported();
    }
}
