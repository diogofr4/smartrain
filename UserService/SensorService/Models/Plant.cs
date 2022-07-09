using UserService.Models;

namespace SensorService.Models
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string SensorId { get; set; }
        public List<Measurement> Measurements { get; set; }
        public IrrigationConfig IrrigationConfig { get; set; }
    }
}
