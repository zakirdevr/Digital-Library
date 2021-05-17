using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        public string MyLink { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{MyLink}");
            output.Attributes.Add("id", "my-id");
            output.Attributes.SetAttribute("target", "_blank");
            output.Content.SetContent("Planet Of Zakir");
        }
    }
}
