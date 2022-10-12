using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTNLightningAlert.Exceptions
{
    public class LightningAlertException : Exception
    {
        public LightningAlertException(string message) : base(message)
        { }
    }
}
