using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class UserLike
    {
        public Appuser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public Appuser LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}