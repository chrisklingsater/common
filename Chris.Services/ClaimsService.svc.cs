using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Chris.Services
{
    using System.Security.Claims;

    public class ClaimsService : IClaimsService
    {
        #region Implementation of IClaimsService

        public string ComputeResponse(string input)
        {
            // Get the caller's identity from ClaimsPrincipal.Current
            var claimsPrincipal = OperationContext.Current.ClaimsPrincipal;

            // Start generating the output
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Computed by ClaimsAwareWebService");
            builder.AppendLine("Input received from client:" + input);

            if (claimsPrincipal != null)
            {
                // Display the claims from the caller. Do not use this code in a production application.
                var identity = claimsPrincipal.Identity as ClaimsIdentity;
                builder.AppendLine("Client Name:" + identity.Name);
                builder.AppendLine("IsAuthenticated:" + identity.IsAuthenticated);
                builder.AppendLine("The service received the following issued claims of the client:");

                // Iterate over the caller’s claims and append to the output
                foreach (Claim claim in claimsPrincipal.Claims)
                {
                    builder.AppendLine("ClaimType :" + claim.Type + "   ClaimValue:" + claim.Value);
                }
            }

            return builder.ToString();
        }

        #endregion
    }
}
