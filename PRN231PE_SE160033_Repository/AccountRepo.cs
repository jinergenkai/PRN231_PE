using Microsoft.EntityFrameworkCore;
using PRN231PE_SE160033_Repository.Models;

namespace PRN231PE_SE160033_Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly SilverJewelry2023DbContext _context;
        public AccountRepo(SilverJewelry2023DbContext context)
        {
            _context = context;
        }
        public Task<BranchAccount?> Get(string email, string password)
        {
            return _context.BranchAccounts.FirstOrDefaultAsync(b => b.EmailAddress == email && b.AccountPassword == password);
        }
    }
}
