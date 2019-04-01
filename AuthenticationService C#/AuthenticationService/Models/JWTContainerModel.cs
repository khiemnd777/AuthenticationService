using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        #region Public Methods
        private int _expireMinutes = 10080; // expiring at 7 days

        public int ExpireMinutes
        {
            get { return _expireMinutes; }
            set { _expireMinutes = value; }
        }

        public Claim[] Claims { get; set; }
        #endregion
    }
}
