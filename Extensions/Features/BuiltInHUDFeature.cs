using KSL.API.Extensions;
using KSL.API.Extensions.UI;
using UnityEngine;

[ModFeature]
public class BuiltInHUDFeature : FeatureBase, IModFeatureLifecycle
{
    public override string Id => "system.hud";

    public new bool Enabled = true;
    public bool IsSystem = true;

    private string _lastGear = "";
    private float _gearDisplayTime = 0f;

    private float _rpmSmoothed = 0f;

    public void Update()
    {
        if (!Enabled || !GameContext.IsOnTrack || !GameContext.CarIsValid) return;

        var gear = GameContext.CarX.gear.ToString();
        if (_lastGear != gear)
        {
            _lastGear = gear;
            _gearDisplayTime = Time.time + 1.5f;
        }
    }

    public void Draw()
    {
        if (!Enabled || !GameContext.IsOnTrack || !GameContext.CarIsValid) return;

        var car = GameContext.CarX;

        float targetRpm = car.rpm;
        _rpmSmoothed = Mathf.Lerp(_rpmSmoothed, targetRpm, 10f * Time.deltaTime);
        float rpm = _rpmSmoothed;

        float speed = car.speedKMH;

        float visualRevLimiter = Mathf.Max(1000f, car.engineRevLimiter * 0.97f);
        float rpmPct = Mathf.Clamp01(rpm / visualRevLimiter);

        float width = 320f;
        float barHeight = 24f;
        float boxHeight = 48f;
        float x = (Screen.width - width) * 0.5f;
        float y = Screen.height - (barHeight + boxHeight) - 112f;

        var rpmBarRect = new Rect(x, y, width, barHeight);
        var infoRect = new Rect(x, y + barHeight, width, boxHeight);

        float fillWidth = rpmBarRect.width * rpmPct;

        GUI.DrawTexture(
            new Rect(rpmBarRect.x, rpmBarRect.y, fillWidth, rpmBarRect.height),
            UIStyle.RPMFillCyan,
            ScaleMode.StretchToFill
        );

        GUI.DrawTexture(infoRect, UIStyle.HUDBoxBgBottom);

        string gearText;
        if (car.gear == -1)
            gearText = "R";
        else if (car.gear == 0)
            gearText = "N";
        else
            gearText = speed.ToString("F0");

        string infoText = gearText + " | " + rpm.ToString("F0");

        var infoStyle = new GUIStyle(UIStyle.LabelStyle)
        {
            fontSize = 28,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        GUI.Label(infoRect, infoText, infoStyle);

        if (Time.time < _gearDisplayTime)
        {
            string gearDisplay;
            if (car.gear == -1)
                gearDisplay = "R";
            else if (car.gear == 0)
                gearDisplay = "N";
            else
                gearDisplay = car.gear.ToString();

            var gearStyle = new GUIStyle(UIStyle.LabelStyle)
            {
                fontSize = 64,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold
            };

            GUI.Label(
                new Rect((Screen.width - 200f) * 0.5f, (Screen.height - 100f) * 0.5f, 200f, 100f),
                gearDisplay,
                gearStyle
            );
        }

        // === DRIFT ANGLE BAR (bottom)
        if (GameStateTracker.IsDrifting)
        {
            float driftAngle = GameStateTracker.DriftAngle;
            float maxDriftAngle = 75f;
            float driftPct = Mathf.Clamp01(driftAngle / maxDriftAngle);

            float driftBarWidth = 320f;
            float driftBarHeight = 16f;
            float driftX = (Screen.width - driftBarWidth) * 0.5f;
            float driftY = Screen.height - driftBarHeight - 32f;

            var driftBarRect = new Rect(driftX, driftY, driftBarWidth, driftBarHeight);
            GUI.DrawTexture(driftBarRect, UIStyle.HUDBoxBgBottom);

            float driftFillWidth = driftBarRect.width * driftPct;

            GUI.DrawTexture(
                new Rect(driftBarRect.x, driftBarRect.y, driftFillWidth, driftBarRect.height),
                UIStyle.RPMFillCyan,
                ScaleMode.StretchToFill
            );

            var driftLabelStyle = new GUIStyle(UIStyle.LabelStyle)
            {
                fontSize = 18,
                alignment = TextAnchor.UpperCenter,
                fontStyle = FontStyle.Bold
            };

            GUI.Label(
                new Rect(driftBarRect.x, driftBarRect.y - 38f, driftBarRect.width, 28f),
                driftAngle.ToString("F0") + "°",
                driftLabelStyle
            );
        }
    }

    public override void Shutdown() { }
}
