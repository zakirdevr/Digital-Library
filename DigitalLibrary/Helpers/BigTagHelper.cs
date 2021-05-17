using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Helpers
{
    [HtmlTargetElement("big")] //For only Tag
    [HtmlTargetElement(Attributes = "big")] //For only Attributes
    [HtmlTargetElement("big", Attributes = "big")] //For both Tag and Attributes
    public class BigTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            output.TagName = "h1";
            output.Attributes.RemoveAll("big");
        }
    }
}
