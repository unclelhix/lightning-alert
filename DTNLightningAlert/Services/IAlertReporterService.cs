using DTNLightningAlert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Services
{
    public interface IAlertReporterService
    {
        void Report(Asset asset);
    }
}
