using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Libre.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; } = "Libre sp. z o.o.";
        public string SendGridKey { get; set; } = "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";
    }
}
