using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class PhotoRepository<T> : IPhotoRepository<T> where T : class
    {
        private readonly CarDealershipContext _context;

        public PhotoRepository(CarDealershipContext context)
        {
            _context = context;
        }
        public void DeleteRange(List<T> photos)
        {
            _context.Set<T>().RemoveRange(photos);
        }

        public async Task AddRangeAsync(List<T> photos)
        {
            await _context.Set<T>().AddRangeAsync(photos);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
