/*using System.Web;using System.Web.Services;
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
        Cars car = null;
        using (var client = new HttpClient())
        {
      //  System.Diagnostics.Debug.WriteLine("res =  " + result);

             var result = GetResponseString(client,id);

            if (result!=null)
            {
                   System.Diagnostics.Debug.WriteLine("res =  " + result);
                               // car=result;
                                car = JsonConvert.DesirializeObject(result);

            }
        }

       return View(car);
    }
[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
public string GetResponseString(HttpClient client,int id) {
     client.BaseAddress = new Uri("http://localhost:57265/api/");

   // var request = "http://localhost:57265/api/";
    var response = client.GetAsync("cars?id=" + id.ToString()).Result;
    var content = response.Content.ReadAsStringAsync().Result;
    return content;
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


        /*[HttpPost]
        public IActionResult Index(Cars model){
            if(ModelState.IsValid){
                //to do
            }
            return View();
        }*/


    /*
    // Declares the DbContext class
    private CarsContext dataContext;
    // The instance of DbContext is passed via dependency injection
    public CarsController(CarsContext context)
    {
      this.dataContext=context;
    }
    // GET: /<controller>/
    // Return the list of cars to the caller view
    public IActionResult Index()
    {
      return View(this.dataContext.Cars.ToList());
    }
    public IActionResult Create()
    {
      return View();
    }
    // Add a new object via a POST request
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cars car)
    {
      // If the data model is in a valid state ...
      if (ModelState.IsValid)
      {
        // ... add the new object to the collection
        dataContext.Cars.Add(car);
        // Save changes and return to the Index method
        dataContext.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(car);
    }
    [ActionName("Delete")]
    public IActionResult Delete(int? id)
    {
      if (id == null)
      {
        return HttpNotFound();
      }
      Cars car = dataContext.Cars.Single(m => m.id == id);
      if (car == null)
      {
        return HttpNotFound();
      }
      return View(car);
    }
    // POST: Cars/Delete/5
    // Delete an object via a POST request
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      Cars car = dataContext.Cars.SingleOrDefault(m => m.id == id);
      // Remove the car from the collection and save changes
      dataContext.Cars.Remove(car);
      dataContext.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}

HttpClient client = new HttpClient();
public async Task<string> GetResponseString() {
    var request = "http://localhost:51843/api/values/getMessage?id=1";
    var response = await client.GetAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    return content;
}

HttpClient client = new HttpClient();
public string GetResponseString() {
    var request = "http://localhost:51843/api/values/getMessage?id=1";
    var response = client.GetAsync(request).Result;
    var content = response.Content.ReadAsStringAsync().Result;
    return content;
}*/