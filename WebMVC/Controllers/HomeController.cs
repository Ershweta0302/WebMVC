using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Text;
using WebMVC.Models;

namespace WebApiAppl.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();

        public HomeController()
        {
            client.BaseAddress = new Uri("https://localhost:44340/");
        }
        public ActionResult Index()
        {
            List<Employee> emplist = new List<Employee>();

            HttpResponseMessage listemp = client.GetAsync(client.BaseAddress + "Get/EmpDetails").Result;
            if (listemp.IsSuccessStatusCode)
            {
                string demo = listemp.Content.ReadAsStringAsync().Result;
                emplist = JsonConvert.DeserializeObject<List<Employee>>(demo);
            }
            return View(emplist);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            string demo = JsonConvert.SerializeObject(employee);

            StringContent content = new StringContent(demo, Encoding.UTF8, "application/json");

            HttpResponseMessage Listemp = client.PostAsync(client.BaseAddress + "Post/EmpAdd", content).Result;
            if (Listemp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            Employee employee = new Employee();

            HttpResponseMessage Listemp = client.GetAsync(client.BaseAddress + "Get/EmpEdit" + "?Id=" + Id.ToString()).Result;

            if (Listemp.IsSuccessStatusCode)
            {
                string demo = Listemp.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(demo);
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            string demo = JsonConvert.SerializeObject(employee);

            StringContent content = new StringContent(demo, Encoding.UTF8, "application/json");

            HttpResponseMessage Emplist = client.PutAsync(client.BaseAddress + "Post/EditEmp", content).Result;
            if (Emplist.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int Id)
        {
            Employee employee = new Employee();

            HttpResponseMessage Listemp = client.DeleteAsync(client.BaseAddress + "Get/EmpDelete" + "?Id=" + Id.ToString()).Result;
            if (Listemp.IsSuccessStatusCode)
            {
                //string demo = Listemp.Content.ReadAsStringAsync().Result;
                //employee = JsonConvert.DeserializeObject<Employee>(demo);
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            Employee employee = new Employee();

            HttpResponseMessage Listemp = client.GetAsync(client.BaseAddress + "Get/EmpEdit" + "?Id=" + Id.ToString()).Result;

            if (Listemp.IsSuccessStatusCode)
            {
                string demo = Listemp.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(demo);
            }
            return View(employee);
        }
    }
}