using CarX;
using System.Collections.Generic;
using System.Linq;

namespace KSL.API.Extensions
{
    public class SurfaceStatus
    {
        public struct WheelStatus
        {
            public bool IsValid;
            public SurfaceType Type;
            public bool IsAsphalt => Type == SurfaceType.Asphalt;
        }

        public WheelStatus[] Wheels = new WheelStatus[4];

        public bool AllValid => Wheels.All(w => w.IsValid);
        public bool AllAsphalt => Wheels.All(w => w.IsValid && w.IsAsphalt);
        public bool HasSurfaceMismatch => Wheels.Select(w => w.Type).Distinct().Count() > 1;
        public int AsphaltCount => Wheels.Count(w => w.IsValid && w.IsAsphalt);
        public int NonAsphaltCount => Wheels.Count(w => w.IsValid && !w.IsAsphalt);

        public bool HasOffTrackPenalty => NonAsphaltCount > 2;
    }
}
