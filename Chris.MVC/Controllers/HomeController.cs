using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chris.MVC.Controllers
{
    using System.IdentityModel.Services;
    using System.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Threading;

    using Chris.Common.Helpers;
    using Chris.Membership;
    using Chris.Membership.EF;
    using Chris.Membership.Interfaces;

    public class HomeController : Controller
    {
        private MembershipContext context;
        private MembershipUnitOfWork worker;
        private MembershipService service;

        public HomeController()
        {
            context = new MembershipContext("Claims");
            worker = new MembershipUnitOfWork(context);
            service = new MembershipService(worker);
        }

        public ActionResult Index()
        {
            var user = service.CreateUser("christopher", "test");

            worker.Commit();

            var claims = GetBasicClaims(user);

            var id = new ClaimsIdentity(claims, AuthenticationTypes.Password);
            var cp = new ClaimsPrincipal(id);

            // claims transform
            cp = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(string.Empty, cp);

            // issue cookie
            IssueToken(cp);

            return View(user);
        }

        public ActionResult Do()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Authorized()
        {
            ViewBag.Message = "Authorized";
            return View();
        }

        private static IEnumerable<Claim> GetBasicClaims(UserAccount account)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.AuthenticationInstant, DateTime.Now.ToString("s")),
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Email)
                };

            if(account.Roles.Any())
                claims.AddRange(from r in account.Roles select new Claim(ClaimTypes.Role, r.Name));

            return claims;
        }

        protected void IssueToken(ClaimsPrincipal principal, TimeSpan? tokenLifetime = null, bool? persistentCookie = null)
        {
            if (principal == null) throw new ArgumentNullException("principal");

            if (tokenLifetime == null)
            {
                var handler = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers[typeof(SessionSecurityToken)] as SessionSecurityTokenHandler;
                if (handler == null)
                    throw new Exception("SessionSecurityTokenHandler is not configured and it needs to be.");

                tokenLifetime = handler.TokenLifetime;
            }

            if (persistentCookie == null)
            {
                persistentCookie = FederatedAuthentication.FederationConfiguration.WsFederationConfiguration.PersistentCookiesOnPassiveRedirects;
            }

            var sam = FederatedAuthentication.SessionAuthenticationModule;
            if (sam == null)
                throw new Exception("SessionAuthenticationModule is not configured and it needs to be.");

            var token = new SessionSecurityToken(principal, tokenLifetime.Value);
            token.IsPersistent = persistentCookie.Value;
            token.IsReferenceMode = sam.IsReferenceMode;

            sam.WriteSessionTokenToCookie(token);
        }
    }
}
