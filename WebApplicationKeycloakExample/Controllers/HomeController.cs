using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationKeycloakExample.Models;
using IdentityModel.Client;
using WebApplicationKeycloakExample.Config;

namespace WebApplicationKeycloakExample.Controllers {
    public class HomeController: Controller {
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> KeycloakExample() {
            DiscoveryResponse disco;
            using(var client = new HttpClient()) {
                disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                    Address = Constants.WELL_KNOWN,
                    Policy = new DiscoveryPolicy {
                        RequireHttps = false
                    }
                });

                var redirectUrl = new RequestUrl(disco.AuthorizeEndpoint).CreateAuthorizeUrl(
                    Constants.CLIENT_ID,
                    "code",
                    Constants.SCOPES,
                    Constants.REDIRECT_URL
                    );

                return Redirect(redirectUrl);

//                var res = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest {
//                    ClientId = Constants.CLIENT_ID,
//                    Address = disco.AuthorizeEndpoint,
//                    RedirectUri = "http://localhost:5000/Home/KeycloakCallback",
//                    Code = "sdf"
//                });
            }
            
            

//            return Json(disco);

            //return View(disco);
        }

        public async Task<IActionResult> KeycloakCallback(string session_state, string code) {
            TokenResponse token;
            using(var client = new HttpClient()) {
                var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                    Address = Constants.WELL_KNOWN,
                    Policy = new DiscoveryPolicy {
                        RequireHttps = false
                    }
                });

                token = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest {
                    ClientId = Constants.CLIENT_ID,
                    Address = disco.TokenEndpoint,
                    RedirectUri = "http://localhost:5000/Home/KeycloakCallback",
                    Code = code,
                    ClientSecret = Constants.CLIENT_SECRET
                });

//                var intro = await client.IntrospectTokenAsync(new TokenIntrospectionRequest {
//                    Address = disco.IntrospectionEndpoint,
//                    ClientId = Constants.CLIENT_ID,
//                    ClientSecret = Constants.CLIENT_SECRET,
//                    Token = token.AccessToken
//
//
//                });
//
//                var userInfo = await client.GetUserInfoAsync(new UserInfoRequest {
//                    Token = token.AccessToken,
//                    Address = disco.UserInfoEndpoint
//                });

                //return Json(userInfo);
            }
            
//            var handler = new JwtSecurityTokenHandler();
//            var jwt = handler.ReadJwtToken(token.IdentityToken);

            return View(token);
            
            //return Json(jwt.Payload);
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
