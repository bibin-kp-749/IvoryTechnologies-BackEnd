using MachinTest_Backend.Model;
using MachinTest_Backend.Model.DTO;

namespace MachinTest_Backend.Service.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLoactions();
        Task<bool> AddLoaction(LocationDto details);
        Task<bool> DeletLoaction(string Name);
    }
}
