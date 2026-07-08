using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Locators
{
    public class ByXPath : ILocator
    {
        public string LocatorType { get; set; }
        public string XPath { get; set; }
    }
}
