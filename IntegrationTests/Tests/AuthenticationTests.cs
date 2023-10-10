using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroAPI.Models.Request;
using SuperHeroAPI.Models.Response;
using SuperHeroAPI.Services;
using System.Net.Http.Json;

namespace IntegrationTests.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public async void Signup()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            using (var scope = application.Services.CreateScope())
            {
                AuthenticationRequest request = new AuthenticationRequest();
                request.Username = "user";
                request.Password = "password";

                var client = application.CreateClient();

                // Act
                var response = await client.PostAsJsonAsync("/auth/signup", request);

                // Assert
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                responseBody.Should().NotBeNullOrEmpty();
                var tokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
                tokenService.IsValidToken(responseBody).Should().BeTrue();
            }
        }

        [Fact]
        public async void Signin()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            using (var scope = application.Services.CreateScope())
            {
                AuthenticationRequest request = new AuthenticationRequest();
                request.Username = "eduardo";
                request.Password = "senha";

                var client = application.CreateClient();

                // Act
                var response = await client.PostAsJsonAsync("/auth/signin", request);

                // Assert
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                responseBody.Should().NotBeNullOrEmpty();
                var tokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
                tokenService.IsValidToken(responseBody).Should().BeTrue();
            }
        }

        [Fact]
        public async void Signin_WrongPassword()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            using (var scope = application.Services.CreateScope())
            {
                AuthenticationRequest request = new AuthenticationRequest();
                request.Username = "eduardo";
                request.Password = "password";

                var client = application.CreateClient();

                // Act
                var response = await client.PostAsJsonAsync("/auth/signin", request);

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

                var responseBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                responseBody.Should().NotBeNull();
                responseBody!.Message.Should().Be("Invalid credentials.");
            }
        }


        [Fact]
        public async void Signup_UsernameAlreadyTaken()
        {
            // Arrange
            var application = new SuperHeroWebApplicationFactory();
            using (var scope = application.Services.CreateScope())
            {
                AuthenticationRequest request = new AuthenticationRequest();
                request.Username = "eduardo";
                request.Password = "password";

                var client = application.CreateClient();

                // Act
                var response = await client.PostAsJsonAsync("/auth/signup", request);

                // Assert
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

                var responseBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                responseBody.Should().NotBeNull();
                responseBody!.Message.Should().Be("Username already taken.");
            }
        }
    }
}