using AdServiceAPI.Ads;

namespace AdServiceAPI.Services
{
    public interface IAdService
    {
        Task<List<Ad>> GetAll();

        Task<Ad?> GetOne(int id);

        Task<Ad> PostAd(NewAd newAd);

        Task<Ad?> PutAd(int id, UpdateAd ad);

        Task<Ad?> DeleteAd(int id);
    }
}
