using Duende.IdentityServer.Models;

namespace Identity_Server
{
    public class Config
    {

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("userApi")
                {
                    Scopes = new List<string>{ "user.read", "user.write", "shared" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                },
                new ApiResource("orderApi")
                {
                    Scopes = new List<string>{ "order.read", "order.write", "shared"},
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("user.read", "Read Access in User API"),
            new ApiScope("user.write", "Write Access in User API"),

            new ApiScope("order.read", "Read Access in Order API"),
            new ApiScope("order.write", "Write Access in Order API"),

            new ApiScope("shared", "Shared Scope")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = "clientUserReadOnly",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "user.read" }
            },
            new Client
            {
                ClientId = "clientUserFullAccess",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "user.read", "user.write", "shared" }
            },
            new Client
            {
                ClientId = "clientOrderReadOnly",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "order.read" }
            },
            new Client
            {
                ClientId = "clientOrderFullAccess",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "order.read", "order.write", "shared" }
            }
            };

    }
}
