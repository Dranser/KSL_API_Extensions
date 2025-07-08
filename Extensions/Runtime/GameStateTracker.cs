using UnityEngine;

namespace KSL.API.Extensions
{
    public class GameStateTracker : MonoBehaviour
    {
        public static bool IsInGarage { get; private set; }
        public static bool IsOnTrack { get; private set; }

        public static SurfaceStatus Surface { get; private set; } = new SurfaceStatus();

        public static bool IsOnValidSurface => Surface.AllAsphalt;
        public static bool HasSurfaceMismatch => Surface.HasSurfaceMismatch;

        public static float OffTrackTime { get; private set; }
        public static bool IsDrifting { get; private set; }
        public static float DriftAngle { get; private set; }

        private const float SpeedThreshold = 20f;

        private static GameStateTracker _instance;

        public static void Init()
        {
            if (_instance != null) return;
            var go = new GameObject("GameStateTracker");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<GameStateTracker>();
        }

        private void Update()
        {
            var main = States.mainCurrent;
            IsInGarage = main is GarageGUIState;
            IsOnTrack = main is BaseRaceState;

            var ctx = CarState.Current;
            Surface = SurfaceAnalyzer.Analyze(ctx?.Wheels);

            UpdateDriftAndOffTrack(ctx);
        }

        private void UpdateDriftAndOffTrack(CarContext ctx)
        {
            if (!IsOnTrack || !CarValidator.IsValid(ctx))
            {
                OffTrackTime = 0f;
                IsDrifting = false;
                return;
            }

            IsDrifting = ctx.RaceCar.attachedDriftController?.isDrift ?? false;
            DriftAngle = ctx?.RaceCar?.attachedDriftController?.driftAngle ?? 0f;

            bool isOffTrack = Surface.HasOffTrackPenalty;
            bool highSpeed = CarDataAccessor.GetCarX(ctx)?.speedKMH > SpeedThreshold;

            if (isOffTrack && highSpeed && !IsDrifting)
                OffTrackTime += Time.deltaTime;
            else
                OffTrackTime = 0f;
        }
    }
}
