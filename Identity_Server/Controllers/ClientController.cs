using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using static Duende.IdentityModel.OidcConstants;
using GrantTypes = Duende.IdentityServer.Models.GrantTypes;
using IdentityResource = Duende.IdentityServer.EntityFramework.Entities.IdentityResource;

namespace Identity_Server.Controllers
{
    public class ClientController : Controller
    {
        private readonly ConfigurationDbContext _configuration;

        public ClientController(ConfigurationDbContext configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var clients = _configuration.Clients.ToList();
            return View(clients);
        }

        public IActionResult Add()
        {

            _configuration.IdentityResources.Add(new IdentityResource
            {
                Name = "openid",
                DisplayName = "OpenID",
                Enabled = true,
                Required = true,
                UserClaims = new List<IdentityResourceClaim>
    {
        new IdentityResourceClaim { Type = "sub" }
    }
            });

            _configuration.IdentityResources.Add(new IdentityResource
            {
                Name = "profile",
                DisplayName = "Profile",
                Enabled = true,
                UserClaims = new List<IdentityResourceClaim>
    {
        new IdentityResourceClaim { Type = "name" },
        new IdentityResourceClaim { Type = "family_name" },
        new IdentityResourceClaim { Type = "preferred_username" }
    }
            });

            _configuration.IdentityResources.Add(new IdentityResource
            {
                Name = "email",
                DisplayName = "Email",
                Enabled = true,
                UserClaims = new List<IdentityResourceClaim>
    {
        new IdentityResourceClaim { Type = "email" }
    }
            });


            _configuration.Clients.Add(new Duende.IdentityServer.EntityFramework.Entities.Client
            {
                ClientId = "NojavanEnterpriseId",
                RequirePkce = true,
                RedirectUris = new List<Duende.IdentityServer.EntityFramework.Entities.ClientRedirectUri>
                {
                    new Duende.IdentityServer.EntityFramework.Entities.ClientRedirectUri
                    {
                        RedirectUri = "https://localhost:7081/signin-oidc"
                    }
                },

                PostLogoutRedirectUris = new List<Duende.IdentityServer.EntityFramework.Entities.ClientPostLogoutRedirectUri>
                {
                    new Duende.IdentityServer.EntityFramework.Entities.ClientPostLogoutRedirectUri
                    {
                        PostLogoutRedirectUri = "https://localhost:7081/"
                    }
                },

               ClientSecrets = new List<Duende.IdentityServer.EntityFramework.Entities.ClientSecret>
               {
                   new Duende.IdentityServer.EntityFramework.Entities.ClientSecret
                   {
                        Value = "123456".Sha256()
                   }
               },

               AllowedGrantTypes = GrantTypes.Code
               .Select(gt => new Duende.IdentityServer.EntityFramework.Entities.ClientGrantType
               {
                    GrantType = gt
               }).ToList(),

                AllowedScopes = new List<Duende.IdentityServer.EntityFramework.Entities.ClientScope>
               {
                    new Duende.IdentityServer.EntityFramework.Entities.ClientScope { Scope = StandardScopes.OpenId },
                    new Duende.IdentityServer.EntityFramework.Entities.ClientScope { Scope = StandardScopes.Profile },
                    new Duende.IdentityServer.EntityFramework.Entities.ClientScope { Scope = StandardScopes.Email }
               }


            });

            _configuration.SaveChanges();

            return Ok();
        }
    }
}
