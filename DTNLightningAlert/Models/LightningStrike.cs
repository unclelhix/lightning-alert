

using DTNLightningAlert.Enums;

namespace DTNLightningAlert.Models
{

    /// <summary>
    /// Lightning Strike Model
    /// </summary>  
    public class LightningStrike
    {
        /// <summary>
        /// Flash Type of the Lightning
        /// </summary>   
        public FlashType FlashType { get; set; }      
        public long StrikeTime { get; set; } 
        public long ReceivedTime { get; set; }        
        public double Latitude { get; set; }   
        public double Longitude { get; set; }     
        public int PeakAmps { get; set; }
        public string Reserved { get; set; }
        public int IcHeight { get; set; }           
        public int NumberOfSensors { get; set; }      
        public int Multiplicity { get; set; }
    }
}
