using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        IWebHostEnvironment _enviro;           //注入=把IWebHostEnvironment變全域變數
        public AController(IWebHostEnvironment p)//宣告參數
        {
            _enviro = p;
        }
        public string demoJson2Obj()
        {
            string json = demoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);//DeSerialize去序列化:字串變回物件
            return "姓名：" + x.FName;
        }

        public string demoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Marco",
                FPhone = "0966541254",
                FEmail = "marco@gmail.com",
                FAddress = "Taipei",
                FPassword = "123"
            };

            return JsonSerializer.Serialize(x);//Serialize序列化:把物件變字串
        }

        public ActionResult showCountBySession()//sessiona存在記憶體,彼此互不影響
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("COUNT"))
                count = (int)HttpContext.Session.GetInt32("COUNT");
            count++;
            HttpContext.Session.SetInt32("COUNT", count);
            ViewBag.COUNT = count;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult displayById(int? id)
        {
            if (id != null)
            {
                             
            }
            return View();
        }
      
        public IActionResult demoUpload(IFormFile photo)
        {
            if (photo != null)
            {
                string path = _enviro.WebRootPath + "/images/" + photo.FileName;
                photo.CopyTo(new FileStream(path, FileMode.Create));
            }
            return View();
        }
    }
}
