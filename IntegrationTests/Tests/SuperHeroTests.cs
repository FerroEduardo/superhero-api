using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroAPI.Models.DTO;
using SuperHeroAPI.Models.Request;
using SuperHeroAPI.Services;
using System.Net.Http.Json;

namespace IntegrationTests.Tests
{
    public class SuperHeroTests
    {
        [Fact]
        public async void Index()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                // Act
                var response = await client.GetAsync("/superhero");

                // Assert
                response.EnsureSuccessStatusCode();

                var superHeroes = await response.Content.ReadFromJsonAsync<IEnumerable<SuperHeroDTO>>();
                superHeroes.Should().NotBeNull();
                superHeroes.Should().HaveCount(1);
            }
        }

        [Theory]
        [InlineData(1)]
        public async void Show(int heroId)
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                // Act
                var response = await client.GetAsync($"/superhero/{heroId}");

                // Assert
                response.EnsureSuccessStatusCode();

                var superHero = await response.Content.ReadFromJsonAsync<SuperHeroDTO>();
                superHero.Should().NotBeNull();
            }
        }

        [Theory]
        [InlineData(2)]
        public async void Show_NotFound(int heroId)
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                // Act
                var response = await client.GetAsync($"/superhero/{heroId}");

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async void Create()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var request = new SuperHeroRequest
                {
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "New York"
                };

                // Act
                var response = await client.PostAsJsonAsync("/superhero", request);

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
                var superHero = await response.Content.ReadFromJsonAsync<SuperHeroDTO>();
                superHero.Should().NotBeNull();
                superHero!.Name.Should().Be(request.Name);
                superHero.FirstName.Should().Be(request.FirstName);
                superHero.LastName.Should().Be(request.LastName);
                superHero.Place.Should().Be(request.Place);
            }
        }

        [Fact]
        public async void Create_MissingFields()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var request = new SuperHeroRequest
                {
                    Name = "Iron Man",
                    FirstName = "Tony",
                };

                // Act
                var response = await client.PostAsJsonAsync("/superhero", request);

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [Theory]
        [InlineData(1)]
        public async void Update(int heroId)
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var request = new SuperHeroRequest
                {
                    Name = "Hulk",
                    FirstName = "Robert",
                    LastName = "Banner",
                    Place = "Dayton"
                };

                // Act
                var response = await client.PutAsJsonAsync($"/superhero/{heroId}", request);

                // Assert
                response.EnsureSuccessStatusCode();
                var superHero = await response.Content.ReadFromJsonAsync<SuperHeroDTO>();
                superHero.Should().NotBeNull();
                superHero!.Name.Should().Be(request.Name);
                superHero.FirstName.Should().Be(request.FirstName);
                superHero.LastName.Should().Be(request.LastName);
                superHero.Place.Should().Be(request.Place);
            }
        }

        [Theory]
        [InlineData(1)]
        public async void Update_MissingFields(int heroId)
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var request = new SuperHeroRequest
                {
                    Name = "Hulk",
                    FirstName = "Robert",
                };

                // Act
                var response = await client.PutAsJsonAsync($"/superhero/{heroId}", request);

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            }
        }

        [Theory]
        [InlineData(1)]
        public async void Delete(int heroId)
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            var client = application.CreateClient();
            using (var scope = application.Services.CreateScope())
            {
                var token = await authenticate(scope, client);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                // Act
                var deleteResponse = await client.DeleteAsync($"/superhero/{heroId}");

                // Assert
                deleteResponse.EnsureSuccessStatusCode();

                // Check if was in fact deleted
                var indexResponse = await client.GetAsync("/superhero");
                indexResponse.EnsureSuccessStatusCode();

                var superHeroes = await indexResponse.Content.ReadFromJsonAsync<IEnumerable<SuperHeroDTO>>();
                superHeroes.Should().NotBeNull().And.HaveCount(0);
            }
        }

        private async Task<string> authenticate(IServiceScope scope, HttpClient client)
        {
            AuthenticationRequest request = new AuthenticationRequest();
            request.Username = "eduardo";
            request.Password = "senha";

            // Act
            var response = await client.PostAsJsonAsync("/auth/signin", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadAsStringAsync();
            token.Should().NotBeNullOrEmpty();
            var tokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
            tokenService.IsValidToken(token).Should().BeTrue();

            return token;
        }
    }
}
