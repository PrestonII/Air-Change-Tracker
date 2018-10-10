using HIVE.Domain.Entities;

namespace Hive.Domain.Services.Ventilation
{
    public interface ILookupService
    {
        double GetOAACHRBasedOnOccupancyCategory(string category);
        double GetACHRBasedOnOccupancyCategory(string category);
    }
}