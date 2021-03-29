using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Infrastructure
{
    //Specify the kind of tag the tag helper is being built for
    //Attribute - expecting PageNumberingInfo type - can use this to draw necessary page info into the construction of tags
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        //Constructor to initialize urlInfo
        public PaginationTagHelper(IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }

        //Property to set as attribute - store paging information 
        //public bool setUpCorrectly { get; set; }
        public PageNumberingInfo PageInfo { get; set; }

        //Property to set as attribute that gets the selected team info
        //public string TeamName { get; set; }

        //Dictionary 
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();
        
        //Created for the IUrlHelper to use
        [HtmlAttributeNotBound] [ViewContext]
        public ViewContext ViewContext { get; set; }

        //Overridden process method - what to do when tag is used/referenced
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelp = urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= PageInfo.NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                //Create Dictionary entry
                KeyValuePairs["pageNum"] = i;

                individualTag.Attributes["href"] = urlHelp.Action("Index", KeyValuePairs);    //"/?pagenum=" + i;
                individualTag.InnerHtml.AppendHtml(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }

            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
