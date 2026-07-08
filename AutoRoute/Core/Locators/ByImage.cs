using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRoute.Abstarctions.Locators
{
    public class ByImage : ILocator
    {
        public string LocatorType { get; set; }
        public string? Path { get; }
        public byte[]? Bytes { get; }
    }
}
