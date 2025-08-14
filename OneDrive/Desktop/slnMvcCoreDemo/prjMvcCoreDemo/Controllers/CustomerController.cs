using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult List(CKeywordViewModel p)
        {
            DbDemoContext db = new DbDemoContext();
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(p.txtKeyword))//偷雞摸狗法-----搜尋關鍵字
            {
                datas = from t in db.TCustomers
                        select t;
            }
            else
                datas = db.TCustomers.Where(t => t.FName.Contains(p.txtKeyword)
                || t.FPhone.Contains(p.txtKeyword)
                || t.FEmail.Contains(p.txtKeyword)
                || t.FAddress.Contains(p.txtKeyword));
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCustomer p)
        {
            DbDemoContext db = new DbDemoContext();           
            db.TCustomers.Add(p);                          
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                DbDemoContext db = new DbDemoContext();
                TCustomer d = db.TCustomers.FirstOrDefault(p => p.FId == id);
                if (d != null)
                {
                    db.TCustomers.Remove(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            DbDemoContext db = new DbDemoContext();
            TCustomer d = db.TCustomers.FirstOrDefault(p => p.FId == id);
            if (d == null)
                return RedirectToAction("List");
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(TCustomer uiCustomer)
        {
            DbDemoContext db = new DbDemoContext();
            TCustomer dbCustomer = db.TCustomers.FirstOrDefault(p => p.FId == uiCustomer.FId);
            if (dbCustomer == null)
                return RedirectToAction("List");

            dbCustomer.FName = uiCustomer.FName;
            dbCustomer.FPhone = uiCustomer.FPhone;
            dbCustomer.FEmail = uiCustomer.FEmail;
            dbCustomer.FAddress = uiCustomer.FAddress;
            dbCustomer.FPassword = uiCustomer.FPassword;
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
