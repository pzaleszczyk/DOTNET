﻿
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace pzaleszczyk.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "sigma.ug.edu.pl";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";                                 // Replaces <email> with <a> tag
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Content.SetContent(content.GetContent());
        }
    }
}