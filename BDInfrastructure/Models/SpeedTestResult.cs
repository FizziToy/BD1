namespace BDInfrastructure.Models
{
    public class SpeedTestResult
    {
        public double SqlTimeMs { get; set; }
        public int SqlCount { get; set; }
        public double MongoTimeMs { get; set; }
        public int MongoCount { get; set; }
        public int Runs { get; set; }
    }
}