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
    using System.ServiceModel;

    [ServiceContract]
    public interface IClaimsService
    {
        [OperationContract]
        string ComputeResponse(string input);
    }
}
