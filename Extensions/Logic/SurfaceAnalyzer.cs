using CarX;

namespace KSL.API.Extensions
{
    public static class SurfaceAnalyzer
    {
        public static SurfaceStatus Analyze(Wheel[] wheels)
        {
            var status = new SurfaceStatus();

            if (wheels == null || wheels.Length != 4)
                return status;

            for (int i = 0; i < 4; i++)
            {
                status.Wheels[i] = new SurfaceStatus.WheelStatus
                {
                    IsValid = wheels[i].isValid,
                    Type = wheels[i].surfaceType
                };
            }

            return status;
        }
    }
}
