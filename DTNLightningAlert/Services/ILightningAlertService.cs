
namespace DTNLightningAlert.Services
{
    public interface ILightningAlertService 
    {
        /// <summary>
        /// Excetues the lightning alert
        /// </summary>
        void ExecuteLightningAlert();
        /// <summary>
        /// Getting the Assetss Reported
        /// </summary>
        /// <returns>HashSet of string</returns>
        HashSet<string> GetAssetsReported();
    }
}
