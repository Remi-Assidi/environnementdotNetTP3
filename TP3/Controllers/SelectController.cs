using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP3.Data;
using TP3.Models;

namespace TP3.Views
{
    public class SelectController : Controller
    {
        private MyContext db = new MyContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Select(long id)
        {
            User user = db.Users.Find(id);
            Session["userId"] = user.Id;
            Session["roleId"] = user.CurrentRole.Id;
            if (user.CurrentRole.Name.ToLower() == "seller")
            {
                return RedirectToAction("ListBookSeller", "Select");
            }
            return RedirectToAction("SearchCustomer", "Select");
        }

        public ActionResult ListBookSeller()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            User user = db.Users.Find(Session["userId"]);
            return View(user.Books.ToList());
        }

        public ActionResult SearchCustomer()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            User user = db.Users.Find(Session["userId"]);
            if (user.CurrentRole.Name.ToLower() != "customer")
            {
                return RedirectToAction("Index", "Select");
            }
            return RedirectToAction("Search", "Select");
        }

        public ActionResult AddBookSeller(string Name, int NbPage, int Price)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            User user = db.Users.Find(Session["userId"]);
            if (user.CurrentRole.Name.ToLower() != "seller")
            {
                return RedirectToAction("Index", "Select");
            }
            db.SaveChanges();
            Book book = new Book();
            book.Name = Name;
            book.NbPage = NbPage;
            book.Price = Price;
            user.Books.Add(book);
            db.Books.Add(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListBookSeller", "Select");
        }

        public ActionResult Delete(long? id)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(Session["userId"]);
            foreach (Book book in user.Books)
            {
                if (book.Id == id)
                {
                    user.Books.Remove(book);
                    break;
                }
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListBookSeller", "Select");
        }

        public ActionResult Search()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            User userData = db.Users.Find(Session["userId"]);
            if (userData.CurrentRole.Name.ToLower() != "customer")
            {
                return RedirectToAction("Index", "Select");
            }
            var users = db.Users.Where(x => x.CurrentRole.Name == "Seller");
            List<BookUserVM> booksUsersVm = new List<BookUserVM>();
            foreach (User user in users)
            {
                foreach (Book book in user.Books)
                {
                    BookUserVM bookUserVM = new BookUserVM();
                    bookUserVM.idBook = book.Id;
                    bookUserVM.idUser = user.Id;
                    bookUserVM.lastname = user.Lastname;
                    bookUserVM.firstname = user.Firstname;
                    bookUserVM.name = book.Name;
                    bookUserVM.nbPage = book.NbPage;
                    bookUserVM.price = book.Price;
                    booksUsersVm.Add(bookUserVM);
                }
            }

            return View(booksUsersVm.ToList());
        }

        public ActionResult SearchWithParam()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Index", "Select");
            }
            User userData = db.Users.Find(Session["userId"]);
            if (userData.CurrentRole.Name.ToLower() != "customer")
            {
                return RedirectToAction("Index", "Select");
            }
            var users = db.Users.Where(x => x.CurrentRole.Name == "Seller");
            List<BookUserVM> booksUsersVm = new List<BookUserVM>();
            string name = Request.QueryString["name"];
            foreach (User user in users)
            {
                foreach (Book book in user.Books)
                {
                    if (string.IsNullOrEmpty(name)){
                        BookUserVM bookUserVM = new BookUserVM();
                        bookUserVM.idBook = book.Id;
                        bookUserVM.idUser = user.Id;
                        bookUserVM.lastname = user.Lastname;
                        bookUserVM.firstname = user.Firstname;
                        bookUserVM.name = book.Name;
                        bookUserVM.nbPage = book.NbPage;
                        bookUserVM.price = book.Price;
                        booksUsersVm.Add(bookUserVM);
                    } else if (book.Name.Contains(name))
                    {
                        BookUserVM bookUserVM = new BookUserVM();
                        bookUserVM.idBook = book.Id;
                        bookUserVM.idUser = user.Id;
                        bookUserVM.lastname = user.Lastname;
                        bookUserVM.firstname = user.Firstname;
                        bookUserVM.name = book.Name;
                        bookUserVM.nbPage = book.NbPage;
                        bookUserVM.price = book.Price;
                        booksUsersVm.Add(bookUserVM);
                    }
                }
            }
            return View("Search", booksUsersVm.ToList());
        }

        public ActionResult Buy()
        {
            string idBook = Request.QueryString["idBook"];
            User customer = db.Users.Find(Session["userId"]);
            Book book = db.Books.Find(Int64.Parse(idBook));
            customer.Books.Add(book);
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Search", "Select");
        }
    }
}