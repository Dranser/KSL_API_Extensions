namespace KSL.API.Extensions
{
    public class CarLinkerFeature : FeatureBase
    {
        public override string Id => "system.carlinker";

        public override void Init()
        {
            KSLEvents.CarLoaded += OnCarLoaded;
            KSLEvents.DynoEntered += OnDynoEntered;
            KSLEvents.DynoExited += OnDynoExited;
        }

        public override void Shutdown()
        {
            KSLEvents.CarLoaded -= OnCarLoaded;
            KSLEvents.DynoEntered -= OnDynoEntered;
            KSLEvents.DynoExited -= OnDynoExited;
            CarState.Clear();
        }

        private void OnCarLoaded(RaceCar car)
        {
            var context = CarContextFactory.Create(car);
            context.Profile = CarProfileService.Load(car);
            CarState.Set(context);
        }

        private void OnDynoEntered(UIDynostandContext ctx)
        {
            if (CarState.Current != null)
                CarState.Current.DynoContext = ctx;
        }

        private void OnDynoExited()
        {
            if (CarState.Current != null)
                CarState.Current.DynoContext = null;
        }
    }
}
