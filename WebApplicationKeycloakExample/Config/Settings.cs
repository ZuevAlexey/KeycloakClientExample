using System;
using Microsoft.IdentityModel.Protocols;

namespace WebApplicationKeycloakExample.Config {
    public static class Constants {
        public const string AUTHORITY = "http://localhost:8080/auth/realms/master";
        public const string WELL_KNOWN = AUTHORITY + "/.well-known/openid-configuration";
        public const string CLIENT_ID = "testWebApp";
        public const string SCOPES = "openid profile roles";
        public const string REDIRECT_URL = "http://localhost:5000/Home/KeycloakCallback";
        public const string CLIENT_SECRET = "5864ddc5-3900-4f51-b289-f88e2a9537a0";
    }
}
