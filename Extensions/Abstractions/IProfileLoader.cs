using DB.Meta;

namespace KSL.API.Extensions
{
    public interface IProfileLoader
    {
        PlayerCarProfile Load(RaceCar car);
    }
}
