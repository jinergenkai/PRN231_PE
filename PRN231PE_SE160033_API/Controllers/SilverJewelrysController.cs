using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231PE_SE160033_API.Models;
using PRN231PE_SE160033_Repository;
using PRN231PE_SE160033_Repository.Models;

namespace PRN231PE_SE160033_API.Controllers
{

    public class SilverJewelrysController : ODataController
    {
        private readonly ISilverJewelryRepo _repo;
        public SilverJewelrysController(ISilverJewelryRepo repo)
        {
            _repo = repo;
        }
        [EnableQuery]
        [Authorize(Roles = "4,1")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.Get());
        }
        [EnableQuery]
        [Authorize(Roles = "4,1")]
        public async Task<IActionResult> Get([FromODataUri] string key)
        {
            var entiy = await _repo.Get(key);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Post([FromBody] SilverJelRequest request)
        {
            if (request.ProductionYear < 1900)
            {
                ModelState.AddModelError(nameof(request.ProductionYear), "Invalid Production Year");
            }
            if (request.Price < 0)
            {
                ModelState.AddModelError(nameof(request.Price), "Invalid Price");
            }
            string name = request.SilverJewelryName.Trim();
            for (int i = 0; i < name.Length; ++i)
            {
                if (!(name[i] == ' ' || (name[i] >= 'a' && name[i] <= 'z')
                    || (name[i] >= 'A' && name[i] <= 'Z') || (name[i] >= '0' && name[i] <= 9)))
                {
                    ModelState.AddModelError(nameof(request.SilverJewelryName), "Invalid Character");
                }
                if (i > 0 && name[i - 1] == ' ')
                {
                    if (!(name[i] >= 'A' && name[i] <= 'Z'))
                    {
                        ModelState.AddModelError(nameof(request.SilverJewelryName), "Invalid Name, Each Words Should Begin With Capital");
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SilverJewelry j = new SilverJewelry
            {
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
                MetalWeight = request.MetalWeight,
                Price = request.Price,
                SilverJewelryId = request.SilverJewelryId,
                SilverJewelryDescription = request.SilverJewelryDescription,
                SilverJewelryName = name,
                ProductionYear = request.ProductionYear,
            };
            await _repo.Add(j);
            return Created(j);
        }
        [HttpPut]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Put([FromODataUri] string key, [FromBody] SilverJelRequest request)
        {
            if (request.ProductionYear < 1900)
            {
                ModelState.AddModelError(nameof(request.ProductionYear), "Invalid Production Year");
            }
            if (request.Price < 0)
            {
                ModelState.AddModelError(nameof(request.Price), "Invalid Price");
            }
            string name = request.SilverJewelryName.Trim();
            for (int i = 0; i < name.Length; ++i)
            {
                if (!(name[i] == ' ' || (name[i] >= 'a' && name[i] <= 'z')
                    || (name[i] >= 'A' && name[i] <= 'Z') || (name[i] >= '0' && name[i] <= 9)))
                {
                    ModelState.AddModelError(nameof(request.SilverJewelryName), "Invalid Character");
                }
                if (i > 0 && name[i - 1] == ' ')
                {
                    if (!(name[i] >= 'A' && name[i] <= 'Z'))
                    {
                        ModelState.AddModelError(nameof(request.SilverJewelryName), "Invalid Name, Each Words Should Begin With Capital");
                    }
                }
            }
            SilverJewelry j = new SilverJewelry
            {
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
                MetalWeight = request.MetalWeight,
                Price = request.Price,
                SilverJewelryId = request.SilverJewelryId,
                SilverJewelryDescription = request.SilverJewelryDescription,
                SilverJewelryName = name,
                ProductionYear = request.ProductionYear,
            };
            await _repo.Update(j);
            return Updated(j);
        }
        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete([FromODataUri] string key)
        {
            var j = await _repo.Get(key);
            if (j == null)
            {
                return NotFound();
            }
            await _repo.Delete(j);
            return NoContent();
        }

    }
}
