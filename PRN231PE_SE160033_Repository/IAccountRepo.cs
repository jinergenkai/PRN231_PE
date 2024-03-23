using PRN231PE_SE160033_Repository.Models;

namespace PRN231PE_SE160033_Repository
{
    public interface IAccountRepo
    {
        Task<BranchAccount?> Get(string email, string password);
    }
}
