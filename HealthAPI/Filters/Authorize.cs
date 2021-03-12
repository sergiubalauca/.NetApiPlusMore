using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Filters
{
    public class Authorize : TypeFilterAttribute
    {
        public Authorize(string permissions) : base(typeof(TypeFilterAttribute))
        {
            Arguments = new object[] {
                permissions
            };
        }
    }
}
