using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IOPTCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace IOPTCore.Controllers
{
    public class RESTfulController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Model(string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                ViewData["serialized"] = JsonConvert.SerializeObject(Snapshot.current);
            }
            else
            {
                if (route[route.Length - 1] == '/') route = route.Substring(0, route.Length - 1);
                string[] path = route.Split('/');
                //ViewData["path"] = path;
                switch (path.Length)
                {
                    case 1:
                        try
                        {
                            var model = (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First();
                            if (model != null)
                                ViewData["serialized"] = JsonConvert.SerializeObject(model);
                            else
                                return NotFound();
                        }
                        catch { return NotFound(); }
                        break;
                    case 2:
                        try
                        {
                            var obj = (from o in (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First().objects where o.pathUnit == path[1] select o).First();
                            if (obj != null)
                                ViewData["serialized"] = JsonConvert.SerializeObject(obj);
                            else
                                return NotFound();
                        }
                        catch { return NotFound(); }
                        break;
                    case 3:
                        try
                        {
                            var prop = (from p in (from o in (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First().objects where o.pathUnit == path[1] select o).First().properties where p.pathUnit == path[2] select p).First();
                            if (prop != null)
                                ViewData["serialized"] = JsonConvert.SerializeObject(prop);//, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }
                            else
                                return NotFound();
                        }
                        catch { return NotFound(); }
                        break;
                    case 4:
                        try
                        {
                            var script = (from s in (from p in (from o in (from m in Snapshot.current.models where m.pathUnit == path[0] select m).First().objects where o.pathUnit == path[1] select o).First().properties where p.pathUnit == path[2] select p).First().scripts where s.pathUnit==path[3] select s).First();
                            if (script != null)
                                ViewData["serialized"] = JsonConvert.SerializeObject(script);//, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }
                            else
                                return NotFound();
                        }
                        catch { return NotFound(); }
                        break;
                    case 0:
                    default:
                        return NotFound();
                }
                //if (path.Length == 0) { ViewData["serialized"] = "Not Found"; return View(); }
                //Model model = null;
                //foreach (var m in Snapshot.Current.Models)
                //{
                //    if (m.Id == path[0]) model = m;
                //}
                //if (path.Length == 1) { ViewData["serialized"] = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }); return View(); }
                //Models.Object obj = null;
                //foreach (var o in model.Objects)
                //{
                //    if (o.Id == path[1]) obj = o;
                //}
                //if (path.Length == 2) { ViewData["serialized"] = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }); return View(); }
                //Property prop = null;
                //foreach (var p in obj.Properties)
                //{
                //    if (p.Id == path[2]) prop = p;
                //}
                //if (path.Length == 3) { ViewData["serialized"] = JsonConvert.SerializeObject(prop, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }); return View(); }

                //ViewData["serialized"] = slugs;
            }
            return View();
        }

        [HttpPut("Model/")]
        [Authorize]
        public IActionResult Model( [FromBody]Snapshot json)
        {
            //if (string.IsNullOrEmpty(slugs)||string.IsNullOrWhiteSpace(slugs.Replace('/',' ')))
            //{
                try
                {
                    if (Snapshot.current.lastUpdate > json.lastUpdate) return BadRequest();
                    Snapshot.current = json;
                    //ViewData["serialized"] = "<br>" + sh.LastUpdate + "<br>" + sh.Models;
                    return Ok();
                }
                catch { return NotFound(); }
            //}
            //return BadRequest();
        }
        [HttpPatch("Model/{MName}/{OName}/{PName}/")]
        [Authorize]
        public IActionResult Model(string MName, string OName, string PName, [FromBody]Property json)
        {
            if (!string.IsNullOrEmpty(PName) || !string.IsNullOrWhiteSpace(PName.Replace('/', ' ')))
            {
                try
                {
                    var prop = (from p in (from o in (from m in Snapshot.current.models where m.pathUnit == MName select m).First().objects where o.pathUnit == OName select o).First().properties where p.pathUnit == PName select p).First();
                    if (!prop.GenerateId(prop.pathUnit).Equals(json.pathUnit)) { return BadRequest(); }
                    prop.value = json.value;
                    if (!prop.value.Equals(json.value)) return NoContent();
                    return Ok();
                }
                catch { return NotFound(); }
            }
            return BadRequest();
        }
    }
}
