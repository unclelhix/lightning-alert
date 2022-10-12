using DTNLightningAlert.Exceptions;
using DTNLightningAlert.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
* Bing Maps Tile System
* https://learn.microsoft.com/en-us/bingmaps/articles/bing-maps-tile-system?redirectedfrom=MSDN
* **/

namespace DTNLightningAlert.Services
{
    /**
     * This class encapsulates the Tile System Computation
     * If ever the library has changes we only need to update this service 
     * and it's not tightly coupled to any Repository
     **/

    /// <summary>
    /// Tile System Service
    /// </summary>
    public class TileSystemService : ITileSystemService
    {

        public string GetQuadKey(double latitude, double longtitude, int levelDetail = 12)
        {
            if (levelDetail < 1 || levelDetail > 23)
                throw new ArgumentException($"{nameof(levelDetail)} should be within 1 - 23 only");

            try
            {
                TileSystem.LatLongToPixelXY(latitude, longtitude, levelDetail, out int pixelX, out int pixelY);
                TileSystem.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);

                return TileSystem.TileXYToQuadKey(tileX, tileY, levelDetail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Quand Key Error: {ex.Message}");

                throw new LightningAlertException($"Error converting Lat and Long to QuadKey: {ex.Message}");               
            }
        }
    }
}
