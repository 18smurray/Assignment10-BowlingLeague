using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctxt)
        {
            _logger = logger;
            context = ctxt;
        }

        //Receives the teamnameid and teamname parameter through the url - set up in the view component asp-route-teamname attribute
        //Pass pagenum as parameter - default is 0; can ref through url using /?pageNum=3
        public IActionResult Index(long? teamnameid, string teamname, int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowlers = context.Bowlers
                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamnameid} OR {teamnameid} IS NULL")
                .Where(b => b.TeamId == teamnameid || teamnameid == null)
                .OrderBy(x => x.BowlerLastName)

                //Determine how many and which bowlers to display on each page
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)

                .ToList(),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize, //Set variable
                    CurrentPage = pageNum, //Passed in parameter

                    //If no specific team is selected, get the full count of all bowlers to 
                    //generate number of pages needed. Otherwise only count Bowlers from the team selected
                    TotalNumItems = (teamnameid == null ? context.Bowlers.Count()
                        : context.Bowlers.Where(b => b.TeamId == teamnameid).Count())
                },

                TeamName = teamname
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
