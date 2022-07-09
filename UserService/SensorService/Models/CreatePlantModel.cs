using Newtonsoft.Json;

namespace UserService.Models
{
    public class CreatePlantModel
    {
        [JsonProperty("plantName")]
        public string PlantName { get; set; }
        [JsonProperty("sensorId")]
        public string SensorId { get; set; }
        [JsonProperty("minHumidity")]
        public double MinHumidity { get; set; }
        [JsonProperty("maxTemperature")]
        public double MaxTemperature { get; set; }
        [JsonProperty("plantId")]
        public int? PlantId { get; set; }
    }
}
