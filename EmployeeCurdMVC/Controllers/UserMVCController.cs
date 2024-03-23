using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using EmployeeCurdMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeCurdMVC.Controllers
{
    public class UserMVCController : Controller
    {
        // GET: UserMVC
        Uri baseAddress = new Uri("https://localhost:44343/api");
        private readonly HttpClient client;
        public UserMVCController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            List<EmpMVCModel> modelList = new List<EmpMVCModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
               modelList= JsonConvert.DeserializeObject<List<EmpMVCModel>>(data);
            }
            return View(modelList);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EmpMVCModel Empmodel)
        {
            string data = JsonConvert.SerializeObject(Empmodel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Employee", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            EmpMVCModel model = new EmpMVCModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employee/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<EmpMVCModel>(data);
            }
            return View("Edit",model);
        }
        [HttpPost]
        public ActionResult Edit(int id,EmpMVCModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Employee/" + id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Employee/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to delete employee.";
                return RedirectToAction("Index");
            }
        }
    }
}