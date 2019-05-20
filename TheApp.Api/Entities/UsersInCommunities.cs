using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheApp.Api.Entities
{
    public class UsersInCommunities
    {
        public int CommunityId { get; set; }
        public int UserID { get; set; }

        public Community Community { get; set; }
        public DBUser User { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
