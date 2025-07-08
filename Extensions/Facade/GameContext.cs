using DB.Meta;
using CarX;

namespace KSL.API.Extensions
{
    public static class GameContext
    {
        public static CarContext Car => CarState.Current;

        public static RaceCar RaceCar => Car?.RaceCar;
        public static PlayerCarProfile Profile => Car?.Profile;
        public static Wheel[] Wheels => Car?.Wheels;
        public static CarX.Car CarX => CarDataAccessor.GetCarX(Car);
        public static BaseCar.Desc Desc => CarDataAccessor.GetDesc(Car);

        public static bool CarIsValid => CarValidator.IsValid(Car);
        public static bool CarIsLinked => Car?.IsLinked ?? false;

        public static bool IsOnTrack => GameStateTracker.IsOnTrack;
        public static bool IsInGarage => GameStateTracker.IsInGarage;
        public static bool IsOnValidSurface => GameStateTracker.IsOnValidSurface;
        public static bool HasSurfaceMismatch => GameStateTracker.HasSurfaceMismatch;

        public static float OffTrackTime => GameStateTracker.OffTrackTime;
        public static bool IsDrifting => GameStateTracker.IsDrifting;
    }
}
