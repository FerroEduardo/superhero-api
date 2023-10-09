using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        Task<List<SuperHero>> Index(int userId);
        Task<SuperHero?> Show(int id, int userId);
        Task Delete(int id, int userId);
        Task<SuperHero> Create(SuperHero hero);
        Task<SuperHero> Update(int id, SuperHero hero);
    }
}
