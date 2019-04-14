using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace webapp.Helpers.Html
{
    public static class DisplayStringHelper
    {
        public static MvcHtmlString DisplayRowFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, int labelWidth, int valueWidth)
        {
            var html = "<div class='row col-md-12'>" +
                       "<div class='col-md-" + labelWidth + "'>" +
                       helper.LabelFor(expression) +
                       "</div>" +
                       "<div class='col-md-" + valueWidth + "'>" +
                       helper.DisplayFor(expression) +
                       "</div>" +
                       "</div>";

            return new MvcHtmlString(html);
        }
    }
}