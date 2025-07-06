using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public static class UIStyle
    {
        private static bool _initialized = false;

        public static GUIStyle LabelStyle;
        public static GUIStyle ButtonStyle;
        public static GUIStyle ToggleStyle;
        public static GUIStyle SectionHeaderStyle;
        public static GUIStyle SectionContentStyle;
        public static GUIStyle WindowStyle;
        public static GUIStyle NotificationStyle;

        private static Texture2D _windowBg;
        private static Texture2D _sectionHeaderBg;
        private static Texture2D _sectionContentBg;
        private static Texture2D _buttonBase;
        private static Texture2D _buttonHover;
        private static Texture2D _buttonActive;
        private static Texture2D _sliderBg;
        private static Texture2D _sliderFill;
        private static Texture2D _sliderKnob;
        private static Texture2D _notificationBg;

        public static void Init()
        {
            if (_initialized)
                return;

            GenerateTextures();
            CreateStyles();

            _initialized = true;
        }

        private static void GenerateTextures()
        {
            _windowBg = UITextureGenerator.GenerateForRect(400, 300, new Color32(26, 26, 26, 255), 12, 12);
            _sectionHeaderBg = UITextureGenerator.GenerateForRect(400, 28, new Color32(42, 42, 42, 255), 6, 6);
            _sectionContentBg = UITextureGenerator.GenerateForRect(400, 200, new Color32(30, 30, 30, 255), 0, 6);

            _buttonBase = UITextureGenerator.GenerateForRect(200, 30, new Color32(50, 50, 50, 255), 4, 4);
            _buttonHover = UITextureGenerator.GenerateForRect(200, 30, new Color32(70, 70, 70, 255), 4, 4);
            _buttonActive = UITextureGenerator.GenerateForRect(200, 30, new Color32(0, 191, 207, 255), 4, 4);

            _sliderBg = UITextureGenerator.GenerateForRect(200, 12, new Color32(50, 50, 50, 255), 2, 2);
            _sliderFill = UITextureGenerator.GenerateForRect(200, 12, new Color32(0, 191, 207, 255), 2, 2);
            _sliderKnob = UITextureGenerator.GenerateForRect(16, 24, new Color32(255, 255, 255, 255), 4, 4);

            _notificationBg = UITextureGenerator.GenerateForRect(400, 60, new Color32(50, 50, 50, 255), 6, 6);
        }

        private static void CreateStyles()
        {
            LabelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                normal = { textColor = Color.white }
            };

            ButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 13,
                alignment = TextAnchor.MiddleCenter,
                border = new RectOffset(4, 4, 4, 4),
                padding = new RectOffset(8, 8, 4, 4),
                normal = { background = _buttonBase, textColor = Color.white },
                hover = { background = _buttonHover, textColor = Color.white },
                active = { background = _buttonActive, textColor = Color.black },
                focused = { background = _buttonBase, textColor = Color.white }
            };

            ToggleStyle = new GUIStyle(ButtonStyle)
            {
                alignment = TextAnchor.MiddleLeft
            };

            SectionHeaderStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 13,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(8, 8, 4, 4),
                fontStyle = FontStyle.Bold,
                border = new RectOffset(6, 6, 6, 6),
                normal = { background = _sectionHeaderBg, textColor = Color.white },
                onNormal = { background = _sectionHeaderBg, textColor = Color.white },
                hover = { background = _sectionHeaderBg, textColor = Color.white },
                onHover = { background = _sectionHeaderBg, textColor = Color.white },
                active = { background = _sectionHeaderBg, textColor = Color.white },
                onActive = { background = _sectionHeaderBg, textColor = Color.white },
                focused = { background = _sectionHeaderBg, textColor = Color.white },
                onFocused = { background = _sectionHeaderBg, textColor = Color.white }
            };

            SectionContentStyle = new GUIStyle(GUI.skin.box)
            {
                border = new RectOffset(6, 6, 6, 6),
                padding = new RectOffset(8, 8, 8, 8),
                normal = { background = _sectionContentBg }
            };

            WindowStyle = new GUIStyle(GUI.skin.window)
            {
                fontSize = 14,
                alignment = TextAnchor.UpperCenter,
                fontStyle = FontStyle.Bold,
                border = new RectOffset(12, 12, 12, 12),
                padding = new RectOffset(10, 10, 24, 10),
                normal = { background = _windowBg, textColor = Color.white },
                onNormal = { background = _windowBg, textColor = Color.white },
                hover = { background = _windowBg, textColor = Color.white },
                onHover = { background = _windowBg, textColor = Color.white },
                active = { background = _windowBg, textColor = Color.white },
                onActive = { background = _windowBg, textColor = Color.white },
                focused = { background = _windowBg, textColor = Color.white },
                onFocused = { background = _windowBg, textColor = Color.white }
            };

            NotificationStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 13,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true,
                border = new RectOffset(6, 6, 6, 6),
                padding = new RectOffset(10, 10, 10, 10),
                normal = { background = _notificationBg, textColor = Color.white }
            };
        }

        public static Texture2D GetSliderBg() => _sliderBg;
        public static Texture2D GetSliderFill() => _sliderFill;
        public static Texture2D GetSliderKnob() => _sliderKnob;
    }
}
