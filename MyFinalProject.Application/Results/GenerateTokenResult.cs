using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Results
{
    public record GenerateTokenResult
    (
        string AccessToken,
        double ExpiresInSeconds
    );
}
