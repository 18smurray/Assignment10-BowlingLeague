using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    //Used to store multiple sets of data to be sent in a single package to the index view
    public class IndexViewModel
    {
        public List<Bowlers> Bowlers { get; set; }
        public PageNumberingInfo PageNumberingInfo { get; set; }

        //Use to account for selected team when generating dynamic html tags
        public string TeamName { get; set; }
    }
}
