using AdServiceAPI.Ads;
using Microsoft.EntityFrameworkCore;

namespace AdServiceAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }

        public void SeedData()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (!_dbContext.Ads.Any(
                a => a.Title == "Skobonanza"))
            {
                _dbContext.Add(new Ad
                {
                    Title = "Skobonanza",
                    Product = "Löparsko",
                    Price = 1000,
                    StartDate = today.AddDays(10),
                    EndDate = today.AddDays(20),
                });
            }

            if (!_dbContext.Ads.Any(
               a => a.Title == "Balla brallor"))
            {
                _dbContext.Add(new Ad
                {
                    Title = "Balla brallor",
                    Product = "Byxa",
                    Price = 900,
                    StartDate = today,
                    EndDate = today.AddDays(12),
                });
            }

            if (!_dbContext.Ads.Any(
                a => a.Title == "Tuffa tishor"))
            {
                _dbContext.Add(new Ad
                {
                    Title = "Tuffa tishor",
                    Product = "T-shirt",
                    Price = 150,
                    StartDate = today.AddDays(30),
                    EndDate = today.AddDays(37),
                });
            }
        }
    }
}
