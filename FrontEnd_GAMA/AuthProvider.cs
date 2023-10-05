using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd_GAMA
{
    public class AuthProvider : AuthenticationStateProvider
    {

        private ILocalStorageService _localStorageService;
        private AuthenticationState authenticationState;

        public AuthProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (authenticationState is null)
            {
                var identity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }
            else return Task.FromResult(authenticationState);
        } 
        
        public async Task<AuthenticationState> AuthorizeUser ()
        {
            var token = await _localStorageService.GetItemAsStringAsync("token");
            token = token.Trim('"');
            
          
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                var claims = new JwtSecurityTokenHandler().ReadJwtToken(token);
                foreach (var claim in claims.Claims)
                {
                    identity.AddClaim(claim);
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            authenticationState = state;
            return state;
            
        }
    }
}
