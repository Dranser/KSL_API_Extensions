namespace KSL.API.Extensions.UI
{
    public class UISpacer : UIElementBase
    {
        private readonly float _height;

        public UISpacer(float height)
        {
            _height = height;
        }

        public override float GetHeight(float width) => _height;

        public override void Draw(UnityEngine.Rect rect)
        {

        }
    }
}
