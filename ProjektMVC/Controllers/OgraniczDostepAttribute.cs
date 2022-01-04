using Microsoft.AspNetCore.Http;
using System;

namespace ProjektAPI.Controllers
{
    internal class OgraniczDostepAttribute : Attribute
    {
        HttpContext uzyt;
        public OgraniczDostepAttribute(HttpContext x)
        {
            uzyt = x;
            
        }

    }
}