using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATSystem.Models
{
    public class MovementAuthorityN
    {
        public int id { get; set; }
        public string date { get; set; }
        public string asset { get; set; }
        public string fromOrg { get; set; }
        public string frombranch { get; set; }
        public string toorg { get; set; }
        public string tobranch { get; set; }
        public string moveby { get; set; }
        public bool permision { get; set; }
    }
}