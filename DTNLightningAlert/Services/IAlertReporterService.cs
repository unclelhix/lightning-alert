using DTNLightningAlert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Services
{
    /// <summary>
    /// Implements Report
    /// </summary>
    public interface IAlertReporterService
    {
        /// <summary>
        /// Reports the affected assets by lightning
        /// </summary>
        /// <param name="asset"></param>
        void Report(Asset asset);
    }
}
