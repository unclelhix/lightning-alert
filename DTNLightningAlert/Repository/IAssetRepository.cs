using DTNLightningAlert.Models;


namespace DTNLightningAlert.Repository
{
    public interface IAssetRepository
    {
        Asset GetAsset(LightningStrike lightningStrike);
    }
}
