using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly DataContext context;

        public SuperHeroService(DataContext context)
        {
            this.context = context;
        }

        public async Task<SuperHero> Create(SuperHero hero)
        {
            await this.context.SuperHeroes.AddAsync(hero);
            await this.context.SaveChangesAsync();

            return hero;
        }

        public async Task Delete(int id)
        {
            var superHero = await this.context.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                throw new Exception("Hero not found");
            }
            this.context.SuperHeroes.Remove(superHero);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(int id, int userId)
        {
            var superHero = await this.context.SuperHeroes.Where(hero => hero.Id == id && hero.UserId == userId).FirstOrDefaultAsync();
            if (superHero == null)
            {
                throw new Exception("Hero not found");
            }
            this.context.SuperHeroes.Remove(superHero);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<SuperHero>> Index()
        {
            return await this.context.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>> Index(int userId)
        {
            return await this.context.SuperHeroes.Where(hero => hero.UserId == userId).ToListAsync();
        }

        public async Task<SuperHero?> Show(int id)
        {
            return await this.context.SuperHeroes.FindAsync(id);
        }

        public async Task<SuperHero?> Show(int id, int userId)
        {
            return await this.context.SuperHeroes.Where(hero => hero.UserId == userId && hero.Id == id).FirstAsync();
        }

        public async Task<SuperHero> Update(int id, SuperHero hero)
        {
            var superHero = await this.context.SuperHeroes.FindAsync(id);
            if (superHero == null)
            {
                throw new Exception("Hero not found");
            }
            superHero.Name = hero.Name;
            superHero.Place = hero.Place;
            superHero.FirstName = hero.FirstName;
            superHero.LastName = hero.LastName;

            await this.context.SaveChangesAsync();

            return superHero;
        }
    }
}
