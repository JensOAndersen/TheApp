using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public AppSettings Value { get; internal set; }
    }
}
