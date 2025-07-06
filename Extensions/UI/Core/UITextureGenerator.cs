using UnityEngine;

namespace KSL.API.Extensions.UI
{
    public static class UITextureGenerator
    {
        public static Texture2D GenerateForRect(int width, int height, Color color, int radiusTop, int radiusBottom)
        {
            var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var clear = new Color(0, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool inCorner = false;

                    if (y >= height - radiusTop)
                    {
                        if (x < radiusTop)
                            inCorner = Vector2.Distance(new Vector2(x, y), new Vector2(radiusTop, height - radiusTop)) > radiusTop;

                        if (x >= width - radiusTop)
                            inCorner = Vector2.Distance(new Vector2(x, y), new Vector2(width - radiusTop - 1, height - radiusTop)) > radiusTop;
                    }

                    if (y < radiusBottom)
                    {
                        if (x < radiusBottom)
                            inCorner = Vector2.Distance(new Vector2(x, y), new Vector2(radiusBottom, radiusBottom)) > radiusBottom;

                        if (x >= width - radiusBottom)
                            inCorner = Vector2.Distance(new Vector2(x, y), new Vector2(width - radiusBottom - 1, radiusBottom)) > radiusBottom;
                    }

                    tex.SetPixel(x, y, inCorner ? clear : color);
                }
            }

            tex.Apply();
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;

            return tex;
        }
    }
}
