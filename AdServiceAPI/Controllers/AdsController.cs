using AdServiceAPI.Ads;
using AdServiceAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AdServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdsController(IAdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            return Ok(await _adService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = await _adService.GetOne(id);

            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(NewAd newAd)
        {
            var ad = await _adService.PostAd(newAd);

            return CreatedAtAction(nameof(GetOne), new { id = ad.Id }, ad);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Ad>> PutAd(int id, UpdateAd ad)
        {
            var adToUpdate = await _adService.PutAd(id, ad);

            if (adToUpdate == null)
            {
                return NotFound();
            }

            return Ok(adToUpdate);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAd(int id)
        {
            var ad = await _adService.DeleteAd(id);

            if (ad == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Ad>> PatchAd(int id, JsonPatchDocument<UpdateAd> patchDoc)
        {
            var ad = await _adService.GetOne(id);

            if (ad == null)
            {
                return NotFound();
            }

            var updateAd = new UpdateAd
            {
                Title = ad.Title,
                Product = ad.Product,
                Price = ad.Price,
                StartDate = ad.StartDate,
                EndDate = ad.EndDate
            };

            patchDoc.ApplyTo(updateAd);

            if (!TryValidateModel(updateAd))
            {
                return ValidationProblem(ModelState);
            }

            var savedAd = await _adService.PutAd(id, updateAd);

            return Ok(savedAd);
        }
    }
}
