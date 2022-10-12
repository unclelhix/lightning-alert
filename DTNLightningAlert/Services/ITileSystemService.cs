using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Services
{
    /// <summary>
    /// Implements Get Quad Key
    /// </summary>
    public interface ITileSystemService
    {
        /// <summary>
        /// Returns the QuadKey
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="levelDetail"></param>
        /// <returns></returns>
        string GetQuadKey(double latitude, double longitude, int levelDetail = 12);
    }
}
