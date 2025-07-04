using KSL.API.Extensions;

public class CarLinkerFeature : ModFeatureBase
{
    public override string Id => "system.carlinker";
    public override bool IsSystem => true;

    private readonly ICarBinder _carBinder = new CarBinder();
    private readonly IProfileLoader _profileLoader = new ProfileLoader();

    public override void OnInit()
    {
        PatcherHooks.CarLoaded += OnCarLoaded;
        PatcherHooks.DynoEntered += OnDynoEntered;
        PatcherHooks.DynoExited += OnDynoExited;
    }

    public override void OnShutdown()
    {
        PatcherHooks.CarLoaded -= OnCarLoaded;
        PatcherHooks.DynoEntered -= OnDynoEntered;
        PatcherHooks.DynoExited -= OnDynoExited;
        CarState.Clear();
    }

    private void OnCarLoaded(RaceCar car)
    {
        var context = _carBinder.Bind(car);
        context.Profile = _profileLoader.Load(car);
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
