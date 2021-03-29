using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Holds all information related to configuring the pages
//Puts all properties in a convenient package for passing around

namespace Assignment10.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        //Calculate the total number of pages needed 
        public int NumPages =>
            (int)(Math.Ceiling((decimal)TotalNumItems / NumItemsPerPage));
    }
}
