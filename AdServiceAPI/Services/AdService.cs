using AdServiceAPI.Ads;
using AdServiceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AdServiceAPI.Services
{
    public class AdService : IAdService
    {
        private readonly ApplicationDbContext _dbContext;

        public AdService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Ad>> GetAll() =>
            _dbContext.Ads.ToListAsync();
            
        public Task<Ad?> GetOne(int id) =>
            _dbContext.Ads.FirstOrDefaultAsync(
                a => a.Id == id);

        public async Task<Ad> PostAd(NewAd newAd)
        {
            Ad ad = new Ad
            {
                Title = newAd.Title,
                Product = newAd.Product,
                Price = newAd.Price,
                StartDate = newAd.StartDate,
                EndDate = newAd.EndDate
            };

            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();
            return ad;
        }

        public async Task<Ad?> PutAd(int id, UpdateAd ad)
        {
            var adToUpdate = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);

            if (adToUpdate == null)
            {
                return null;
            }

            adToUpdate.Title = ad.Title;
            adToUpdate.Product = ad.Product;
            adToUpdate.Price = ad.Price;
            adToUpdate.StartDate = ad.StartDate;
            adToUpdate.EndDate = ad.EndDate;

            await _dbContext.SaveChangesAsync();
            return adToUpdate;
        }

        public async Task<Ad?> DeleteAd(int id)
        {
            var ad = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return null;
            }
            _dbContext.Ads.Remove(ad);

            await _dbContext.SaveChangesAsync();
            return ad;
        }
    }
}
