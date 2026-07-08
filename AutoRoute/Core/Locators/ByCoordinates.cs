using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Locators
{
    public class ByCoordinates : ILocator
    {
        public string LocatorType {  get; set; }
        public object X { get; set; }
        public object Y { get; set; }
    }
}
