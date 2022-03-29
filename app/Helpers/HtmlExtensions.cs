using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data.Entities;

namespace app.Helpers
{
    public static class HtmlExtensions
    {
        private const string UserHtmlTemplate =
@"
<p>Name: {0}</p>
<p>Status: {1}</p>
";
        public static string ToHtml(this User user)
        {
            return string.Format(UserHtmlTemplate, user.Name, user.Status.ToString());
        }
    }
}
