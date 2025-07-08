using HarmonyLib;

namespace KSL.API.Extensions
{
    public interface IPatchHandler
    {
        void Apply(Harmony harmony);
    }
}
