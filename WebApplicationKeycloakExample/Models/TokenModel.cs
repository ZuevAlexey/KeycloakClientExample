using IdentityModel.Client;

namespace WebApplicationKeycloakExample.Models {
    public class TokenModel {
        public TokenResponse Token { get; set; }
        public UserInfoResponse UserInfo { get; set; }
    }
}