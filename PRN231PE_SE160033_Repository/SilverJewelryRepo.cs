using Microsoft.EntityFrameworkCore;
using PRN231PE_SE160033_Repository.Models;

namespace PRN231PE_SE160033_Repository
{
    public class SilverJewelryRepo : ISilverJewelryRepo
    {
        private readonly SilverJewelry2023DbContext _context;
        public SilverJewelryRepo(SilverJewelry2023DbContext context)
        {
            _context = context;
        }
        public async Task Add(SilverJewelry silverJewelry)
        {
            _context.Add(silverJewelry);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SilverJewelry silverjewelry)
        {
            _context.Remove(silverjewelry);
            await _context.SaveChangesAsync();
        }

        public Task<List<SilverJewelry>> Get()
        {
            return _context.SilverJewelries.Include(s => s.Category).ToListAsync();
        }

        public Task<SilverJewelry?> Get(string id)
        {
            return _context.SilverJewelries.Include(s => s.Category).FirstOrDefaultAsync(s => s.SilverJewelryId == id);
        }

        public async Task Update(SilverJewelry silverJewelry)
        {
            _context.SilverJewelries.Update(silverJewelry);
            await _context.SaveChangesAsync();
        }
    }
}
