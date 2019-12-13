
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using System.Net.Http;
namespace mvc.Controllers
{
  public class CarsController : Controller
  {
// GET: Cars
    public ActionResult Index()
    {
        IEnumerable<Cars> cars = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:57265/api/");
            //HTTP GET
            var responseTask = client.GetAsync("cars");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Cars>>();
                readTask.Wait();

                cars = readTask.Result;
            }
            else //web api sent error response
            {
                //log response status here..

                cars = Enumerable.Empty<Cars>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
        }
        return View(cars);
    }
    public ActionResult create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult create(Cars car)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:57265/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Cars>("cars", car);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
        }

        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        return View(car);

    }
    //single out the car
     public ActionResult Edit(int id)
    {
        //Cars car = null;

        IList<Cars> car = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:57265/api/");
            //HTTP GET
            var responseTask = client.GetAsync("cars?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Cars>>();
                readTask.Wait();

                car = readTask.Result;
            }
        }

        return View(car);
    }
       [HttpPost]//updater car
    public ActionResult Edit(Cars car)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:57265/api/");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Cars>("cars", car);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
        }
        return View(car);
    }
    //delete


    public ActionResult Delete(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:64189/api/");

            //HTTP DELETE
            var deleteTask = client.DeleteAsync("cars/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
        }

        return RedirectToAction("Index");
    }


  }
}