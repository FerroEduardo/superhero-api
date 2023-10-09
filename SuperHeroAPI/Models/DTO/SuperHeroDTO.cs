namespace SuperHeroAPI.Models.DTO
{
    public record SuperHeroDTO (
        string Name,
        string FirstName,
        string LastName,
        string Place
    )
    {
        public static SuperHeroDTO fromEntity(SuperHero hero)
        {
            return new SuperHeroDTO(
                hero.Name,
                hero.FirstName,
                hero.LastName,
                hero.Place
            );
        }
    }
}
