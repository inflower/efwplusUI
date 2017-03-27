using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace efwplusWebApi.SSO
{
    public class TokenInfo
    {
        public Guid tokenId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ActivityTime { get; set; }
        //public string RemoteIp { get; set; }
        //public string UserId { get; set; }

        public bool IsValid { get; set; }
        public UserInfo userinfo { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public DateTime CreateDate { get; set; }
        public object Tag { get; set; }
    }
}
