using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Tag_GoAPI
{
    public class IntRouteConstraint : IRouteConstraint
    {
    #nullable disable
        private static readonly Regex _intRegex = new Regex(@"^\d+$", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value) && value != null)
            {
                var valuesString = Convert.ToString(value);
                return _intRegex.IsMatch(valuesString);
            }
            return false;
        }
    }
}
