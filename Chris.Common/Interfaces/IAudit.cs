using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chris.Common.Interfaces
{
    public interface IAudit
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }
    }
}
