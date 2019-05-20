using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Entities
{
    public class Community
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public DateTime Established { get; set; }
        public string Description { get; set; }
    }
}
