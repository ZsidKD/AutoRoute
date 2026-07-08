using AutoRoute.DependecyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.WebScrapping.Execute
{
    public static class WebScrapperModule 
    {
        public static AutoRouteBuilder AddWebScrapper(this AutoRouteBuilder builder, Action<WebScrapperOptions> configure)
        {
            return builder;
        }
    }
}
