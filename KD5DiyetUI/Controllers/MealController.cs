using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Kd5DiyetModel;
using System.Net.Http.Json;

namespace KD5DiyetUI.Controllers
{
    public class MealController : Controller
    {
        // GET: MealController
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8174/"); //Api'ın local adresini // Server'da çalışıyor olsaydık server adresini girecektik.
                var responseTask = client.GetAsync("api/OgunAsync");//bilgileri getirecek metot
                responseTask.Wait(); //Bilgilerin gelemesi için bekle dedik.
                var ResultTask = responseTask.Result; //Bize sonuçları döndür dedik.

                //Bu işlem doğru döndümü onu kontrol ediyoruz
                if (responseTask.IsCompletedSuccessfully)
                {
                    var readTask = ResultTask.Content.ReadAsAsync<IEnumerable<Ogun>>(); // Sen git o listeyi oku ve bana döndür
                    readTask.Wait();
                    return View(readTask.Result);//Bu listeyi view olarak döndürmüş olduk.
                }
                else
                {
                    ViewBag.EmptyListMessage = "Öğün Bulunamamıştır!!";
                    return View(new List<Ogun>());
                }
            }
        }

        // GET: MealController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MealController/Create
        public ActionResult Create()
        {
            return View(new Ogun());
        }

        // POST: MealController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ogun ogun)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8174/"); //Api'ın local adresini // Server'da çalışıyor olsaydık server adresini girecektik.
                var responseTask = client.PostAsJsonAsync<Ogun>("api/OgunAsync", ogun);//bilgileri getirecek metot
                responseTask.Wait(); //Bilgilerin gelemesi için bekle dedik.
                var ResultTask = responseTask.Result; //Bize sonuçları döndür dedik.
                if (responseTask.IsCompletedSuccessfully)//İçerik doğru okundu ise index'i döndür dedik.
                {
                    //var readTask = ResultTask.Content.ReadAsAsync<Ogun>(); // Sen git o ogunu bize getir.
                    //readTask.Wait();
                    return RedirectToAction("Index");//Bu listeyi view olarak döndürmüş olduk.
                }
                else
                {
                    return NotFound(); // Eğer okuma işlemi gerçekleşmez ise hata mesajı döndüre biliriz.Biz burada boş ekran döndürdük pek öantıklı değil ama olsun
                }
            }
        }
        private string TarihÇevir(DateTime d)
        {
            return d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString();
        }
        // GET: MealController/Delete/5
        public ActionResult Delete(DateTime Day, OgunListesi Hour)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8174/"); //Api'ın local adresini // Server'da çalışıyor olsaydık server adresini girecektik.
                var responseTask = client.GetAsync("api/OgunAsync/" + TarihÇevir(Day) + "/" + ((int)Hour).ToString());//bilgileri getirecek metot
                responseTask.Wait(); //Bilgilerin gelemesi için bekle dedik.
                var ResultTask = responseTask.Result; //Bize sonuçları döndür dedik.

                //Bu işlem doğru döndümü onu kontrol ediyoruz
                if (responseTask.IsCompletedSuccessfully)
                {
                    var readTask = ResultTask.Content.ReadAsAsync<Ogun>(); // Sen git o listeyi oku ve bana döndür
                    readTask.Wait();
                    return View(readTask.Result);//Bu listeyi view olarak döndürmüş olduk.
                }
                else
                {
                    ViewBag.EmptyListMessage = "Öğün Bulunamamıştır!!";
                    return NotFound();
                }
            }
        }

        // POST: MealController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecord(DateTime Day, OgunListesi Hour)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8174/"); //Api'ın local adresini // Server'da çalışıyor olsaydık server adresini girecektik.
                var responseTask = client.DeleteAsync("api/OgunAsync/" + TarihÇevir(Day) + "/" + ((int)Hour).ToString());//bilgileri getirecek metot
                responseTask.Wait(); //Bilgilerin gelemesi için bekle dedik.
                var ResultTask = responseTask.Result; //Bize sonuçları döndür dedik.

                //Bu işlem doğru döndümü onu kontrol ediyoruz
                if (responseTask.IsCompletedSuccessfully)
                {
                    //var readTask = ResultTask.Content.ReadAsAsync<IEnumerable<Ogun>>(); // Sen git o listeyi oku ve bana döndür
                    //readTask.Wait();
                    return RedirectToAction("Index");//Bu listeyi view olarak döndürmüş olduk.
                }
                else
                {
                    ViewBag.EmptyListMessage = "Öğün Bulunamamıştır!!";
                    return NotFound();
                }
            }
        }

    }
}

