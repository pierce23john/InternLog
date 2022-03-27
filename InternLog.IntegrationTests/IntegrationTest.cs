using InternLog.Api.Contracts.V1;
using InternLog.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using InternLog.Api.Contracts.V1.Requests.Identity;
using InternLog.Api.Contracts.V1.Responses.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System;

namespace InternLog.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        protected readonly HttpClient HttpClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(SqlDataContext));
                        services.RemoveAll(typeof(DbContextOptions<SqlDataContext>));
                        services.AddDbContext<SqlDataContext>(options => options.UseInMemoryDatabase("InMemory"));
                    });
                });
            _serviceProvider = appFactory.Services;
            HttpClient = appFactory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        protected async Task<string> AuthenticateAsync()
        {
            var token = await GetJwtAsync();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await HttpClient.PostAsJsonAsync(ApiV1Routes.Identity.Register, new RegisterUserRequest()
            {
                Email = "test@integration.com",
                Password = "P@ssw0rd123!"
            });

            response.EnsureSuccessStatusCode();
            var tokenResponse = await response.Content.ReadFromJsonAsync<AuthenticationSuccessResponse>();
            return tokenResponse.Token;
        }

        protected static Guid GetUserIdFromJwt(string token)
        {
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return Guid.Parse(securityToken.Claims.First(claim => claim.Type == "id").Value);
        }

        public void Dispose()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<SqlDataContext>();
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}