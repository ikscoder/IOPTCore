using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IOPTCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace IOPTCore.Controllers
{
    public class ModelEditController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View(Snapshot.current.models);
        }
        [Authorize]
        public IActionResult GetView(string slugs)
        {
            //if(string.IsNullOrEmpty(slugs)) return StatusCode(404);
            //if (slugs[slugs.Length - 1] == '/') slugs = slugs.Substring(0, slugs.Length - 1);
            //string[] path = slugs.Split('/');
            //ViewData["path"] = slugs;
            //switch (path.Length)
            //{
            //    case 1:
            //        try
            //        {
            //            var model = (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First();
            //            if (model != null)
            //            {
            //                ViewData["type"] = "model";
            //                ViewBag.Fields = new List<string> { model.pathUnit, model.name };
            //            }
            //            else
            //                return StatusCode(404);
            //        }
            //        catch { return StatusCode(404); }
            //        break;
            //    case 2:
            //        try
            //        {
            //            var obj = (from o in (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First().Objects where o.pathUnit == path[1] select o).First();
            //            if (obj != null)
            //            {
            //                ViewData["type"] = "object";
            //                ViewBag.Fields = new List<string> { obj.Id,obj.Name };
            //            }
            //            else
            //                return StatusCode(404);
            //        }
            //        catch { return StatusCode(404); }
            //        break;
            //    case 3:
            //        try
            //        {
            //            var prop = (from p in (from o in (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First().objects where o.pathUnit == path[1] select o).First().properties where p.pathUnit == path[2] select p).First();
            //            if (prop != null)
            //            {
            //                ViewData["type"] = "property";
            //                ViewBag.Fields = new List<string> { prop.pathUnit, prop.name,prop.type.ToString(),prop.Value };
            //            }
            //            else
            //                return StatusCode(404);
            //        }
            //        catch { return StatusCode(404); }
            //        break;
            //    case 4:
            //        try
            //        {
            //            var script = (from s in (from p in (from o in (from m in Snapshot.Current.Models where m.Id == path[0] select m).First().Objects where o.Id == path[1] select o).First().Properties where p.Id == path[2] select p).First().Scripts select s).First();
            //            if (script != null)
            //            {
            //                ViewData["type"] = "script";
            //                ViewBag.Fields = new List<string> {script.Id,script.Name,script.Value };
            //            }
            //            else
            //                return StatusCode(404);
            //        }
            //        catch { return StatusCode(404); }
            //        break;
            //    case 0:
            //    default:
            //        return StatusCode(404);
            //}
            return View(Snapshot.current.models);
        }
    }
}
