using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertismentPlatform.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class QueryStringAttribute : ActionMethodSelectorAttribute
    {

        public string[] ValueNames ;
        public bool ValuePresent { get; set; }
        public QueryStringAttribute(string[] ValueNames, bool valuePresent=true)
        {
            this.ValueNames = ValueNames;
            this.ValuePresent = valuePresent;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            bool ok = false;
            foreach(var value in ValueNames)
            {
               ok = (ok || !StringValues.IsNullOrEmpty(routeContext.HttpContext.Request.Query[value]));
            }
            if (this.ValuePresent)
            {
                return ok;
            }
            return !ok;
        }
    }

}
