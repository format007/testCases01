using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Tools
{
    public class URLHelpers
    {
        public static readonly string HttpRegexTemplate =
            @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";
    }
}
