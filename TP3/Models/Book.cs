using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3.Models
{

    public class Book
    {
        private long id;
        private string name;
        private int nbPage;
        public decimal price;

        public long Id { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }
        public int NbPage { get => this.nbPage; set => this.nbPage = value; }
        public decimal Price { get => this.price; set => this.price = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}