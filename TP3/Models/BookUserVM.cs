using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3.Models
{
    public class BookUserVM
    {
        public long idUser { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public long idBook { get; set; }
        public string name { get; set; }
        public int nbPage { get; set; }
        public decimal price { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}