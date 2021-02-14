using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3.Models
{
    public class UserVM
    {
        public User User { get; set; }
        public long CurrentRoleId { get; set; }
        public List<Role> Roles { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}