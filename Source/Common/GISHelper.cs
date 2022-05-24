using System;
using System.Collections.Generic;
using System.Text;
using MapInfo.Mapping;
using MapInfo.Engine;
using MapInfo.Windows.Controls;
using MapInfo.Geometry;

namespace Devgis.Common
{
    public class GISHelper
    {
        public static Double GetDistance(DPoint dpStart, DPoint dpEnd)
        { 
            double dResult=(dpStart.x-dpEnd.x)*(dpStart.x-dpEnd.x)+(dpStart.y-dpEnd.y)*(dpStart.y-dpEnd.y);
            return Math.Sqrt(dResult);
        }

    }
}
