using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.DependecyInjection
{
    public static class AutoRouteModule
    {
        public static AutoRouteBuilder AddAutoRoute(this IServiceCollection services)
        {
            return new AutoRouteBuilder(services);
        }
    }
}
