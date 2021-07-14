using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartyInvites.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            if(hour < 12)
            {
                ViewBag.Greeting = "Good Morning";
            }
            else if(hour > 17)
            {
                ViewBag.Greeting = "Good Evening";
            }
            else
            {
                ViewBag.Greeting = "Good Afternoon";
            }
            //ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View("MyView");
        }

        [HttpGet]
        public ViewResult RsvpForm()   //Presents an empty rsvp form
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            //ToDo: store guest response
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            else
            {
                //there is a validation error
                return View();
            }
        }

        public ViewResult ListResponses()    //Returns the guest list
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true)
                .OrderBy(r => r.FirstName));
        }
    }
}
