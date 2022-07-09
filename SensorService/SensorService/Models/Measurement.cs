namespace SensorService.Models
{
    public class Measurement
    {
        public string SensorId { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string Humidity {  get; set; }
        public string Rain {  get; set; }
        public string Temperature { get; set; }
        public string Luminosity { get; set; }
    }
}
