using DB.Meta;
using DB;
using DI;
using System;

namespace KSL.API.Extensions
{
    public class ProfileLoader : IProfileLoader
    {
        public PlayerCarProfile Load(RaceCar car)
        {
            try
            {
                var prefs = DependencyInjector.Resolve<GamePrefs>();
                var model = DependencyInjector.Resolve<BaseModel>();

                if (prefs == null || model == null || car == null)
                    return null;

                int carId = car.carId;
                int profileId = prefs.carSettings.GetProfileIdForCar(carId);

                return model
                    .QueryRecordsByColumnValue<PlayerCarProfile>("carId", carId, x => x.carId == carId)
                    .Find(x => x.id == profileId);
            }
            catch (Exception ex)
            {
                ExtLog.Error(ex);
                return null;
            }
        }
    }
}
