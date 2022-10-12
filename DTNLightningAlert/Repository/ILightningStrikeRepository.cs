using DTNLightningAlert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Repository
{
    public interface ILightningStrikeRepository
    {
        IEnumerable<LightningStrike> GetLightningStrikes();
    }
}
