using AdServiceAPI.Ads;
using AdServiceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = await _dbContext.Ads.FirstOrDefaultAsync(
                a => a.Id == id);

            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(NewAd newAd)
        {
            Ad ad = new Ad { 
                Title = newAd.Title, 
                Product = newAd.Product,
                Price = newAd.Price, 
                StartDate = newAd.StartDate,
                EndDate = newAd.EndDate
            };

            _dbContext.Ads.Add(ad);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOne), new { id = ad.Id }, ad);
        }


    }
}
