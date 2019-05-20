using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Entities
{
    public class CleaningDate
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DBUser User { get; set; }

        public DateTime Date { get; set; }
    }
}
