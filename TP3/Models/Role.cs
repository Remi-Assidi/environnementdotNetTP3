using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3.Models
{
    public class Role
    {
        private long id;
        private string name;

        public long Id { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}