using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace TradeApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;
            //settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}/{itemid}",
                defaults: new { id = RouteParameter.Optional, itemid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SearchApi",
                routeTemplate: "api/{controller}/{action}/{id}/{term}",
                defaults: new { id = RouteParameter.Optional, term = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "Products",
            //    routeTemplate: "api/{controller}/{action}/{id}/{productid}",
            //    defaults: new { id = RouteParameter.Optional, productid = RouteParameter.Optional }
            //);
        }
    }
}
