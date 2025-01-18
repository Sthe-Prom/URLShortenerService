using URLShortenerService.Interfaces;
using URLShortenerService.Models.Repos;
using Microsoft.EntityFrameworkCore;

namespace URLShortenerService.Models.EF
{
    public class EFMetroURL: IMetroURL
    {
        public AppDbContext _context;

        public EFMetroURL(AppDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<MetroURL> MetroURLs => _context.MetroURL;

        public async Task<IEnumerable<MetroURL>> GetAllMetroURL()
        {
            return await _context.MetroURL.ToListAsync();
        }

        public async Task<MetroURL> GetMetroURLById(int id)
        {
            return await _context.MetroURL.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<MetroURL> AddMetroURL(MetroURL url)
        {
            await _context.AddAsync(url);

            try
            {
                await _context.SaveChangesAsync();
                return url;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log error, throw specific exception)
                throw new Exception("Failed to Add url.", ex);
            }

        }

        public async Task<MetroURL> UpdateMetroURL(MetroURL url)
        {
            _context.Update(url);

            try
            {
                await _context.SaveChangesAsync();
                return url;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log error, throw specific exception)
                throw new Exception("Failed to update url.", ex);
            }

        }

    }
}