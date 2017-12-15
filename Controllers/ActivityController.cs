using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using CBeltExam.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CBeltExam.Controllers
{
    public class ActivityController : Controller
    {
        private ActivityContext _context;
        public ActivityController(ActivityContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            ViewBag.activities = _context.Activities.Include(r=>r.RSVPs).ThenInclude(u=>u.User).OrderBy(x=>x.Date);
            ViewBag.curUser = _context.Users.Where(a=>a.id == HttpContext.Session.GetInt32("id")).Include(r=>r.RSVPs).ThenInclude(a=>a.Activity).First();
            return View();
        }
        
        [HttpGet]
        [Route("dashboard/new")]
        public IActionResult New(){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            ViewBag.error ="";
            return View();
        }

        [HttpGet]
        [Route("dashboard/show/{activityId}")]
        public IActionResult Show(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            ViewBag.curUser = _context.Users.SingleOrDefault(a=>a.id == HttpContext.Session.GetInt32("id"));
            ViewBag.activity = _context.Activities.Where(a=>a.id==activityId).Include(r=>r.RSVPs).ThenInclude(u=>u.User).First();
            return View();
        }

        [HttpGet]
        [Route("dashboard/rsvp/{activityId}")]
        public IActionResult rsvp(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            _context.RSVPs.Add(new RSVP{
                UserId = (int)HttpContext.Session.GetInt32("id"),
                User = _context.Users.SingleOrDefault(a=>a.id == HttpContext.Session.GetInt32("id")),
                ActivityId = _context.Activities.SingleOrDefault(a=>a.id==activityId).id,
                Activity = _context.Activities.SingleOrDefault(a=>a.id==activityId)
            });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dashboard/unrsvp/{activityId}")]
        public IActionResult unrsvp(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            _context.RSVPs.Remove(_context.RSVPs.SingleOrDefault(a=>a.UserId == (int)HttpContext.Session.GetInt32("id") && a.ActivityId == activityId));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dashboard/delete/{activityId}")]
        public IActionResult delete(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            if(HttpContext.Session.GetInt32("id")!=_context.Activities.SingleOrDefault(a=>a.id==activityId).creatorId){
                return RedirectToAction("Index");
            }
            _context.Activities.Remove(_context.Activities.SingleOrDefault(a=>a.id==activityId));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dashboard/rsvp2/{activityId}")]
        public IActionResult rsvp2(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            _context.RSVPs.Add(new RSVP{
                UserId = (int)HttpContext.Session.GetInt32("id"),
                User = _context.Users.SingleOrDefault(a=>a.id == HttpContext.Session.GetInt32("id")),
                ActivityId = _context.Activities.SingleOrDefault(a=>a.id==activityId).id,
                Activity = _context.Activities.SingleOrDefault(a=>a.id==activityId)
            });
            _context.SaveChanges();
            return RedirectToAction("Show", new {activityId=activityId});
        }

        [HttpGet]
        [Route("dashboard/unrsvp2/{activityId}")]
        public IActionResult unrsvp2(int activityId){
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
            _context.RSVPs.Remove(_context.RSVPs.SingleOrDefault(a=>a.UserId == (int)HttpContext.Session.GetInt32("id") && a.ActivityId == activityId));
            _context.SaveChanges();
            return RedirectToAction("Show", new {activityId=activityId});
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Create(Activity activity)
        {
            System.Console.WriteLine(activity.Date);
            System.Console.WriteLine(activity.Hour);
            System.Console.WriteLine(activity.Time);
            System.Console.WriteLine(activity.AmPm);
            if(HttpContext.Session.GetInt32("id")==null){//verifies valid login
                return RedirectToAction("Index", "Home");
            }
        if(ModelState.IsValid){
                _context.Activities.Add(new ModelActivity{
                    Title = activity.Title,
                    Date = DateTime.ParseExact($"{activity.Date} {activity.Hour}:{activity.Time} {activity.AmPm}", "yyyy-MM-dd hh:mm tt", null),
                    Duration = activity.Duration,
                    Coordinator = _context.Users.SingleOrDefault(a=>a.id == HttpContext.Session.GetInt32("id")).FirstName,
                    DurType = activity.DurType,
                    Description = activity.Description,
                    creatorId = (int)HttpContext.Session.GetInt32("id"),
                    RSVPs = new List<RSVP>()
                });
                _context.SaveChanges();
                _context.RSVPs.Add(new RSVP{
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    User = _context.Users.SingleOrDefault(a=>a.id == HttpContext.Session.GetInt32("id")),
                    ActivityId = _context.Activities.Last().id,
                    Activity = _context.Activities.Last()
                });
                _context.SaveChanges();
                return RedirectToAction("Show" ,new {activityId=_context.Activities.Last().id});
            }
            ViewBag.error="invalid inputs";
            return View("New");
        }
    }
}
