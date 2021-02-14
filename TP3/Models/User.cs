using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TP3.Models
{

    public class User
    {
        private long id;
        private string firstname;
        private string lastname;
        private Role currentRole;
        private List<Book> books = new List<Book>();

        public long Id { get => this.id; set => this.id = value; }
        [DisplayName("Prénom")]
        public string Firstname { get => this.firstname; set => this.firstname = value; }
        [DisplayName("Nom")]
        public string Lastname { get => this.lastname; set => this.lastname = value; }
        public virtual Role CurrentRole { get => this.currentRole; set => this.currentRole = value; }
        public virtual List<Book> Books { get => this.books; private set => this.books = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}