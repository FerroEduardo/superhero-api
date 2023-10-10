using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO;
using SuperHeroAPI.Models.Request;
using SuperHeroAPI.Services.SuperHeroService;
using SuperHeroAPI.Util;

namespace SuperHeroAPI.Controllers
{
    [Authorize]
    [Route("superhero")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService heroService;

        public SuperHeroController(ISuperHeroService heroService)
        {
            this.heroService = heroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHeroDTO>>> GetAllHeroes()
        {
            int userId = RequestUtil.GetUserId(this);
            IEnumerable<SuperHeroDTO> heroes = (await heroService.Index(userId))
                .Select(SuperHeroDTO.fromEntity);

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroDTO>> GetSingleHero([FromRoute] int id)
        {
            int userId = RequestUtil.GetUserId(this);
            var hero = await heroService.Show(id, userId);
            if (hero == null)
            {
                return NotFound();
            }
            var heroDto = SuperHeroDTO.fromEntity(hero);

            return Ok(heroDto);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHeroDTO>> AddHero([FromBody] SuperHeroRequest request)
        {
            int userId = RequestUtil.GetUserId(this);
            var hero = new SuperHero
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Place = request.Place,
                UserId = userId,
            };
            var superHero = await heroService.Create(hero);
            var heroDto = SuperHeroDTO.fromEntity(superHero);

            return CreatedAtAction(nameof(GetSingleHero), new { id = superHero.Id }, heroDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHeroDTO>> UpdateHero([FromRoute] int id, [FromBody] SuperHeroRequest request)
        {
            int userId = RequestUtil.GetUserId(this);
            var hero = new SuperHero
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Place = request.Place,
                UserId = userId,
            };
            hero = await heroService.Update(id, hero);
            var heroDto = SuperHeroDTO.fromEntity(hero);

            return Ok(heroDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero([FromRoute] int id)
        {
            int userId = RequestUtil.GetUserId(this);
            await heroService.Delete(id, userId);

            return NoContent();
        }
    }
}
