using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMessanger.Models;

namespace WebMessanger.Controllers
{
  public class HomeController : Controller
  {
    arturDBEntities baza = new arturDBEntities();
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(string Uzytkownik, string Haslo)
    {

      var tabela = baza.Uzytkownicies;
      var uzytkownik = (from u in tabela where u.Login == Uzytkownik select u).FirstOrDefault();
      if (uzytkownik == null || uzytkownik.Haslo != Haslo)
      {
        ViewBag.Blad = "Zly uzytkownik lub haslo";
        return View();
      }

      ViewBag.Uzytkownik = Uzytkownik;
      return View("Chat");
    }

    public ActionResult Chat()
    {
      return View();
    }
    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public ActionResult NewLogin()
    {
      return View();
    }

    [HttpPost]
    public ActionResult NewLogin(string Uzytkownik, string Haslo, string Haslo2)
    {
      bool blad = false;
      if (Haslo != Haslo2)
      {
        ViewBag.Blad = "Haslo musi sie zgadzac";
        blad = true;
      }
      var tabela = baza.Uzytkownicies;
      var uzytkownik = (from u in tabela where u.Login == Uzytkownik select u).FirstOrDefault();
      if (uzytkownik != null)
      {
        ViewBag.Blad = "Uzytkownik juz istnieje";
        blad = true;
      }
      if (blad == true)
      {
        return View();
      }
      ViewBag.Blad = "Uzytkownik stworzony";
      tabela.Add(new Uzytkownicy() {Haslo = Haslo, Login = Uzytkownik});
      baza.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}