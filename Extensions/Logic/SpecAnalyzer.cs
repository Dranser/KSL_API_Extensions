namespace KSL.API.Extensions
{
    public class SpecAnalyzer : ISpecAnalyzer
    {
        public SpecProfile Analyze(RaceCar car)
        {
            if (car == null || car.GetDesc() == null)
                return null;

            var desc = car.GetDesc();
            var carXDesc = desc.carXDesc;
            var transmission = carXDesc.transmission;
            var weight = carXDesc.weight;

            var raceClass = ParseRaceClass(car.profileMetaInfo.identifier);
            var drive = ParseDrive(transmission.gearType.ToString());
            var engine = DetermineEnginePosition(weight.frontPercent);

            return new SpecProfile
            {
                RaceClass = raceClass,
                DriveType = drive,
                EnginePosition = engine
            };
        }

        private string ParseRaceClass(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return "UNKNOWN";

            return identifier.ToUpperInvariant();
        }

        private string ParseDrive(string gearTypeName)
        {
            if (string.IsNullOrEmpty(gearTypeName))
                return "UNKNOWN";

            if (gearTypeName.StartsWith("GEAR_"))
                gearTypeName = gearTypeName.Substring("GEAR_".Length);

            return gearTypeName.ToUpperInvariant();
        }

        private string DetermineEnginePosition(float frontPercent)
        {
            if (frontPercent >= 50f) return "FRONT";
            if (frontPercent >= 42f) return "MID";
            return "REAR";
        }
    }
}
