using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class TeamNameViewComponent : ViewComponent
    {
        private BowlingLeagueContext context { get; set; }
        public TeamNameViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }
        public IViewComponentResult Invoke()
        {
            //Set team data in Viewbag for determining which link/button to highlight
            ViewBag.SelectedTeam = RouteData?.Values["teamname"];

            //Return distinct bowling team model objects 
            return View(context.Teams
                .Distinct()
                .OrderBy(t => t));
        }
    }
}
