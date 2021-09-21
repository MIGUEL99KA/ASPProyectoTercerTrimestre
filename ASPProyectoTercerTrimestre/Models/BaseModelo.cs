 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace ASPProyectoTercerTrimestre.Models
{
    public class BaseModelo
    {
        public int Actualpage { get; set; }

        public int Total { get; set; }

        public int Recordspage { get; set; }

        public RouteValueDictionary valueQueryString { get; set; }
    }
}