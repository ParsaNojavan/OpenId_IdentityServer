using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace Identity_Server.Utilities
{

    public static class TestUsers
    {
        public static List<TestUser> Users = new()
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "parsanojavan",
                Password = "Device33",
                IsActive = true,
                Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, "parsa.nojavan85@gmail.com"),
                    new Claim(ClaimTypes.MobilePhone, "09372347173"),
                    new Claim("fullName", "Parsa Nojavan"),
                    new Claim("website","https://developersera.ir")
                }
            }
        };
    }

}
