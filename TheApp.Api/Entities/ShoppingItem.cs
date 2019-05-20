using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Entities
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DBUser User { get; set; }
        public DateTime DateofEntry { get; set; }
        public string Item { get; set; }
        public bool IsBought { get; set; }
    }
}
