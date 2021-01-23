using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libre.Infrastructure
{
    using System.Collections.Generic;
    using Libre.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    namespace Joobie.Infrastructure
    {
        [HtmlTargetElement("ul", Attributes = "page-model")]
        public class PageLinkTagHelper : TagHelper
        {
            private readonly IUrlHelperFactory urlHelperFactory;
            public PageLinkTagHelper(IUrlHelperFactory helperFactory)
            {
                urlHelperFactory = helperFactory;
            }

            [ViewContext]
            [HtmlAttributeNotBound]
            public ViewContext ViewContext { get; set; }

            public PagingInfo PageModel { get; set; }
            public string PageAction { get; set; }

            [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
            public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

            public bool PageClassesEnabled { get; set; } = false;
            public string PageClass { get; set; }
            public string PageClassNormal { get; set; }
            public string PageClassSelected { get; set; }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    if (i == 1)
                    {


                        TagBuilder prevPage = new TagBuilder("li");
                        prevPage.AddCssClass(PageClass + " pagginationButton");

                        TagBuilder aPrev = new TagBuilder("a");
                        aPrev.AddCssClass("page-link");
                        aPrev.InnerHtml.Append("<");



                        PageUrlValues["page"] = PageModel.CurrentPage - 1;
                        if (PageModel.CurrentPage < 2)
                            prevPage.AddCssClass(PageClassNormal + " disabled");
                        else
                        {
                            aPrev.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                            prevPage.AddCssClass(PageClassNormal);
                        }

                        prevPage.InnerHtml.AppendHtml(aPrev);
                        result.InnerHtml.AppendHtml(prevPage);
                    }

                    TagBuilder tag = new TagBuilder("li");
                    PageUrlValues["page"] = i;

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass + " pagginationButton");
                        tag.AddCssClass(i == PageModel.CurrentPage
                            ? PageClassSelected : PageClassNormal);

                    }

                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("page-link");
                    a.InnerHtml.Append(i.ToString());
                    a.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    tag.InnerHtml.AppendHtml(a);

                    result.InnerHtml.AppendHtml(tag);
                }

                TagBuilder nextPage = new TagBuilder("li");
                nextPage.AddCssClass(PageClass + " pagginationButton");

                TagBuilder aNext = new TagBuilder("a");
                aNext.AddCssClass("page-link");
                aNext.InnerHtml.Append(">");



                PageUrlValues["page"] = PageModel.CurrentPage + 1;
                if (PageModel.CurrentPage == PageModel.TotalPages)
                    nextPage.AddCssClass(PageClassNormal + " disabled");
                else
                {
                    aNext.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    nextPage.AddCssClass(PageClassNormal);
                }
                nextPage.InnerHtml.AppendHtml(aNext);
                result.InnerHtml.AppendHtml(nextPage);
                result.AddCssClass("paggination");
                output.Content.AppendHtml(result.InnerHtml);

            }

        }
    }
}
