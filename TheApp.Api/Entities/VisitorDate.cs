using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Entities
{
    public class VisitorDate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DBUser User { get; set; }
        public string Visitor { get; set; }
        public DateTime Date { get; set; }
    }
}
