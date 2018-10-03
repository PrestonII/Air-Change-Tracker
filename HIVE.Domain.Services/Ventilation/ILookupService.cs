using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public interface ILookupService
    {
        double GetVentACHBasedOnOccupancyCategory(string category);
        double GetSupplyACHBasedOnOccupancyCategory(string category);
    }
}