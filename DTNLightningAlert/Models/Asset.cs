using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Models
{
   
    public class Asset
    {

        public string AssetName { get; set; }
        /// <summary>
        /// Quadkey specifies the location of the Asset
        /// </summary>
        public string QuadKey { get; set; }
        public string AssetOwner { get; set; }
    }
}
