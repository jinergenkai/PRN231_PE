using PRN231PE_SE160033_Repository.Models;

namespace PRN231PE_SE160033_Repository
{
    public interface ISilverJewelryRepo
    {
        Task<List<SilverJewelry>> Get();
        Task<SilverJewelry?> Get(string id);
        Task Add(SilverJewelry silverJewelry);
        Task Delete(SilverJewelry silverjewelry);
        Task Update(SilverJewelry silverJewelry);
    }
}
