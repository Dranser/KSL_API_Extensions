namespace KSL.API.Extensions.UI
{
    public static class UIConditionHelper
    {
        public static bool Always() =>
            true;

        public static bool CarLinked() =>
            GameContext.CarIsLinked;

        public static bool CarValid() =>
            GameContext.CarIsValid;

        public static bool OnTrack() =>
            GameContext.IsOnTrack;

        public static bool InGarage() =>
            GameContext.IsInGarage;

        public static bool CarReadyOnTrack() =>
            GameContext.CarIsValid && GameContext.IsOnTrack;

        public static bool ActiveSession() =>
            GameContext.IsOnTrack || GameContext.IsInGarage;
    }
}
