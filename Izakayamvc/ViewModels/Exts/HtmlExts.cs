using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;

namespace Izakayamvc.ViewModels
{
    public static class HtmlExts
    {
        public static IHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> html,
                                                                                                                                        Expression<Func<TModel, TProperty>> expression,
                                                                                                                                        Dictionary<int, string> dataSource)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("radio");

            StringBuilder itemBuilder = new StringBuilder();
            foreach (var key in dataSource.Keys)
            {
                var rdo = html.RadioButtonFor(expression, key);
                var lbl = new TagBuilder("Label");
                lbl.InnerHtml = rdo.ToString() + " " + dataSource[key] + "&nbsp;&nbsp;";

                itemBuilder.AppendLine(lbl.ToString());
            }
            div.InnerHtml = itemBuilder.ToString();

            return MvcHtmlString.Create(div.ToString());

        }
        public static FormsAuthenticationTicket GetUserData()
        {

            string tokenString = HttpContext.Current.Request.Cookies["Izakayz"]?.Value; //抓TOKEN
            if (!string.IsNullOrEmpty(tokenString))
            {
                var decryptedTicket = FormsAuthentication.Decrypt(tokenString);  //解密TOKEN，取得Ticket
                return decryptedTicket;
            }

            return null;
        }
    }
}