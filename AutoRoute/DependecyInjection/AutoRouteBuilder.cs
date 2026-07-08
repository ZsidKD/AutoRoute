using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.DependecyInjection
{
    public sealed class AutoRouteBuilder
    {
        public IServiceCollection Services { get; }

        internal AutoRouteBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
