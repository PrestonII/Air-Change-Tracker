namespace HIVE.Domain.Entities
{
    public class Space : Element
    {
        public double Area { get; set; }
        public double CeilingHeight { get; set; }
        public double CFM_Exhaust { get; set; }
        public double CFM_Supply { get; set; }
        public double CFM_Vent { get; set; }
        public double NumberOfPeople { get; set; }
        public double PercentageOfOutsideAir { get; set; }
        public string OccupancyCategory { get; set; }
    }
}
